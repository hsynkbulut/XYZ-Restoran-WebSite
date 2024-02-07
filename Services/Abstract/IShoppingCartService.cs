using OnlineRestaurantProject.Models.Domain;

namespace OnlineRestaurantProject.Services.Abstract
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetCarts();
        void CreateOrEditCart (ShoppingCart update);
        void Delete(int id);
        double GetTotalAmount();
        void ClearCart();
        ShoppingCart GetCartById(int id);
    }
}
