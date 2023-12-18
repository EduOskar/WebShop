using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public record PlaceOrderCommand
{
    public Adress BillingAdress { get; set; } = new();
    public Adress ShippingAdress { get; set; } = new();

    public List<OrderItemDto> OrderItems { get; set; } = default!;

    public class Adress
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string AdressLine { get; set; } = default!;

        [Required]
        public string City { get; set; } = default!;

        [Required]
        public string PostCode { get; set; } = default!;
    }
}
