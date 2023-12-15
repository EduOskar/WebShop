using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class CurrentUser
{
    public bool IsAuthenticated { get; set; }
    public string UserName { get; set; } = default!;
    public Dictionary<string, string> Claims { get; set; } = default!;
}
