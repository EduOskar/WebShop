using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrderItemsService
{
    Task<ICollection<OrderItemDto>> GetOrderItems();
    Task<OrderItemDto> GetOrderItem(int orderItemId);
    Task<bool> OrderItemExist(int orderItemId);
    Task<bool> CreateOrderItem(OrderItemDto orderItem);
    Task<bool> UpdateOrderItem(OrderItemDto orderItem);
    Task<bool> DeleteOrderItem(OrderItemDto orderItem);
    Task<bool> Save();
}
