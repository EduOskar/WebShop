using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public record PlaceOrderCommand
{
    public Adress BillingAdress { get; set; } = new();

    public class Adress
    {
        public string Name { get; set; } = default!;

        public string AdressLine { get; set; } = default!;

        public string City { get; set; } = default!;

        public string PostCode { get; set; } = default!;
    }
}
