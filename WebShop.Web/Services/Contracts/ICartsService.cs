using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface ICartsService
{
    Task<ICollection<CartDto>> GetCarts();
    Task<CartDto> GetCart(int cartId);
    Task<bool> CartExist(int cartId);
    Task<bool> CreateCart(CartDto cart);
    Task<bool> UpdateCart(CartDto cart);
    Task<bool> DeleteCart(CartDto cart);
    Task<bool> Save();
}
