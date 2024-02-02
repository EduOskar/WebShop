using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;

public enum DiscountStatus
{
    Active = 0,
    InActive = 1
}

public class DiscountDto
{
    public int Id { get; set; }

    public string? DiscountCode { get; set; }

    public decimal DiscountPercentage { get; set; } = default!;

    public int DiscountsUsed { get; set; } = default!;

    public DiscountStatus IsActive { get; set; } = default!;
}
