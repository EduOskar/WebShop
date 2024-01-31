using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.PasswordManagement;
public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
}
