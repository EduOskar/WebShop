using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Api.Entity
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();
        [MaxLength(200)]
        public int Qty { get; set; }

    }
}
