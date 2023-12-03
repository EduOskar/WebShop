using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface ICartRepository
{
    Task<ICollection<Cart>> GetCarts();
    Task<Cart> GetCart(int cartId);
    Task<Cart> GetCartByUser(int UserId);
    Task<bool> CartExist(int cartId);
    Task<bool> CreateCart(Cart cart);
    Task<bool> UpdateCart(Cart cart);
    Task<bool> DeleteCart(Cart cart);
    Task<bool> Save();
}
