﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageURL { get; set; } = default!;
    public decimal Price { get; set; }
    public int Qty { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}
