using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurantProject.Models.Domain
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string ProductDescription { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category CategoryFk { get; set; }
        public string ImageUrl { get; set; }
    }
}
