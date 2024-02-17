namespace WebShop.Api.Entity;

public class SupportMail
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; } = default!;

    public int? SupportId { get; set; }
    public User? Support { get; set; }

    public string From { get; set; } = default!;

    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;
}
