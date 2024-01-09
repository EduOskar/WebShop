using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IOrderRepository
{
    Task<List<Order>> GetOrders();
    Task<Order> GetOrder(int orderId);
    Task<Order> GetLastOrderFromUser(int userId);
    Task<bool> OrderExist(int orderID);
    Task<bool> CreateOrder(Order order);
    Task<bool> UpdateOrder(Order order);
    Task<bool> DeleteOrder(Order order);
    Task<bool> Save();
    
}
