using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class DiscountDto
{
    public int Id { get; set; }

    public string DiscountCode { get; set; } = default!;

    public decimal DiscountPercentage { get; set; } = default!;

    public int DiscountQuantity { get; set; } = default!;

    public bool DiscountIsActive { get; set; } = false!;
}
