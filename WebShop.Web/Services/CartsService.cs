using System.Net.Http;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class CartsService : ICartsService
{
    private readonly HttpClient _httpClient;

    public CartsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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

    public async Task<CartDto> GetCartByUser(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Carts/Get-Cart-By-User/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var cart = await response.Content.ReadFromJsonAsync<CartDto>();

                return cart!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
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
