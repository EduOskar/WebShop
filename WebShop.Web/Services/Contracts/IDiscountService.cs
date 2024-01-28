using Microsoft.AspNetCore.Mvc;
using WebShop.Models.DTOs;

namespace WebShop.Web.Services.Contracts;

public interface IDiscountService
{
    Task<ActionResult<DiscountDto>> GetDiscount(int discountId);

    Task<ActionResult<List<DiscountDto>>> GetDiscounts();

    Task<ActionResult<DiscountDto>> CreateDiscount(DiscountDto discountCreate);

    Task<DiscountDto> ApplyDiscount(int userId, string discountCode);

    Task<ActionResult<bool>> UpdateDiscount(DiscountDto discountUpdate);
}
