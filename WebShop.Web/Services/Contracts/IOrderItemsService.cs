using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IOrderItemsService
{
    Task<List<OrderItemDto>> GetOrderItems();
    Task<OrderItemDto> GetOrderItem(int orderItemId);
    Task<List<OrderItemDto>> GetOrderItemsFromOrder(int orderId);
    Task<OrderItemDto> CreateOrderItem(OrderItemDto orderItemCreate);
    Task<OrderItemDto> UpdateOrderItem(OrderItemDto orderItemUpdate);

    Task<bool> QuantityCheck(int orderItemId, QuantityCheckDto quantityCheck);

    Task<OrderItemDto> DeleteOrderItem(int orderItemId);
}
