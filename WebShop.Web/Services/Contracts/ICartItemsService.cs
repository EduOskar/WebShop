using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface ICartItemService
{
    Task<ICollection<CartItemDto>> GetCartItems();
    Task<CartItemDto> GetCartItem(int cartItemId);
    Task<bool> CartItemExist(int cartItemId);
    Task<bool> CreateCartItem(CartItemDto cartItem);
    Task<bool> UpdateCartItem(CartItemDto cartItem);
    Task<bool> DeleteCartItem(CartItemDto cartItem);
    Task<bool> Save();
}
