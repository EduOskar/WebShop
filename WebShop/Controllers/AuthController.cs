using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly ICartRepository _cartRepository;

    public AuthController(UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        IMapper mapper, 
        ICartRepository cartRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _cartRepository = cartRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user != null)
        {
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (signInResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, request.RememberMe);

                return Ok();
            }            
        }

        return BadRequest("Invalid password or username");
        /*

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
        */
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto parameters)
    {

        var userMap = _mapper.Map<User>(parameters);

        if (await _userManager.FindByEmailAsync(userMap.Email!) != null || await _userManager.FindByNameAsync(userMap.UserName!) != null)
        {
            return BadRequest($"{userMap.Email} or {userMap.UserName} already exist");
        }

        if (parameters.Password != parameters.ConfirmPassword)
        {
            return BadRequest($"password is not eqals to the confirmed password");
        }

        var result = await _userManager.CreateAsync(userMap, parameters.Password);

        Cart newCart = new Cart
        {
            UserId = userMap.Id
        };

        await _cartRepository.CreateCart(newCart);

        await _userManager.AddToRoleAsync(userMap, "User");

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

    [HttpGet("CurrentUserInformation")]
    public CurrentUser CurrentUserInformation()
    {
        return new CurrentUser
        {
            IsAuthenticated = User.Identity!.IsAuthenticated,
            UserName = User.Identity.Name!,
            Claims = User.Claims
            .ToDictionary(c => c.Type, c => c.Value)
        };
    }
}
