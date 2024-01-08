using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IUsersService
{
    Task<List<UserDto>> GetUsers();
    Task<UserDto> GetUser(int userId);
    Task<UserDto> CreateUser(UserDto user);
    Task<bool> UpdateUser(UserDto user);
    Task<bool> DeleteUser(int userId);
}
