using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebShop.Api.Entity;

public class User :  IdentityUser<int>
{
    new public string Email { get; set; } = default!;

    public override string? UserName { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public decimal? Credit { get; set; }

    public string Adress { get; set; } = default!;

    new public string PhoneNumber { get; set; } = default!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = default!;

    public int Role { get; set; } = default!;

    public ICollection<Order>? Orders { get; set; }

    public ICollection<Review>? Reviews { get; set; }

}
