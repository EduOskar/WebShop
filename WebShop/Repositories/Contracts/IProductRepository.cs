using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IProductRepository
{
    Task<ICollection<Product>> GetProducts();
    Task<Product> GetProduct(int productId);
    Task<ICollection<Product>> GetProductByCategory(int categoryId);
    Task<bool> ProductExist(int productId);
    Task<bool> CreateProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Product product);
    Task<bool> Save();
}
