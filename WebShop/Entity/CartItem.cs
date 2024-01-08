using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity;

public class CartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int CartId { get; set; }
    public virtual Cart? Cart { get; set; } 

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = default!;

    public decimal price => Product.Price;

    public decimal TotalPrice => Product.Price * Quantity;

    public int Quantity { get; set; }

}
