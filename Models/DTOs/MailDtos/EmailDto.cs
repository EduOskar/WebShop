using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.MailDtos;
public class EmailDto
{
    public string From { get; set; } = default!;

    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;
}
