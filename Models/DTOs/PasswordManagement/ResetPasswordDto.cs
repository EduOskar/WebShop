using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.PasswordManagement;
public class ResetPasswordDto
{
    public string Email { get; set; } = default!;
 
    public string Token { get; set; } = default!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [DataType(DataType.Password)]
    [Compare($"Password", ErrorMessage ="The password and confirmation password do not match")]
    public string ConfirmPassword { get; set; } = default!;
}
