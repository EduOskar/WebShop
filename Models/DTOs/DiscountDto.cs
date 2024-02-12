using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;

public enum DiscountType
{
    [EnumMember(Value = "TotalaPrice")]
    TotalPrice = 0,

    [EnumMember(Value = "ProductSpecific")]
    ProductSpecific = 1,
}
public enum DiscountStatus
{
    [EnumMember(Value = "Active")]
    Active = 0,

    [EnumMember(Value = "InActive")]
    InActive = 1
}

public class DiscountDto
{
    public int Id { get; set; }

    public string? DiscountCode { get; set; }

    public decimal DiscountPercentage { get; set; } = default!;

    public int DiscountsUsed { get; set; } = default!;

    public DiscountStatus IsActive { get; set; } = default!;

    public DiscountType DiscountType { get; set; } = default!;
}
