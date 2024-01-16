using Microsoft.AspNetCore.Identity;
using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IUserRepository
{
    Task<ICollection<User>> GetUsers();

    Task<User> GetUser(int userId);

    Task<bool> UserExist(int userId);

    Task<IdentityResult> CreateUser(User user, string password);

    Task<IdentityResult> UpdateUser(User user);

    Task<IdentityResult> DeleteUser(User user);

    Task<bool> Save();
}
