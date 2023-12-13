using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IAuthService
{
    Task Login(LoginRequest loginRequest);
    Task Register(UserDto registerRequest);
    Task Logout();
    Task<UserDto> CurrentUserInformation();
}
