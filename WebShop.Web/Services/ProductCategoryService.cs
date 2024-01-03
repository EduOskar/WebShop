using System.Net.Http;
using WebShop.Models.DTOs;
using WebShop.Web.Services.Contracts;

namespace WebShop.Web.Services;

public class ProductCategoryService : IProductsCategoryService
{
    private readonly HttpClient _httpclient;

    public ProductCategoryService(HttpClient httpclient)
    {
        _httpclient = httpclient;
    }
    public async Task<ProductCategoryDto> CreateCategory(ProductCategoryDto categoryCreate)
    {
        try
        {
            var response = await _httpclient.PostAsJsonAsync<ProductCategoryDto>("api/ProductCategories", categoryCreate);

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

    public async Task<ProductCategoryDto> DeleteCategory(int categoryId)
    {
        try
        {
            var response = await _httpclient.DeleteAsync($"api/productCategories/{categoryId}");

            if (response.IsSuccessStatusCode)
            {
                var categoryDelete = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();

                return categoryDelete!;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Task<List<ProductCategoryDto>> GetCategories()
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategoryDto> GetCategory(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<ProductCategoryDto> UpdateCategory(ProductCategoryDto categoryUpdate)
    {
        throw new NotImplementedException();
    }
}
