using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface ICartItemRepository
{
    Task<ICollection<CartItem>> GetCartItems();
    Task<CartItem> GetCartItem(int cartItemId);
    Task<bool> CartItemExist(int cartItemId);
    Task<bool> CreateCartItem(CartItem cartItem);
    Task<bool> UpdateCartItem(CartItem cartItem);
    Task<bool> DeleteCartItem(CartItem cartItem);
    Task<bool> Save();
}
