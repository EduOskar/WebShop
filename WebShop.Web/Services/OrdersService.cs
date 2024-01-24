using System.Text;
using Newtonsoft.Json;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class OrdersService : IOrdersService
{
    private readonly HttpClient _httpClient;

    public OrdersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<OrderDto> CreateOrder(OrderDto orderCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<OrderDto>("api/Orders", orderCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var order = await response.Content.ReadFromJsonAsync<OrderDto>();

                return order!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http stats:{response.StatusCode} Message- {message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<OrderDto> DeleteOrder(int orderId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Orders/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                var orderDelete = await response.Content.ReadFromJsonAsync<OrderDto> ();

                return orderDelete!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<OrderDto> GetOrder(int orderId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Orders/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var order = await response.Content.ReadFromJsonAsync<OrderDto>();
                return order!;
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
    public async Task<OrderDto> GetLastOrderFromUser(int userId) 
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Orders/Last-Order-By-User/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var order = await response.Content.ReadFromJsonAsync<OrderDto>();
                return order!;
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

    public async Task<List<OrderDto>> GetOrders()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Orders");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>();
                return orders!;
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

    public async Task<OrderDto> UpdateOrder(OrderDto orderUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(orderUpdate);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/Orders/{orderUpdate.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadFromJsonAsync<OrderDto>();

                return order!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<OrderDto>> GetOrdersFromUser(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Orders/Orders-from-user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var orders = await response.Content.ReadFromJsonAsync<List<OrderDto>>();
                return orders!;
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

    public async Task<bool> UpdateOrderStatus(int orderId, OrderStatusType newStatus)
    {
        var jsonRequest = JsonConvert.SerializeObject(newStatus);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"api/Orders/OrderStatus/{orderId}", content);

        return response.IsSuccessStatusCode;
    }
}
