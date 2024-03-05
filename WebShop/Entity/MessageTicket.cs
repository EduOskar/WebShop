using WebShop.Models.DTOs.MailDtos;
using WebShop.Models.DTOs;

namespace WebShop.Api.Entity;

public enum IsResolved
{
    NotResolved = 0,
    Resolved = 1,
}

public class MessageTicket
{
    public int Id { get; set; }

    public int? SupportId { get; set; }

    public virtual User? Support { get; set; }

    public int UserID { get; set; }

    public User User { get; set; } = default!;

    public IsResolved IsResolved { get; set; } = IsResolved.NotResolved;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    List<SupportMessages>? SupportMessages { get; set; } = default!;
}
