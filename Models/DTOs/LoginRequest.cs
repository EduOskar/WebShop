using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class LoginRequest
{
    [Required]
    public string UserName { get; set; } = default!;

    [Required] 
    public string Password { get; set; } = default!;

    public bool RememberMe { get; set; }
}
