﻿using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrdersService
{
    Task<ICollection<OrderDto>> GetOrders();
    Task<OrderDto> GetOrder(int orderId);
    Task<OrderDto> GetLastOrderFromUser(int userId);
    Task<OrderDto> CreateOrder(OrderDto orderCreate);
    Task<OrderDto> UpdateOrder(OrderDto orderUpdate);
    Task<OrderDto> DeleteOrder(int orderId);
}
