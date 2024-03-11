using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class CheckoutDto
{
    [Required(ErrorMessage = "Firstname is required.")]
    public string FirstName { get; set; } = default!;

    [Required(ErrorMessage = "Lastname is required.")]
    public string LastName { get; set; } = default!;

    [Required(ErrorMessage = "Phonenumber line is required.")]
    public string PhoneNumber { get; set; } = default!;

    [Required(ErrorMessage = "Email line is required.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; } = default!;

    [Required(ErrorMessage = "Address line is required.")]
    public string AdressLine { get; set; } = default!;

    [Required(ErrorMessage = "Postal code is required.")]
    public string PostCode { get; set; } = default!;

}
