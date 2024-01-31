using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;

namespace WebShop.Api.Services;

public class CartOrderTransferService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    // Other necessary services

    public CartOrderTransferService(ApplicationDbContext dbContext, IMapper mapper /* other dependencies */)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        // Initialize other services
    }

    public async Task<bool> CartOrderTransfer(int userId)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null) return false;

            var cart = await _dbContext.Carts.Include(c => c.CartItems!).ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null || !cart.CartItems!.Any()) return false;

            decimal totalCost = cart.CartItems!.Sum(item => item.Product.Price * item.Quantity);
            if (user.Credit < totalCost) return false;

            user.Credit -= totalCost;
            var order = new Order
            {
                UserId = userId,
                PlacementTime = DateTime.Now,
                OrderStatus = new OrderStatus() // Assuming you set up OrderStatus
            };
            await _dbContext.Orders.AddAsync(order);

            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                await _dbContext.OrderItems.AddAsync(orderItem);

                item.Product.Quantity -= item.Quantity; // Update product stock
            }

            _dbContext.CartItems.RemoveRange(cart.CartItems); // Clear the cart
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            // Log exception
            return false;
        }
    }
}
