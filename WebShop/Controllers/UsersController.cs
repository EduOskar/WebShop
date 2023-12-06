using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
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
    public async Task<ActionResult<User>> GetUser(int userId)
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
    public async Task<ActionResult<User>> CreateUser([FromBody] UserDto userCreate)
    {
        if (userCreate == null)
        {
            return BadRequest(ModelState);
        }

        var user = await _userRepository.GetUsers(); 
        var userCheck = user
            .Where(u => u.Email!.Trim().Equals(userCreate.Email.TrimEnd(), StringComparison.CurrentCultureIgnoreCase))
            .Any();

        if (userCheck)
        {
            ModelState.AddModelError("", "User Already Exist");
            return BadRequest(ModelState);
        }

        var userMap = _mapper.Map<User>(userCreate);

        await _userRepository.CreateUser(userMap);

        return CreatedAtAction("GetUser", new {userId = userMap.Id}, userMap);

    }

    [HttpPut("{userId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> UpdateUser(int userId, [FromBody] UserDto updatedUser)
    {
        if (updatedUser == null)
        {
            return BadRequest(ModelState);
        }

        if (userId != updatedUser.Id)
        {
            return BadRequest(ModelState);
        }

        if (! await _userRepository.UserExist(userId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userMap = _mapper.Map<User>(updatedUser);

        await _userRepository.UpdateUser(userMap);

        return NoContent();
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
