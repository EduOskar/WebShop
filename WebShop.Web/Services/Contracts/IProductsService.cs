using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IProductsService
{
    Task<ICollection<ProductDto>> GetProducts();
    Task<ProductDto> GetProduct(int productId);
    Task<ICollection<ProductDto>> GetProductByCategory(int categoryId);
    Task<bool> ProductExist(int productId);
    Task<bool> CreateProduct(ProductDto product);
    Task<bool> UpdateProduct(ProductDto product);
    Task<bool> DeleteProduct(ProductDto product);
    Task<bool> Save();
}
