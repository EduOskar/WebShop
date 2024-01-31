using WebShop.Api.Entity;
using WebShop.Models.DTOs;

namespace WebShop.Api.Repositories.Contracts;

public interface ICartItemRepository
{
    Task<ICollection<CartItem>> GetCartItems(int userId);
    Task<CartItem> GetCartItem(int cartItemId);
    Task<bool> CartItemExist(int cartItemId);
    Task<bool> CreateCartItem(CartItem cartItem);
    Task<bool> UpdateCartItem(CartItem cartItem);
    Task<bool> UpdateCartItemQty(int cartItemId, CartItemQtyUpdateDto updatedQty);
    Task<bool> DeleteCartItem(CartItem cartItem);
    Task<bool> DeleteCartItems(ICollection<CartItem> cartItems);
    Task<bool> Save();
}
