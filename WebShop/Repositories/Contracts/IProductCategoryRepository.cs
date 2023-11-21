using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts
{
    public interface IProductCategoryRepository
    {
        Task<ICollection<ProductCategory>> GetCategories();
        Task<ProductCategory> GetCategory(int categoryId);
        Task<bool> CategoryExist(int categoryId);
        Task<bool> CreateCategory(ProductCategory category);
        Task<bool> UpdateCategory(ProductCategory category);
        Task<bool> DeleteCategory(ProductCategory category);
        Task<bool> Save();
    }
}
