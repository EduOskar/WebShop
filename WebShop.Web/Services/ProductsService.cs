using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;

    public ProductsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductDto> CreateProduct(ProductDto productCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<ProductDto>("api/Products", productCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var product = await response.Content.ReadFromJsonAsync<ProductDto>();

                return product!;
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

    public async Task<ProductDto> DeleteProduct(int productId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var productDelete = await response.Content.ReadFromJsonAsync<ProductDto>();

                return productDelete!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ProductDto> GetProduct(int productId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/Products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var product = await response.Content.ReadFromJsonAsync<ProductDto>();
                return product!;
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

    public async Task<List<ProductDto>> GetProducts()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Products");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
                return products!;
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
    public async Task<ProductDto> UpdateProduct(ProductDto productUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(productUpdate);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/Products/{productUpdate.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<ProductDto>();

                return product!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
