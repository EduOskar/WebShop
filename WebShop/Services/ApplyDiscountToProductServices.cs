using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;

namespace WebShop.Api.Services;

public class ApplyDiscountToProductServices
{
    private readonly ApplicationDbContext _dbContext;

    public ApplyDiscountToProductServices(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ApplyDiscountsOnProducts()
    {
        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var productsWithDiscounts = await _dbContext.Products
                    .Select(p => new
                    {
                        Product = p,
                        Discount = _dbContext.ProductDiscounts
                            .Where(d => d.ProductId == p.Id && d.Discount.DiscountType == Entity.DiscountType.ProductSpecific && d.Discount.IsActive == DiscountStatus.Active)
                            .Select(d => d.Discount.DiscountPercentage)
                            .FirstOrDefault() 
                    })
                    .ToListAsync();

                foreach (var pd in productsWithDiscounts)
                {
                    if (pd.Discount > 0) 
                    {
                        pd.Product.DiscountedPrice = pd.Product.Price * pd.Discount;
                    }
                }

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
