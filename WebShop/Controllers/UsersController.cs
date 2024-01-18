using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    //private static UserModel loggedOutUser = new UserModel { IsAuthenticated  = false };

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UsersController(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(users);
    }

    [HttpGet("{userId:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UserDto>> GetUser(int userId)
    {
        if (!await _userRepository.UserExist(userId))
        {
            return NotFound();
        }

        var user = _mapper.Map<UserDto>(await _userRepository.GetUser(userId));

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateUser([FromBody] UserDto userCreate)
    {
        if (userCreate == null)
        {
            return BadRequest();
        }

        var user = await _userRepository.GetUsers();
        var userCheck = user
            .Where(u => u.Email!.Trim().Equals(userCreate.Email.TrimEnd(), StringComparison.CurrentCultureIgnoreCase))
            .Any();

        if (userCheck)
        {

            return BadRequest("User already exist");
        }

        var userMap = _mapper.Map<User>(userCreate);

        await _userRepository.CreateUser(userMap, userCreate.Password);

        return CreatedAtAction("GetUser", new { userId = userMap.Id }, userMap);

    }



    [HttpPut("{userId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IdentityResult>> UpdateUser(int userId, UserDto updatedUser)
    {
        try
        {
            if (userId != updatedUser.Id)
            {
                return BadRequest("Invalid Id");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return NotFound("User was not found");
            }

            _mapper.Map(updatedUser, user);

            var hashedPasswod = _userManager.PasswordHasher.HashPassword(user, updatedUser.Password);

            user.PasswordHash = hashedPasswod;

            var result = await _userRepository.UpdateUser(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }

    }
    [HttpDelete("{userId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        if (!await _userRepository.UserExist(userId))
        {
            return NotFound();
        }

        var userDelete = await _userRepository.GetUser(userId);

        await _userRepository.DeleteUser(userDelete);

        return NoContent();
    }

}
