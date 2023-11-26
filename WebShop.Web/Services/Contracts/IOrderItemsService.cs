using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrderItemsService
{
    Task<ICollection<OrderItemDto>> GetOrderItems();
    Task<OrderItemDto> GetOrderItem(int orderItemId);
    Task<OrderItemDto> CreateOrderItem(OrderItemDto orderItem);
    Task<OrderItemDto> UpdateOrderItem(OrderItemDto orderItem);
    Task<OrderItemDto> DeleteOrderItem(OrderItemDto orderItem);
}
