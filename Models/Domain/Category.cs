using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineRestaurantProject.Models.Domain
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kategori adı boş bırakılamaz.")]
        public string Name { get; set; }
    }
}
