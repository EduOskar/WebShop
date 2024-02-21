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
    public string SenderId { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
