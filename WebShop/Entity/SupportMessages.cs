using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class SupportMessages
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int SupportMailId { get; set; }
    public SupportMail SupportMail { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Message { get; set; } = default!;

    public bool CurrentUser { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

}
