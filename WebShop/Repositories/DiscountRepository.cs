using WebShop.Api.Entity;
using WebShop.Api.Repositories.Contracts;

namespace WebShop.Api.Repositories;

public class DiscountRepository : IDiscountRepository
{
    public Task<Discount> CreateDiscount(Discount discount)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDiscount(int discountId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDiscountStatus(int discountId, Discount discount, bool discountOff)
    {
        throw new NotImplementedException();
    }
}
