using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CartService : ICartsService
{
    public Task<CartDto> CreateCart(CartDto cart)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> DeleteCart(CartDto cart)
    {
        throw new NotImplementedException();
    }

    public Task<CartDto> GetCart(int cartId)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<CartDto>> GetCarts()
    {
        throw new NotImplementedException();
    }


    public Task<CartDto> UpdateCart(CartDto cart)
    {
        throw new NotImplementedException();
    }
}
