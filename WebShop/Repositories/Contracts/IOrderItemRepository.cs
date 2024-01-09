using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IOrderItemRepository
{
    Task<List<OrderItem>> GetOrderItems();
    Task<List<OrderItem>> GetOrderItemsFromOrder(int orderId);
    Task<OrderItem> GetOrderItem(int orderItemId);
    Task<bool> OrderItemExist(int orderItemId);
    Task<bool> CreateOrderItem(OrderItem orderItem);
    Task<bool> UpdateOrderItem(OrderItem orderItem);
    Task<bool> DeleteOrderItem(OrderItem orderItem);
    Task<bool> Save();
}
