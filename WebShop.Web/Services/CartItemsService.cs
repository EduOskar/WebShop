using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services
{
    public class CartItemsService : ICartItemsService
    {
        private readonly HttpClient _httpClient;

        public CartItemsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> CreateCartItem(CartItemDto cartItemCreate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CartItemDto>("api/CartItems", cartItemCreate);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null!;
                    }

                    var cartItem = await response.Content.ReadFromJsonAsync<CartItemDto>();

                    return cartItem!;
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

        public async Task<CartItemDto> DeleteCartItem(int cartItemId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/CartItems/{cartItemId}");

                if (response.IsSuccessStatusCode)
                {

                    var itemDelete = await response.Content.ReadFromJsonAsync<CartItemDto>();
                    return itemDelete!;

                }

                return null!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> GetCartItem(int cartItemId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/GetCartItem{cartItemId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null!;
                    }

                    var cartItem = await response.Content.ReadFromJsonAsync<CartItemDto>();
                    return cartItem!;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CartItemDto>> GetCartItems(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CartItems/GetUsersCartItems/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null!;
                    }

                    var cartItems = await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
                    return cartItems!;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<CartItemDto> UpdateCartItem(CartItemDto cartItem)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItemDto> UpdateCartItemQty(CartItemQtyUpdateDto cartItemQty)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(cartItemQty);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await _httpClient.PatchAsync($"api/CartItems/{cartItemQty.CartItemId}", content);

                if (response.IsSuccessStatusCode)
                {
                    var QtyUpdate = await response.Content.ReadFromJsonAsync<CartItemDto>();

                    return QtyUpdate!;
                }

                return null!;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
