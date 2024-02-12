using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebShop.Api.Entity;

public enum ProductStatus
{
    [EnumMember(Value = "Active")]
    Active,

    [EnumMember(Value = "Inactive")]
    Inactive
}

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(500)]
    public string Name { get; set; } = default!;

    [MaxLength(5000)]
    public string Description { get; set; } = default!;

    [StringLength(1000)]
    public string ImageURL { get; set; } = default!;

    [MaxLength(100000)]
    public decimal Price { get; set; } = default!;

    [MaxLength(500)]
    public int Quantity { get; set; } = default!;

    public ProductStatus Status { get; set; }

    public int CategoryId { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public ICollection<Review>? Reviews { get; set; }

    public ICollection<ProductsDiscount>? ProductsDiscount { get; set; }
}
