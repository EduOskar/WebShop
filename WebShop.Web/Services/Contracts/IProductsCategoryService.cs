using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IProductsCategoryService
{
    Task<List<ProductCategoryDto>> GetCategories();
    Task<ProductCategoryDto> GetCategory(int categoryId);
    Task<ProductCategoryDto> CreateCategory(ProductCategoryDto categoryCreate);
    Task<bool> UpdateCategory(ProductCategoryDto categoryUpdate);
    Task<bool> DeleteCategory(int categoryId);
}
