using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateProduct(Product product)
    {
        await _dbContext.AddAsync(product);

        return await Save();
    }

    public async Task<bool> DeleteProduct(Product product)
    {
        _dbContext.Remove(product);

        return await Save();
    }

    public async Task<Product> GetProduct(int productId)
    {
        var product = await _dbContext.Products
            .Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == productId);

        if (product != null)
        {
            return product;
        }

        throw new Exception("Product was not found");
    }

    public async Task<ICollection<Product>> GetProductByCategory(int categoryId)
    {
        var productsByCategory = await _dbContext.Products.Where(pc => pc.CategoryId == categoryId).ToListAsync();

        return productsByCategory;
    }

    public async Task<ICollection<Product>> GetProducts()
    {
        var products = await _dbContext.Products
            .Include(p => p.Category)
            .ToListAsync();

        return products;
    }

    public async Task<bool> ProductExist(int productId)
    {
        var productExist = await _dbContext.Products.AnyAsync(p => p.Id == productId);

        return productExist;
    }

    public async Task<bool> Save()
    {
        var save = await _dbContext.SaveChangesAsync();

        return save > 0;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        _dbContext.Update(product);

        return await Save();
    }
}
