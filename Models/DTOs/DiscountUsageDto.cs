﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class DiscountUsageDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DiscountId { get; set; }
}
