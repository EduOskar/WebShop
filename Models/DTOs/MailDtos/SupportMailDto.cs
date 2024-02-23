using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.MailDtos;


public enum IsResolved
{
    Unresolved = 0,
    Resolved = 1
}

public class SupportMailDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public UserDto? User { get; set; } = default!;

    public int? SupportId { get; set; }

    public IsResolved? IsResolved { get; set; }

    public string From { get; set; } = default!;

    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public List<SupportMessagesDto> Messages { get; set; } = new List<SupportMessagesDto>();
}
 