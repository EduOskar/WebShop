using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public record PlaceOrderCommand
{
    [Required(ErrorMessage ="Billingadress is required")]
    public Adress BillingAdress { get; set; } = new();

    [Required(ErrorMessage = "Shippingadress is required")]
    public Adress ShippingAdress { get; set; } = new();

    public List<OrderItemDto> OrderItems { get; set; } = default!;

    public class Adress
    {
        [Required(ErrorMessage = "Address is required")]
        public string AddressLine { get; set; } = default!;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Post Code is required")]
        public string PostCode { get; set; } = default!;
    }
}
