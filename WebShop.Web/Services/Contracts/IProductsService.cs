using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IProductsService
{
    Task<List<ProductDto>> GetProducts();

    Task<ProductDto>? GetProduct(int productId);

    Task<ProductDto> CreateProduct(ProductDto product);

    Task<bool> UpdateProduct(ProductDto product);

    Task<bool> DeleteProduct(int productId);
}
