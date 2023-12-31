﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime PlacementTime { get; set; }

    //ForeignKey for Customer
    public int UserId { get; set; }
    public virtual User? User { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; } = default!;
}
