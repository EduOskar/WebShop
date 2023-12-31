﻿using System.ComponentModel.DataAnnotations.Schema;
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
    public virtual Product? Product { get; set; }
    public virtual decimal Price { get; set; }

    [MaxLength(200)]
    public int Qty { get; set; }

}
