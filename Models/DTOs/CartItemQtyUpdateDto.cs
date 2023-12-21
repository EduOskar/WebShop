using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class CartItemQtyUpdateDto
{
    public int CartItemId { get; set; }
    public int Quantity { get; set; }
}
