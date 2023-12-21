using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IProductsService
{
    Task<List<ProductDto>> GetProducts();
    Task<ProductDto>? GetProduct(int productId);
    Task<ProductDto> CreateProduct(ProductDto product);
    //Kan bli error för i repository är det bool, här är det en ProductDto
    Task<ProductDto> UpdateProduct(ProductDto product);
    Task<ProductDto> DeleteProduct(int productId);
}
