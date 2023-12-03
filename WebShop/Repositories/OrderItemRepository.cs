﻿using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _dbContext;

    public OrderItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateOrderItem(OrderItem orderItem)
    {
        await _dbContext.AddAsync(orderItem);

        return await Save();
    }

    public async Task<bool> DeleteOrderItem(OrderItem orderItem)
    {
        _dbContext.Remove(orderItem);

        return await Save();
    }

    public async Task<OrderItem> GetOrderItem(int orderItemId)
    {
        var orderItem = await _dbContext.OrderItems.FindAsync(orderItemId);

        if (orderItem != null)
        {
            return orderItem;
        }

        throw new Exception("OrderItem was not found");
    }

    public async Task<ICollection<OrderItem>> GetOrderItems()
    {
        var orderItem = await _dbContext.OrderItems.ToListAsync();

        return orderItem;
    }

    public Task<bool> OrderItemExist(int orderItemId)
    {
        var orderItemExist = _dbContext.OrderItems.AnyAsync(oi => oi.Id == orderItemId);

        return orderItemExist;
    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<bool> UpdateOrderItem(OrderItem orderItem)
    {
        _dbContext.Update(orderItem);

        return await Save();
    }
}