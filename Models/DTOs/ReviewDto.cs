﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Content { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
