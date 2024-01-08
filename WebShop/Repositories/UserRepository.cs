using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public UserRepository(UserManager<User> userManager, ApplicationDbContext dbContext, RoleManager<IdentityRole<int>> roleManager)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _roleManager = roleManager;
    }
    public async Task<IdentityResult> CreateUser(User user)
    {
        var hashPassword = _userManager.PasswordHasher.HashPassword(user, user.Password);
        user.PasswordHash = hashPassword;

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");

            Cart newCart = new Cart()
            {
                UserId = user.Id,
            };

            await  _dbContext.Carts.AddAsync(newCart);
            await _dbContext.SaveChangesAsync();
        }

        return result;
    }

    public async Task<IdentityResult> DeleteUser(User user)
    {
        return await _userManager.DeleteAsync(user);
    }

    public async Task<User> GetUser(int userId)
    {
        string userIdString = userId.ToString();

        var user = await _userManager.FindByIdAsync(userIdString);

        if (user != null)
        {
            return user;
        }

        throw new Exception("User could not be found by that id");
    }

    public async Task<ICollection<User>> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();

        return users;
    }
    public async Task<IdentityResult> UpdateUser(User user)
    {
        return await _userManager.UpdateAsync(user);

    }

    public async Task<bool> Save()
    {
        var save = await _dbContext.SaveChangesAsync();

        return save > 0;
    }

    public async Task<bool> UserExist(int userId)
    {
        var userExist = await _userManager.Users.AnyAsync(u => u.Id == userId);

        return userExist;
    }

}
