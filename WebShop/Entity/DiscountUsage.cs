namespace WebShop.Api.Entity;

public class DiscountUsage
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public int DiscountId { get; set; }
    public virtual Discount Discount { get; set; } = default!;

    public DateTime DiscountUsedDate { get; set; } = DateTime.UtcNow;
}
