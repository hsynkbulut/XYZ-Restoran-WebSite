using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurantProject.Models.DTO
{
    public class GetProducts
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
