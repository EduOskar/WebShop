using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int? UserId { get; set; }
        public User? User { get; set; }


        public ICollection<CartItem> CartItem { get; set;} = new List<CartItem>();
    }
}
