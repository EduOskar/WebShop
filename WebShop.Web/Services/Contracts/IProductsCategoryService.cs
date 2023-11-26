using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IProductsCategoryService
{
    Task<ICollection<ProductCategoryDto>> GetCategories();
    Task<ProductCategoryDto> GetCategory(int categoryId);
    Task<ProductCategoryDto> CategoryExist(int categoryId);
    Task<ProductCategoryDto> CreateCategory(ProductCategoryDto category);
    Task<ProductCategoryDto> UpdateCategory(ProductCategoryDto category);
    Task<ProductCategoryDto> DeleteCategory(ProductCategoryDto category);
}
