using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class LoginResult
{
    public bool Succesful { get; set; }
    public string? Error { get; set; }
    public string? Token { get; set; }
}
