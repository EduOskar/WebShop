using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class QuantityCheckDto
{
    public int OrderItemId { get; set; }

    public int QuantityCheck { get; set; }
}
