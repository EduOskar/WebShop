using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebShop.Api.Entity;


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
public class Discount
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public string? DiscountCode { get; set; } = default!;

    public decimal DiscountPercentage { get; set; } = default!;

    public int DiscountsUsed { get; set; } = default!;

    public DiscountStatus IsActive { get; set; } = default!;

    public DiscountType DiscountType { get; set; } = default!;

    public ICollection<ProductsDiscount>? ProductDiscounts { get; set; }
}
