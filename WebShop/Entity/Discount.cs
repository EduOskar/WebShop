using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class Discount
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string DiscountCode { get; set; } = default!;

    public decimal DiscountPercentage { get; set; } = default!;

    public int DiscountQuantity { get; set; } = default!;

    public bool DiscountIsActive { get; set; } = false!;

}
