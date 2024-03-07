using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.Api.Entity;



public class SupportMessages
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int TicketId { get; set; }
    public virtual MessageTicket Ticket { get; set; } = default!;

    public string UserName { get; set; } = default!;

    public string Message { get; set; } = default!;

    public DateTime CreatedAt { get; set; }

}
