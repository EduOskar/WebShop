using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.MailDtos;
public class SupportMessagesDto
{
    public int Id { get; set; }

    public int SupportMailId { get; set; }
    public SupportMailDto SupportMail { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Message { get; set; } = default!;

    public bool CurrentUser { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
}
