using WebShop.Web.Services.Contracts;
using System.Net.Http.Json;

namespace WebShop.Web.Services;

public class CartOrderTransferService : ICartOrderTransferService
{
    private readonly HttpClient _httpClient;

    public CartOrderTransferService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<bool> CartOrderTransfer(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/CartOrderTransfers/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    var result = await response.Content.ReadAsAsync<bool>();
                    return result;
                }

                return false;
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
}
