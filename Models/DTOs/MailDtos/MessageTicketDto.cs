using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.MailDtos;

public enum IsResolved
{
    NotResolved = 0,
    Resolved = 1,
}

public class MessageTicketDto
{
    public int Id { get; set; }

    public int? SupportId { get; set; }

    public UserDto? Support { get; set; }

    public int UserID { get; set; }

    public UserDto User { get; set; } = default!;

    public IsResolved IsResolved { get; set; }

    public DateTime CreatedDate { get; set; }

    public List<SupportMessagesDto>? SupportMessages { get; set; } = default!;
}
