using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateOrder(Order order)
    {
        await _dbContext.AddAsync(order);

        return await Save();
    }

    public async Task<bool> DeleteOrder(Order order)
    {
        _dbContext.Remove(order);

        return await Save();
    }

    public async Task<Order> GetOrder(int orderId)
    {
        var order = await _dbContext.Orders
            .Include(oi => oi.OrderItems)
            .Include(u => u.User)
            .SingleOrDefaultAsync(o => o.Id == orderId);

        if (order != null)
        {
            return order;
        }

        throw new Exception("Order was not found");
    }

    public async Task<ICollection<Order>> GetOrderFromUser(int userId)
    {
        var orders = await _dbContext.Orders
            .Include(oi => oi.OrderItems)
            .Include(u => u.User)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        return orders;
    }


    public async Task<ICollection<Order>> GetOrders()
    {
        var orders = await _dbContext.Orders
            .Include(oi => oi.OrderItems)
            .Include(u => u.User)
            .ToListAsync();

        return orders;
    }

    public async Task<bool> OrderExist(int orderId)
    {
        var orderExist = await _dbContext.Orders.AnyAsync(o => o.Id == orderId);

        return orderExist;
        
    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        _dbContext.Update(order);

        return await Save();
    }
}
