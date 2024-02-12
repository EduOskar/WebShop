using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IDiscountRepository
{
    string GenerateUniqueCode(int length = 7, string prefix = "DISC-");

    Task RecordDiscountUsage(int userId, string discountCode);

    Task<bool> CanUserUseDiscount(int userId, string DiscountCode);

    Task<List<Discount>> GetDiscounts();

    Task<Discount> GetDiscount(string discountCode);

    Task<Discount> GetDiscountById(int discountId);

    Task<bool> ApplyDiscountOnProduct(int productId, int discountId);

    Task<List<ProductsDiscount>> GetProductDiscounts();

    Task<bool> CreateDiscount(Discount discountCreate);

    Task<bool> DeleteDiscount(Discount discountDelete);

    Task<bool> UpdateDiscount(Discount discount);

    Task<bool> UpdateDiscountStatus(Discount discount);

    Task<bool> Save();
}
