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
    public int UserId { get; set; }
}
