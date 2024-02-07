using OnlineRestaurantProject.Data;
using OnlineRestaurantProject.Models.Domain;
using OnlineRestaurantProject.Services.Abstract;

namespace OnlineRestaurantProject.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingCart GetCartById(int id)
        {
            return _context.Carts.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<ShoppingCart> GetCarts()
        {
            var cart = _context.Carts.OrderBy(x => x.ProductName).ToList();
            return cart;
        }

        public void CreateOrEditCart(ShoppingCart update)
        {
            UpdateCart(update);
        }

        public void UpdateCart(ShoppingCart cart)
        {
            var currentCart = _context.Carts
                .Where(x => x.ProductId == cart.ProductId)
                .FirstOrDefault();

            if (currentCart != null)
            {
                throw new Exception("Bu ürün sepette mevcut");
            }

            else
            {
                _context.Carts.Add(new ShoppingCart
                {
                    //Id = Guid.NewGuid(),
                    ProductId = cart.ProductId,
                    ProductName = cart.ProductName,
                    Price = cart.Price,
                    ProductImage = cart.ProductImage,
                });
            }

            _context.SaveChanges();
        }

        public double GetTotalAmount()
        {
            return _context.Carts.Sum(x => x.Price);
        }

        public void Delete(int id)
        {
            var cart = _context.Carts.Find(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
        }

        public void ClearCart()
        {
            var cartItems = _context.Carts.ToList();

            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();
        }

    }
}
