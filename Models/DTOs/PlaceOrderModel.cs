using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class PlaceOrderModel
{
    public PlaceOrderCommand Command { get; set; } = new();
}
