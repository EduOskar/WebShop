using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrdersService
{
    Task<List<OrderDto>> GetOrders();

    Task<List<OrderDto>> GetOrdersFromUser(int userId);

    Task<OrderDto> GetOrder(int orderId);
    Task<OrderDto> GetLastOrderFromUser(int userId);
    Task<OrderDto> CreateOrder(OrderDto orderCreate);
    Task<OrderDto> UpdateOrder(OrderDto orderUpdate);
    Task<OrderDto> DeleteOrder(int orderId);
}
