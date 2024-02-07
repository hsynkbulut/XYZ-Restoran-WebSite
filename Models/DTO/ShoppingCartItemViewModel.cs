using OnlineRestaurantProject.Models.Domain;

namespace OnlineRestaurantProject.Models.DTO
{
    public class ShoppingCartItemViewModel
    {
        public List<ShoppingCart> Cart { get; set; } //ESKİ İSMİ "public List<Sepet> Getir { get; set; }"
        public decimal TotalAmount { get; set; }
    }
}
