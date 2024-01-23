using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class UpdateOrderStatusRequest
{
    public int OrderId { get; set; }
    public OrderStatusType Status { get; set; }
}
