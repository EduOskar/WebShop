using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface ICartItemsService
{
    Task<ICollection<CartItemDto>> GetCartItems(int userId);
    Task<CartItemDto> GetCartItem(int cartItemId);
    Task<CartItemDto> CreateCartItem(CartItemDto cartItem);
    Task<CartItemDto> UpdateCartItem(CartItemDto cartItem);
    Task<CartItemDto> DeleteCartItem(CartItemDto cartItem);
}
