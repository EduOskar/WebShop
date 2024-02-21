using AutoMapper;
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
    private readonly IMapper _mapper;

    public UserRepository(UserManager<User> userManager, ApplicationDbContext dbContext, IMapper mapper)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<bool> AddSupportMessage(SupportMessages supportMessage)
    {
        await _dbContext.SupportMessages.AddAsync(supportMessage);
        return await Save();
    }

    public async Task<List<SupportMessages>> GetSupportMessagesForMail(int supportMailId)
    {
        return await _dbContext.SupportMessages
                                .Where(m => m.SupportMailId == supportMailId)
                                .OrderBy(m => m.CreatedAt)
                                .ToListAsync();
    }

    public async Task<IdentityResult> CreateUser(User user, string password)
    {
        var hashPassword = _userManager.PasswordHasher.HashPassword(user, password);
        user.PasswordHash = hashPassword;

        var result = await _userManager.CreateAsync(user );

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
        var users = await _userManager.Users
            .AsNoTracking()
            .ToListAsync();

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
