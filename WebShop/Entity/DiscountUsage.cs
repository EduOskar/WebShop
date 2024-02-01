using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class DiscountUsage
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public int DiscountId { get; set; }
    public virtual Discount Discount { get; set; } = default!;

    public DateTime DiscountUsedDate { get; set; } = DateTime.UtcNow;
}
