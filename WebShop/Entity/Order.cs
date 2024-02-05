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

    public DateTime PlacementTime { get; set; } = DateTime.UtcNow;

    public int OrderStatusId { get; set; }
    public virtual OrderStatus OrderStatus { get; set; } = default!;

    //ForeignKey for Customer
    public int UserId { get; set; }
    public User? User { get; set; }

    //foreginKey for warehouseWorkers
    public int? WareHouseWorkerId { get; set; }
    public virtual User? WarehouseWorker { get; set; }

    public List<OrderItem> OrderItems { get; set; } = default!;

}
