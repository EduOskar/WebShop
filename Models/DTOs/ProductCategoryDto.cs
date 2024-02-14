using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;

public class ProductCategoryDto
{
    public int Id { get; set; }

    public string? Name { get; set; } = default!;

    public string IconCSS { get; set; } = default!;
}
