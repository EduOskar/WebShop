using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;


public class OrderDto
{
    public int Id { get; set; }
    
    public DateTime PlacementTime { get; set; }

    public int OrderStatusId { get; set; }

    public OrderStatusDto OrderStatus { get; set; } = default!;

    public int UserId { get; set; }

    public int? WareHouseWorkerId { get; set; }

    public UserDto? WarehouseWorker { get; set; } = default!;

    public List<OrderItemDto> OrderItems { get; set; } = default!;

}
