using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class DiscountDto
{
    public int Id { get; set; }

    public string discountCode { get; set; } = default!;

    public decimal discountPercentage { get; set; } = default!;

    public int DiscountQuantity { get; set; } = default!;
}
