using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs;
public class UserRoleDto
{
    public int UserId { get; set; } = default!; 
    public int RoleId { get; set; } = default!;
}
