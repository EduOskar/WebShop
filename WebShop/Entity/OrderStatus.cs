using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class OrderStatus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? New { get; set; }
    public DateTime? Created { get; set; } = DateTime.UtcNow;

    public string? Sent { get; set; }
    public DateTime? OnRoute { get; set; } = DateTime.UtcNow;

    public string? Delivered { get; set; }
    public DateTime? Arrived { get; set; } = DateTime.UtcNow;

    public string? Cancelled { get; set; }
    public DateTime? Removed { get; set; } = DateTime.UtcNow;

}
