using Microsoft.AspNetCore.Mvc;
using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface ICartsService
{
    Task<ICollection<CartDto>> GetCarts();
    Task<CartDto> GetCart(int cartId);
    Task<CartDto> GetCartByUser(int userId);
    Task<CartDto> CreateCart(CartDto cart);
    Task<CartDto> UpdateCart(CartDto cart);
    Task<CartDto> DeleteCart(CartDto cart);
}
