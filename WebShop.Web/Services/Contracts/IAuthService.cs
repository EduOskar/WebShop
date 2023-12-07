using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IAuthService
{
    Task<UserDto> Register(UserDto registerUser);

    Task<LoginResult> Login(LoginModels loginModel);

    Task Logout();
}
