using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IProductsCategoryService
{
    Task<ICollection<ProductCategoryDto>> GetCategories();
    Task<ProductCategoryDto> GetCategory(int categoryId);
    Task<bool> CategoryExist(int categoryId);
    Task<bool> CreateCategory(ProductCategoryDto category);
    Task<bool> UpdateCategory(ProductCategoryDto category);
    Task<bool> DeleteCategory(ProductCategoryDto category);
    Task<bool> Save();
}
