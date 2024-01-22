using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersAndRolesController : ControllerBase
{
    private readonly IUserRolesRepository _rolesRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UsersAndRolesController(IUserRolesRepository rolesRepository, IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _rolesRepository = rolesRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPut("{userId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateUserRole(int userId, [FromBody] UserRoleDto UserRole)
    {
        if (userId != UserRole.UserId)
        {
            return BadRequest($"UserId {userId} did not match {UserRole.UserId}");
        }

        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            return NotFound("UserId did not correlate to any existing users");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            return BadRequest("Failed to remove user from current roles");
        }

        var role = await _roleManager.FindByIdAsync(UserRole.RoleId.ToString());

        if (role == null)
        {
            return NotFound("Role did not correlate to any existing roles");
        }

        var addResult = await _userManager.AddToRoleAsync(user, role.Name!);
        if (!addResult.Succeeded)
        {
            return BadRequest("Failed to add user to new role");
        }

        return NoContent();
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UserRole>> GetUserRole(int userId)
    {
        var role = await _rolesRepository.GetUserRole(userId);

        return Ok(role);
    }

    [HttpGet]
    public async Task<ActionResult<List<IdentityUserRole<int>>>> GetUsersAndRoles()
    {
        var userAndRoles = await _rolesRepository.GetUsersAndRoles();

        if (userAndRoles == null)
        {
            return BadRequest();
        }

        return Ok(userAndRoles);
    }

    [HttpGet("GetRole/{roleId}")]
    public async Task<ActionResult<IdentityRole>> GetRole(int roleId)
    {
        var role = await _rolesRepository.GetRole(roleId);

        if (role.Id != roleId)
        {
            return BadRequest();
        }

        if (!await _rolesRepository.RoleExist(role.Id))
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpGet("Roles")]
    public async Task<ActionResult<List<IdentityUserRole<int>>>> GetRoles()
    {
        var roles = await _rolesRepository.GetRoles();

        if (roles == null)
        {
            return BadRequest();
        }

        return Ok(roles);
    }

    [HttpDelete("{UserId:int}")]
    public async Task<ActionResult<bool>> DeleteRoles(int UserId)
    {

        var deletedUser = await _rolesRepository.DeleteUserRoles(UserId);

        if (!deletedUser)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<IdentityResult>> CreateRole(RolesDto roleCreate)
    {
        if (roleCreate == null)
        {
            return BadRequest();
        }

        var roles = await _rolesRepository.GetRoles();
        var roleCheck = roles
            .Where(u => u.Name!.Trim().Equals(roleCreate.Name.TrimEnd(), StringComparison.CurrentCultureIgnoreCase))
            .Any();

        if (roleCheck)
        {
            return BadRequest("Role already exist");
        }

        var roleMap = _mapper.Map<Role>(roleCreate);

        await _rolesRepository.CreateRole(roleMap);

        return Ok(roleMap);
    }
}
