using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurantProject.Models.Domain
{
    [Table("Cart")]
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string ProductName { get; set; }

        [Required, MinLength(1)]
        public int Count { get; set; }
        public string ProductImage { get; set; }
        public double Price { get; set; }
    }
}
