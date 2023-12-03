using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CartRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CartExist(int cartId)
    {
        var cartExist = await _dbContext.Carts.AnyAsync(ci => ci.Id == cartId);

        return cartExist;
    }

    public async Task<bool> CreateCart(Cart cart)
    {
        await _dbContext.AddAsync(cart);

        return await Save();
    }

    public async Task<bool> DeleteCart(Cart cart)
    {
        _dbContext.Remove(cart);

        return await Save();
    }

    public async Task<Cart> GetCart(int cartId)
    {
        var cart = await _dbContext.Carts.Where(c => c.Id == cartId).FirstOrDefaultAsync();

        if (cart != null)
        {
            return cart;
        }

        throw new Exception("No cart with that Id was found");
    }

    public async Task<Cart> GetCartByUser(int UserId)
    {
        var userCart = await _dbContext.Carts.Where(c => c.UserId == UserId).FirstOrDefaultAsync();

        if (userCart != null)
        {
            return userCart;
        }

        throw new Exception("No cart was found with that user");
    }

    public async Task<ICollection<Cart>> GetCarts()
    {
        var carts = await _dbContext.Carts.ToListAsync();

        return carts;
    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<bool> UpdateCart(Cart cart)
    {
        _dbContext.Update(cart);

        return await Save();
    }
}
