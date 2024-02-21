using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class SupportMessages
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int SupportMailId { get; set; }

    public SupportMail SupportMail { get; set; } = default!;

    public string SenderId { get; set; } = default!;

    public string Content { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
