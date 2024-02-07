using System.ComponentModel.DataAnnotations;
using OnlineRestaurantProject.Extensions.CustomValidators;

namespace OnlineRestaurantProject.Models.DTO
{
    public class CreateOrEditProductDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Ürün Adı boş bırakılamaz.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Kategori ID boş bırakılamaz.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Fiyat boş bırakılamaz.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Geçerli bir fiyat giriniz.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Ürün görsel linki boş bırakılamaz.")]
        [ImageFormat(ErrorMessage = "Geçersiz resim formatı. Desteklenen formatlar: png, jpg, jpeg, gif, webp, bmp")]
        public string ImageUrl { get; set; }
    }
}
