using WebShop.Api.Entity;

namespace WebShop.Api.Repositories.Contracts;

public interface IDiscountRepository
{
    Task<Discount> CreateDiscount(Discount discount);
    Task<bool> DeleteDiscount(int discountId);

    Task<bool> UpdateDiscountStatus(int discountId, Discount discount, bool discountOff); 
}
