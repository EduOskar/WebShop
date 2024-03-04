using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;

public class SupportMessages
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string UserName { get; set; } = default!;

    public string Message { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

}
