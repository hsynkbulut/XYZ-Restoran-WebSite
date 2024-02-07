using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurantProject.Models.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string CategoryName { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
