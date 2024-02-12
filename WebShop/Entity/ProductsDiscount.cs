using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class ProductsDiscount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public virtual int ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public int DiscountId { get; set; }
    public Discount Discount { get; set; } = default!;

}
