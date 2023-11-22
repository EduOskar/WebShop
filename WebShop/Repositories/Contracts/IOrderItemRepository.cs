using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IOrderItemRepository
{
    Task<ICollection<OrderItem>> GetOrderItems();
    Task<OrderItem> GetOrderItem(int orderItemId);
    Task<bool> OrderItemExist(int orderItemId);
    Task<bool> CreateOrderItem(OrderItem orderItem);
    Task<bool> UpdateOrderItem(OrderItem orderItem);
    Task<bool> DeleteOrderItem(OrderItem orderItem);
    Task<bool> Save();
}
