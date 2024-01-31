using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public enum DiscountStatus
{
    Active = 0,
    InActive = 1
}
public class Discount
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string DiscountCode { get; set; } = default!;

    public decimal DiscountPercentage { get; set; } = default!;

    public int DiscountsUsed { get; set; } = default!;

    public DiscountStatus IsActive { get; set; } = default!;

}
