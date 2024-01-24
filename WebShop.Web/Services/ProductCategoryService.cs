using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ProductCategoryService : IProductsCategoryService
{
    private readonly HttpClient _httpClient;

    public ProductCategoryService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WebShop.Api");
    }

    public async Task<ProductCategoryDto> CreateCategory(ProductCategoryDto categoryCreate)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<ProductCategoryDto>("api/ProductCategories", categoryCreate);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var productCategory = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();

                return productCategory!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode} Message -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<bool> DeleteCategory(int categoryId)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/productCategories/{categoryId}");

            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<List<ProductCategoryDto>> GetCategories()
    {
        try
        {
            var response = await _httpClient.GetAsync("api/ProductCategories");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var productsCategories = await response.Content.ReadFromJsonAsync<List<ProductCategoryDto>>();
                return productsCategories!;
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

    public async Task<ProductCategoryDto> GetCategory(int categoryId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ProductCategories/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null!;
                }

                var productCategory = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();
                return productCategory!;
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

    public async Task<bool> UpdateCategory(ProductCategoryDto categoryUpdate)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(categoryUpdate);

            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PutAsync($"api/ProductCategories/{categoryUpdate.Id}", content);

            return response.IsSuccessStatusCode;

        }
        catch (Exception)
        {

            throw;
        }
    }
}
