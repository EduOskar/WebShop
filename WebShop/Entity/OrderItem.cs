using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OrderId { get; set; }
    public virtual Order? Order { get; set; }

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public string? ProductName;

    public string? ProductDescription;

    public int Quantity { get; set; }

    public int QuantityCheck { get; set; }
}
 