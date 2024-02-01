using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;

namespace WebShop.Api.Services;

public class CartOrderTransferService
{
    private readonly ApplicationDbContext _dbContext;

    public CartOrderTransferService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CartOrderTransfer(int userId)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return false;

            var cart = await _dbContext.Carts.Include(c => c.CartItems!)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems!.Any()) return false;

            decimal totalCost = cart.CartItems!.Sum(item => item.Product.Price * item.Quantity);
            if (user.Credit < totalCost) return false;

            var orderItem = new OrderItem
            {
                //OrderId = order.Id,
                //ProductId = item.ProductId,
                //Quantity = item.Quantity
            };

            user.Credit -= totalCost;
            var order = new Order
            {
                UserId = userId,
                PlacementTime = DateTime.Now,
                OrderStatus = new OrderStatus() 
            };
            await _dbContext.Orders.AddAsync(order);

            foreach (var item in cart.CartItems!)
            {
                orderItem.Order = order;
                orderItem.ProductId = item.ProductId;
                orderItem.Product = item.Product;
                orderItem.Quantity = item.Quantity;
                orderItem.OrderId = order.Id;
                orderItem.ProductDescription = item.Product.Description;

                await _dbContext.OrderItems.AddAsync(orderItem);

                item.Product.Quantity -= item.Quantity; 
            }

            _dbContext.CartItems.RemoveRange(cart.CartItems); 
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}
