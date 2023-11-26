using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrdersService
{
    Task<ICollection<OrderDto>> GetOrders();
    Task<OrderDto> OrderExist(int orderId);
    Task<OrderDto> GetOrder(int orderId);
    Task<OrderDto> CreateOrder(OrderDto order);
    Task<OrderDto> UpdateOrder(OrderDto order);
    Task<OrderDto> DeleteOrder(OrderDto order);
}
