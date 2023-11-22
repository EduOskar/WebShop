using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IUsersService
{
    Task<ICollection<UserDto>> GetUsers();
    Task<UserDto> GetUser(int userId);
    Task<bool> UserExist(int userId);
    Task<bool> CreateUser(UserDto user);
    Task<bool> UpdateUser(UserDto user);
    Task<bool> DeleteUser(UserDto user);
    Task<bool> Save();
}
