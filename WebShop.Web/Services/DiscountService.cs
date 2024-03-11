using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;

    public DiscountService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<bool> ActivateProductDiscounts()
    {
        try
        {
            var response = await _httpClient.PostAsync($"api/Discounts/Activate-Discounts-On-Products", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw new Exception(message);
        }

    }

    public async Task<bool> ApplyDiscount(int userId, string discountCode)
    {
        try
        {
            string requestUrl = $"api/Discounts/apply-discount/{userId}-{discountCode}";

            var response = await _httpClient.PostAsync(requestUrl, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            throw new Exception(message);
        }

    }

    public async Task<bool> ApplyDiscountOnProduct(int productId, int discountId)
    {
        try
        {
            var response = await _httpClient.PostAsync($"api/Discounts/apply-discount-on-product/{productId}/{discountId}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            var message = await response.Content.ReadAsStringAsync();
            throw new Exception($"Http status:{response.StatusCode} Message -{message}");
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw new Exception(message);
        }


    }

    public async Task<DiscountDto> CreateDiscount(DiscountDto discountCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<DiscountDto>("api/Discounts", discountCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var discount = await response.Content.ReadFromJsonAsync<DiscountDto>();

                return discount!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            throw new Exception(message);
        }

    }

    public async Task<bool> EmailDiscounts(int discountId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Discounts/Email-discounts/{discountId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return false!;
                }
                return true;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status:{response.StatusCode} Message -{message}");
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            throw new Exception(message);
        }

    }

    public async Task<DiscountDto> GetDiscount(string discountCode)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Discounts/{discountCode}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var discount = await response.Content.ReadFromJsonAsync<DiscountDto>();
                return discount!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            throw new Exception(message);
        }

    }

    public async Task<List<DiscountDto>> GetDiscounts()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Discounts");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var discounts = await response.Content.ReadFromJsonAsync<List<DiscountDto>>();

                if (discounts == null)
                {
                    return null!;
                }
                return discounts;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            throw new Exception(message);
        }

    }

    public async Task<bool> RemoveProductDiscounts()
    {
        try
        {
            var response = await _httpClient.DeleteAsync("api/Discounts");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message - {message}");
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            throw new Exception(message);
        }

    }

    public async Task<bool> UpdateDiscount(DiscountDto discountUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(discountUpdate);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/Discounts/{discountUpdate.Id}", content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            throw new Exception(message);
        }

    }
}
