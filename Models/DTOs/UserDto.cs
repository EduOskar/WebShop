using System.ComponentModel.DataAnnotations;
using WebShop.Models.DTOs.MailDtos;

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
    public string Phonenumber { get; set; } = default!;

    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    public bool? HasActiveSupportMail()
    {
        return SupportMails?.Any(mail => !mail.IsResolved.HasValue) ?? false;
    }

    // Or for display purposes
    public string? ActiveSupportMailDisplay => HasActiveSupportMail() ?? false ? "Yes" : "No";

    public IEnumerable<SupportMailDto>? SupportMails { get; set; }

}

