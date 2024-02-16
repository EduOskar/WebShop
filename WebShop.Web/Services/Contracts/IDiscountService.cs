using Microsoft.AspNetCore.Mvc;
using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IDiscountService
{
    Task<DiscountDto> GetDiscount(string discountCode);

    Task<List<DiscountDto>> GetDiscounts();

    Task<DiscountDto> CreateDiscount(DiscountDto discountCreate);

    Task<bool> ApplyDiscount(int userId, string discountCode);

    Task<bool> ApplyDiscountOnProduct(int productId, int discountId);

    Task<bool> ActivateProductDiscounts();

    Task<bool> UpdateDiscount(DiscountDto discountUpdate);

    Task<bool> EmailDiscounts(int discountId);

    Task<bool> RemoveProductDiscounts();
}
