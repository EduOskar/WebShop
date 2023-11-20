using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public decimal Credit { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phonenumber { get; set; } = string.Empty;
        public int Role { get; set; }

        public int OrderId { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        //public ICollection<Review> Revíews { get; set; } = new List<Review>(); //Add in future
    }
}
