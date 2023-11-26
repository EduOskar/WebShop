using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IUsersService
{
    Task<ICollection<UserDto>> GetUsers();
    Task<UserDto> GetUser(int userId);
    Task<UserDto> CreateUser(UserDto user);
    Task<UserDto> UpdateUser(UserDto user);
    Task<UserDto> DeleteUser(int userId);
}
