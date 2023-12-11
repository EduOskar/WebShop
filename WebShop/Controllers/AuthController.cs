using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebShop.Api.Entity;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return BadRequest("User does not exist");
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        
        if (!signInResult.Succeeded)
        {
            return BadRequest("Invalid password or username");
        }

        await _signInManager.SignInAsync(user, request.RememberMe);

        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto parameters)
    {

        var userMap = _mapper.Map<User>(parameters);

        var result = await _userManager.CreateAsync(userMap, parameters.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }

        return await Login(new LoginRequest
        {
            UserName = parameters.UserName,
            Password = parameters.Password
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet]
    public CurrentUser CurrentUserInformation()
    {
        return new CurrentUser
        {
            IsAuthenticated = User.Identity!.IsAuthenticated,
            UserName = User.Identity.Name!,  
            Claims = User.Claims
            .ToDictionary(c => c.Type, c=>c.Value)
        };
    }
}
