using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();
        Task<User> GetUser(int userId);
        Task<bool> UserExist(int userId);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        Task<bool> Save();
    }
}
