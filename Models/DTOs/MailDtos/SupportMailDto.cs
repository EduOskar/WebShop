using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.DTOs.MailDtos;


public enum IsAnswered
{
    NotAnswered = 0,
    Answered = 1
}

public enum IsSupport
{
    Support = 0,
    User = 1,
}

public class SupportMailDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public UserDto? User { get; set; } = default!;

    public int? SupportId { get; set; }

    public UserDto? Support { get; set; }

    public IsSupport? IsSupport { get; set; }

    public IsAnswered? IsAnswered { get; set; }

    public string From { get; set; } = default!;

    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public DateTime DateTime { get; set; } = DateTime.UtcNow;

}
 