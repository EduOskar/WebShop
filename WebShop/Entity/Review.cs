using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity;

public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(40)]
    public string? Title { get; set; } = default!;

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [Required]
    [StringLength(500)]
    public string? Content { get; set; } = default!;

    // Foreign Key
    public int? ProductId { get; set; }
    public virtual Product? Product { get; set; }

    // Foreign Key
    public int? UserId { get; set; }
    public virtual User? User { get; set; }
}
