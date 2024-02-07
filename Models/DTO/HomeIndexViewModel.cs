using OnlineRestaurantProject.Models.Domain;

namespace OnlineRestaurantProject.Models.DTO
{
    public class HomeIndexViewModel
    {
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }
        public List<ProductDto> Products { get; set; }
        public string Sorting { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
