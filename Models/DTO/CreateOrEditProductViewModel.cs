using OnlineRestaurantProject.Models.Domain;

namespace OnlineRestaurantProject.Models.DTO
{
    public class CreateOrEditProductViewModel
    {
        public CreateOrEditProductDto Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
