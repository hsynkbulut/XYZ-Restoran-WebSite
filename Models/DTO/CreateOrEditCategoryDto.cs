using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurantProject.Models.DTO
{
    public class CreateOrEditCategoryDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Boş bırakılamaz.")]
        public string CategoryName { get; set; }
    }
}
