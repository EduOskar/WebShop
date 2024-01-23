using WebShop.Api.Entity;
using WebShop.Models.DTOs;

namespace WebShop.Api.Repositories.Contracts;

public interface IOrderStatusRepository
{
    Task<bool> OrderStatusExist(int orderId);

    Task<bool> CreateOrderStatus(OrderStatus orderStatus);

    Task<OrderStatus> GetOrderStatus(int orderStatusId);

    Task<bool> UpdateOrderStatus(OrderStatus orderstatus);

    Task<bool> Save();
}
