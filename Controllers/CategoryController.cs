using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineRestaurantProject.Models.DTO;
using OnlineRestaurantProject.Services.Abstract;

namespace OnlineRestaurantProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories();

            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            if (TempData.ContainsKey("SweetAlert"))
            {
                ViewBag.SweetAlertMessage = TempData["SweetAlert"];
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(CreateOrEditCategoryDto input)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateOrEditCategory(input);
                return RedirectToAction("Index");
            }
            return View(input);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            var model = new CreateOrEditCategoryDto
            {
                Id = category.Id,
                CategoryName = category.Name
            };
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(CreateOrEditCategoryDto updateDto)
        {
            _categoryService.CreateOrEditCategory(updateDto);
            return RedirectToAction("Index");
        }
    }
}
