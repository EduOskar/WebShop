﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity;

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
    public int Qty { get; set; } = default!;

    public int CategoryId { get; set; }

    public virtual ProductCategory? Category { get; set; }

    public ICollection<Review>? Reviews { get; set; }
}
