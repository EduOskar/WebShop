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

    public Task<bool> CreateProduct(ProductDto product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(ProductDto product)
    {
        throw new NotImplementedException();
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
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
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

    public Task<ICollection<ProductDto>> GetProductByCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<ProductDto>> GetProducts()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/Products");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }
                return await response.Content.ReadFromJsonAsync<ICollection<ProductDto>>();
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

    public Task<bool> ProductExist(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(ProductDto product)
    {
        throw new NotImplementedException();
    }
}
