using System.Text;
using Newtonsoft.Json;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class OrderItemsService : IOrderItemsService
{
    private readonly HttpClient _httpClient;

    public OrderItemsService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<OrderItemDto> CreateOrderItem(OrderItemDto orderItemCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<OrderItemDto>("api/OrderItems", orderItemCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var orderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

                return orderItem!;
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

    public async Task<OrderItemDto> DeleteOrderItem(int orderItemId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/OrderItems/{orderItemId}");

            if (response.IsSuccessStatusCode)
            {

                var orderItemDelete = await response.Content.ReadFromJsonAsync<OrderItemDto>();

                return orderItemDelete!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<OrderItemDto> GetOrderItem(int orderItemId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/OrderItems/{orderItemId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var orderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

                return orderItem!;
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

    public async Task<List<OrderItemDto>> GetOrderItemsFromOrder(int orderId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/OrderItems/Order-Items-From-User/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var orderItems = await response.Content.ReadFromJsonAsync<List<OrderItemDto>>();

                return orderItems!;
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

    public async Task<List<OrderItemDto>> GetOrderItems()
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/OrderItems");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var orderItems = await response.Content.ReadFromJsonAsync<List<OrderItemDto>>();

                return orderItems!;
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

    public async Task<OrderItemDto> UpdateOrderItem(OrderItemDto orderItemUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(orderItemUpdate);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/OrderItems/{orderItemUpdate.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var orderItem = await response.Content.ReadFromJsonAsync<OrderItemDto>();

                return orderItem!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> QuantityCheck(int orderItemId, QuantityCheckDto quantityCheck)
    {
        var jsonRequest = JsonConvert.SerializeObject(quantityCheck);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"api/OrderItems/QuantityCheck/{orderItemId}", content);

        return response.IsSuccessStatusCode;
    }
}
