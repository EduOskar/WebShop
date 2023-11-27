using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface ICartItemsService
{
    Task<List<CartItemDto>> GetCartItems(int userId);
    Task<CartItemDto> GetCartItem(int cartItemId);
    Task<CartItemDto> CreateCartItem(CartItemDto cartItem);
    Task<CartItemDto> UpdateCartItem(CartItemDto cartItem);
    Task<CartItemDto> UpdateCartItemQty(CartItemQtyUpdateDto cartItemQty);
    Task<CartItemDto> DeleteCartItem(int cartItemId);
}
