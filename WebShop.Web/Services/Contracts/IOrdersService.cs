using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrdersService
{
    Task<ICollection<OrderDto>> GetOrders();
    Task<OrderDto> GetOrder(int orderId);
    Task<bool> OrderExist(int orderID);
    Task<bool> CreateOrder(OrderDto order);
    Task<bool> UpdateOrder(OrderDto order);
    Task<bool> DeleteOrder(OrderDto order);
    Task<bool> Save();
}
