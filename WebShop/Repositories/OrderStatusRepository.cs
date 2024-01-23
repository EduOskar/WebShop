using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderStatusRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateOrderStatus(OrderStatus orderStatus)
    {
        var status = await _dbContext.AddAsync(orderStatus);

        if (status != null)
        {
            return await Save();
        }

        return false;
    }

    public async Task<OrderStatus> GetOrderStatus(int orderStatusId)
    {
        var orderStatus = await _dbContext.OrderStatus.FirstOrDefaultAsync(os => os.Id == orderStatusId);

        if (orderStatus != null)
        {
            return orderStatus;
        }

        throw new Exception(orderStatus?.ToString());
    }

    public async Task<bool> OrderStatusExist(int orderStatusId)
    {
        var orderStatusExist = await _dbContext.OrderStatus.AnyAsync(o => o.Id == orderStatusId);

        return orderStatusExist;

    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<bool> UpdateOrderStatus(OrderStatus orderstatus)
    {
        _dbContext.Update(orderstatus);

        return await Save();
    }
}
