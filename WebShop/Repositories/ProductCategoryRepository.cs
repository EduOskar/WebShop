using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductCategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CategoryExist(int categoryId)
    {
        var categoryExist = await _dbContext.ProductCategories.AnyAsync(c => c.Id == categoryId);

        return categoryExist;
    }

    public async Task<bool> CreateCategory(ProductCategory category)
    {
        await _dbContext.AddAsync(category);

        return await Save();
    }

    public async Task<bool> DeleteCategory(ProductCategory category)
    {
        _dbContext.Remove(category);

        return await Save();
    }

    public async Task<ICollection<ProductCategory>> GetCategories()
    {
        var categories = await _dbContext.ProductCategories.ToListAsync();

        return categories;
    }

    public async Task<ProductCategory> GetCategory(int id)
    {
        var category = await _dbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);

        if (category != null)
        {
            return category;
        }

        throw new Exception("Category was not found");
    }

    public async Task<bool> Save()
    {
        var save = await _dbContext.SaveChangesAsync();

        return save > 0;
    }

    public async Task<bool> UpdateCategory(ProductCategory category)
    {
        _dbContext.Update(category);

        return await Save();
    }
}
