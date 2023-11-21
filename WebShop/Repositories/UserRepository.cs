using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateUser(User user)
        {
            await _dbContext.AddAsync(user);

            return await Save();
        }

        public async Task<bool> DeleteUser(User user)
        {
            _dbContext.Remove(user);
            
            return await Save();
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _dbContext.Users.Where(u => u.Id == userId)
                .Include(r => r.Reviews)
                .SingleOrDefaultAsync();

            if (user != null)
            {
                return user;
            }

            throw new Exception("No user by that Id");
        }

        public async Task<ICollection<User>> GetUsers()
        {
            var users = await _dbContext.Users.ToListAsync();

            return users;
        }

        public async Task<bool> Save()
        {
            var saved = await _dbContext.SaveChangesAsync();

            return saved > 0; ;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _dbContext.Update(user);

            return await Save();
        }

        public async Task<bool> UserExist(int userId)
        {
           var userExist = await _dbContext.Users.AnyAsync(u => u.Id == userId);

            return userExist;
        }
    }
}
