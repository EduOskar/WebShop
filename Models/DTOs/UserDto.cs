using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;

public class UserDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public decimal? Credit { get; set; }

    public string Adress { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string Phonenumber { get; set; } = default!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = default!;

}
