using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurantProject.Models.DTO;
using OnlineRestaurantProject.Services.Abstract;

namespace OnlineRestaurantProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(string sorting)
        {
            var products = _productService.GetProductDtos(new GetProducts { CategoryId = -1 }, sorting);
            return View(products);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var model = new CreateOrEditProductViewModel()
            {
                Categories = _categoryService.GetCategories(),
                Product = new CreateOrEditProductDto()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreateOrEditProductViewModel input)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateOrEditProduct(input.Product);
                return RedirectToAction("Index");
            }

            // Eğer model geçerli değilse, hata durumunda aynı view'i tekrar göster
            input.Categories = _categoryService.GetCategories();
            return View(input);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var product = _productService.GetProductById(id);

            var productDto = new CreateOrEditProductDto
            {
                Id = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
            };

            var model = new CreateOrEditProductViewModel()
            {
                Categories = _categoryService.GetCategories(),
                Product = productDto
            };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(CreateOrEditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateOrEditProduct(viewModel.Product);
                return RedirectToAction("Index");
            }

            viewModel.Categories = _categoryService.GetCategories();
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult ViewDetails(int id)
        {
            var f = new ProductDto();
            var product = _productService.GetProductById(id);
            f.Id = product.Id;
            f.ProductName = product.Name;
            f.Price = product.Price;
            f.ImageUrl = product.ImageUrl;

            return View(f);
        }
    }
}
