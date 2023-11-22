using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public decimal? Credit { get; set; }
    public string Adress { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Phonenumber { get; set; } = default!;
    public int Role { get; set; } = default!;
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
