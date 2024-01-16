using System.ComponentModel.DataAnnotations;
using WebShop.Models.Validation;

namespace WebShop.Models.DTOs;

public class UserDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public decimal? Credit { get; set; }

    public string Adress { get; set; } = default!;

    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Email address is required.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|se|net|org|edu|gov|uk|co|us|info|io)$", ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Phone number is required.")]
    [ValidPhoneNumber(ErrorMessage ="Invalid Phonenumber")]
    public string Phonenumber { get; set; } = default!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = default!;

}
