using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebShop.Api.Controllers;

namespace WebShop.Api.Entity;



public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime PlacementTime { get; set; }

    public virtual int OrderStatusId { get; set; }
    public OrderStatus Status { get; set; } = default!;

    //ForeignKey for Customer
    public int UserId { get; set; }
    public User? User { get; set; }

    public List<OrderItem> OrderItems { get; set; } = default!;

}
