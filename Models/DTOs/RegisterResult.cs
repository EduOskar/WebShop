using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class RegisterResult
{
    public bool Successfull { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
}
