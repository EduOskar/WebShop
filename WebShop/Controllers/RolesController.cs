using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRolesRepository _rolesRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RolesController(IRolesRepository rolesRepository, IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
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
}
