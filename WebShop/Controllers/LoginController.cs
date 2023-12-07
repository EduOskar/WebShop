using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.Api.Entity;
using WebShop.Models.DTOs;

namespace WebShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly SignInManager<User> signInManager;

    public LoginController(IConfiguration configuration, SignInManager<User> signInManager)
    {
        this.configuration = configuration;
        this.signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModels login)
    {
        var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

        if (!result.Succeeded) 
        {
            return BadRequest(new LoginResult {Succesful = false, Error = "Username or password are invalid" });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, login.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(configuration["JwtExpiryInDays"]));

        var token = new JwtSecurityToken(
            configuration["JwtIssuer"],
            configuration["JwtAudience"],
            claims,
            expires: expiry,
            signingCredentials: creds
            );
        return Ok(new LoginResult { Succesful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}
