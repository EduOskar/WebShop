using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebShop.Api.Data;
using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly Random _random = new Random();
    private readonly ApplicationDbContext _dbContext;

    public DiscountRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private bool IsCodeExists(string code)
    {
        return _dbContext.Discounts
            .Any(dc => dc.DiscountCode == code);
    }

    public string GenerateUniqueCode(int length = 7, string prefix = "DISC-")
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string newCode;

        do
        {
            newCode = prefix + new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
        while (IsCodeExists(newCode));

        return newCode;
    }

    public async Task<bool> CreateDiscount(Discount discountCreate)
    {

        await _dbContext.AddAsync(discountCreate);

        return await Save();
    }

    public async Task<bool> DeleteDiscount(Discount discountDelete)
    {
        _dbContext.Remove(discountDelete);

        return await Save();
    }

    public async Task<Discount> GetDiscount(string discountCode)
    {
        var discount = await _dbContext.Discounts
            .AsNoTracking()
            .FirstOrDefaultAsync(dc => dc.DiscountCode == discountCode);

        if (discount != null)
        {
            return discount;
        }

        throw new Exception("Entity Not found");
    }

    public async Task<Discount> GetDiscountById(int discountId)
    {
        var discount = await _dbContext.Discounts
            .AsNoTracking()
            .FirstOrDefaultAsync(dc => dc.Id == discountId);

        if (discount != null)
        {
            return discount;
        }

        throw new Exception("Entity Not found");
    }


    public async Task<List<Discount>> GetDiscounts()
    {
        var discounts = await _dbContext.Discounts
            .AsNoTracking()
            .ToListAsync();

        if (discounts != null)
        {
            return discounts;
        }

        throw new Exception("Did not manage to fetch all discounts");
    }

    public async Task<bool> Save()
    {
        var saved = await _dbContext.SaveChangesAsync();

        return saved > 0;
    }

    public async Task<bool> UpdateDiscount(Discount discount)
    {
        if (discount.IsActive == DiscountStatus.InActive)
        {
            var productsWithDiscount = await _dbContext.ProductDiscounts
                .Include(p => p.Product)
                .Where(pd => pd.DiscountId == discount.Id)
                .ToListAsync();

            foreach (var product in productsWithDiscount)
            {
                product.Product.DiscountedPrice = null;
            }

            _dbContext.RemoveRange(productsWithDiscount);
        }

        _dbContext.Update(discount);

        return await Save();
    }

    public Task<bool> UpdateDiscountStatus(Discount discount)
    {
        throw new NotImplementedException();
    }

    public async Task RecordDiscountUsage(int userId, string discountCode)
    {
        var discount = await _dbContext.Discounts.FirstOrDefaultAsync(dc => dc.DiscountCode == discountCode);
        if (discount == null)
        {
            return;
        }
        var discountUsage = new DiscountUsage
        {
            UserId = userId,
            DiscountId = discount.Id
        };

        _dbContext.DiscountUsages.Add(discountUsage);

        await Save();
    }

    public async Task<bool> CanUserUseDiscount(int userId, string discountCode)
    {

        var discount = await _dbContext.Discounts.FirstOrDefaultAsync(dc => dc.DiscountCode == discountCode);

        if (discount == null)
        {
            return false;
        }

        var discountFalse = await _dbContext.DiscountUsages.AnyAsync(du => du.UserId == userId && du.DiscountId == discount.Id);

        return !discountFalse;
    }

    public async Task<bool> ApplyDiscountOnProduct(int productId, int discountId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        var discount = await _dbContext.Discounts.FirstOrDefaultAsync(x => x.Id == discountId);

        if (discount != null && product != null)
        {
            var productDiscount = new ProductsDiscount
            {
                DiscountId = discountId,
                Discount = discount,
                ProductId = productId,
                Product = product
            };


            await _dbContext.AddAsync(productDiscount);

            await Save();

            return true;
        }

        return false;
    }

    public async Task<List<ProductsDiscount>> GetProductDiscounts()
    {
        var productDiscounts = await _dbContext.ProductDiscounts
            .Include(u => u.Product)
            .Include(d => d.Discount)
            .ToListAsync();

        return productDiscounts;
    }

    public async Task<bool> RemoveProductDiscounts()
    {
        var productdiscounts = await _dbContext.ProductDiscounts.ToListAsync();

        var productIdsWithDiscounts = productdiscounts.Select(pd => pd.ProductId).ToList();

        var productsToUpdate = await _dbContext.Products
            .Where(p => productIdsWithDiscounts.Contains(p.Id))
            .ToListAsync();

        foreach (var product in productsToUpdate)
        {
            product.DiscountedPrice = null;
        }

        _dbContext.RemoveRange(productdiscounts);

        return await Save();
    }
}
