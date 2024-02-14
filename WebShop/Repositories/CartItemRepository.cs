using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;
using WebShop.Models.DTOs;

namespace WebShop.Api.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CartItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CartItemExist(int cartItemId)
    {
        var cartItemExist = await _dbContext.CartItems
            .AsNoTracking()
            .Include(ci => ci.Product)
            .AnyAsync(ci => ci.Id == cartItemId);


        return cartItemExist;
    }

    public async Task<bool> CreateCartItem(CartItem cartItem)
    {
        await _dbContext.AddAsync(cartItem);
        return await Save();
    }
    public async Task<bool> DeleteCartItem(CartItem cartItem)
    {
        _dbContext.Remove(cartItem);

        return await Save();
    }

    public async Task<bool> DeleteCartItems(ICollection<CartItem> cartItems)
    {
        _dbContext.RemoveRange(cartItems);

        return await Save();
    }

    public async Task<CartItem> GetCartItem(int cartItemId)
    {
        var cartItem = await _dbContext.CartItems
            .AsNoTracking()
            .Include(ci => ci.Cart)
            .Include(p => p.Product)
            .ThenInclude(d => d.ProductsDiscount)
            .Where(ci => ci.Id == cartItemId)
            .FirstOrDefaultAsync();

        if (cartItem != null)
        {
            return cartItem;
        }

        throw new Exception("There was no cartItem with that Id");
    }

    public async Task<ICollection<CartItem>> GetCartItems(int userId)
    {
        var cartItems = await _dbContext.CartItems
            .AsNoTracking()
            .Include(ci => ci.Cart)
            .Include(p => p.Product)
            .Where(ci => ci.Cart!.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
        return cartItems;
    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<bool> UpdateCartItem(CartItem cartItem)
    {

        _dbContext.Update(cartItem);

        return await Save();
    }

    public async Task<bool> UpdateCartItemQty(int cartItemId, CartItemQtyUpdateDto updatedQty)
    {
        var cartItem = await _dbContext.CartItems
            .FindAsync(cartItemId);

        if (cartItem != null)
        {
            cartItem.Quantity = updatedQty.Quantity;

            _dbContext.Update(cartItem);

            return await Save();
        }

        throw new Exception("There was no cartItem with that Id");
    }
}
