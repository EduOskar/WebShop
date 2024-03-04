using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public enum IsSupport
{
    Support = 0,
    Answered = 1,
}
public enum IsAnswered
{
    NotAnswered = 0,
    Answered = 1
}
public class SupportMail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; } = default!;

    public int? SupportId { get; set; }
    public virtual User? Support { get; set; }
     
    public IsSupport IsSupport { get; set; }

    public IsAnswered? IsAnswered { get; set; }

    public string From { get; set; } = default!;

    public string To { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public DateTime DateTime { get; set; } = DateTime.UtcNow;

}
