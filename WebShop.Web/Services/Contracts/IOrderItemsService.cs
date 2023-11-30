using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrderItemsService
{
    Task<List<OrderItemDto>> GetOrderItems();
    Task<OrderItemDto> GetOrderItem(int orderItemId);
    Task<OrderItemDto> CreateOrderItem(OrderItemDto orderItemCreate);
    Task<OrderItemDto> UpdateOrderItem(OrderItemDto orderItemUpdate);
    Task<OrderItemDto> DeleteOrderItem(int orderItemId);
}
