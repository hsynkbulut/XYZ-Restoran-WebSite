using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurantProject.Models;
using OnlineRestaurantProject.Models.Domain;
using OnlineRestaurantProject.Models.DTO;
using OnlineRestaurantProject.Services.Abstract;
using System.Diagnostics;

namespace OnlineRestaurantProject.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IShoppingCartService _shoppingCartService;

		public HomeController(IProductService productService, ICategoryService categoryService, IShoppingCartService shoppingCartService)
		{
			_productService = productService;
			_categoryService = categoryService;
			_shoppingCartService = shoppingCartService;
		}

		public IActionResult Index(int categoryId = -1, string sortOrder = "", int page = 1, int pageSize = 10)
		{
			var categories = _categoryService.GetCategories();
			categories.Insert(0, new Category { Id = -1, Name = "Tümü" });

			var model = new HomeIndexViewModel()
			{
				PageNumber = page,
				PageSize = pageSize,
				TotalPages = (int)Math.Ceiling((double)_productService.GetTotalProductCount() / pageSize),
				CategoryId = categoryId,
				Categories = categories,
				Products = _productService.GetProductDtos(new GetProducts
				{
					CategoryId = categoryId,
				}, sortOrder)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList()
			};
			model.Sorting = sortOrder;

			return View(model);
		}

        [Authorize(Roles = "Kullanici")]
        [HttpPost]
		public IActionResult ShoppingCart(int productId)
		{
			var product = _productService.GetProductById(productId);

			if (product != null)
			{
				var productInCart = new ShoppingCart
				{
					ProductId = product.Id,
					ProductName = product.Name,
					Price = product.Price,
					ProductImage = product.ImageUrl
				};

				try
				{
					_shoppingCartService.CreateOrEditCart(productInCart);
				}
				catch (InvalidOperationException ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}

			return RedirectToAction("Index");
		}

        [Authorize(Roles = "Kullanici")]
        public IActionResult ShoppingCart()
		{
			var cartItems = _shoppingCartService.GetCarts();

			var model = new ShoppingCartItemViewModel
			{
				Cart = cartItems
			};

			return View(model);
		}

        [Authorize(Roles = "Kullanici")]
        [HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
		{
			_shoppingCartService.Delete(id);
			return RedirectToAction("ShoppingCart"); //Sepet
        }

        [Authorize(Roles = "Kullanici")]
        [HttpPost]
		public IActionResult Payment() //Odeme()
		{
			double totalAmount = _shoppingCartService.GetTotalAmount();
			_shoppingCartService.ClearCart();

			return RedirectToAction("PaymentPost", new { totalAmount = totalAmount });
		}

        [Authorize(Roles = "Kullanici")]
        [HttpGet]
		public IActionResult PaymentPost(decimal totalAmount)
		{
			var viewModel = new ShoppingCartItemViewModel
			{
				TotalAmount = totalAmount,
			};

			return View(viewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
