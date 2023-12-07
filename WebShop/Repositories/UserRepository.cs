using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> userManager;
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(UserManager<User> userManager, ApplicationDbContext dbContext)
    {
        this.userManager = userManager;
        _dbContext = dbContext;
    }
    public async Task<IdentityResult> CreateUser(User user)
    {
        var userCreated = await userManager.CreateAsync(user);

        await _dbContext.SaveChangesAsync();

        return userCreated;
    }

    public async Task<IdentityResult> DeleteUser(User user)
    {
        return await userManager.DeleteAsync(user);

    }

    public async Task<User> GetUser(int userId)
    {
        string userIdString = userId.ToString();

        var user = await userManager.FindByIdAsync(userIdString);

        if (user != null)
        {
            return user;
        }

        throw new Exception("User could not be found by that id");
    }

    public async Task<ICollection<User>> GetUsers()
    {
        var users = await userManager.Users.ToListAsync();

        return users;
    }
    public async Task<IdentityResult> UpdateUser(User user)
    {
        return await userManager.UpdateAsync(user);

    }

    public async Task<bool> UserExist(int userId)
    {
        string userIdString = userId.ToString();

        var userExist = await userManager.FindByIdAsync(userIdString);

        return userExist != null;
    }
}
