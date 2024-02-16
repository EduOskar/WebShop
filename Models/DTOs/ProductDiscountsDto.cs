using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class ProductDiscountsDto
{
    public int Id { get; set; }

    public virtual int ProductId { get; set; }

    public int DiscountId { get; set; }
}
