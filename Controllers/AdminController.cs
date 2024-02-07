using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurantProject.Data;
using OnlineRestaurantProject.Models;
using OnlineRestaurantProject.Services.Abstract;

namespace OnlineRestaurantProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<IdentityUser> userManager, ICategoryService categoryService, IProductService productService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _productService = productService;
            _context = context;
        }

        public IActionResult Index()
        {
            var userList = _userManager.Users.ToList();
            var userCount = _userManager.Users.Count();
            var categoryCount = _categoryService.GetCategories().Count();
            var productCount = _productService.GetProducts().Count();

            ViewBag.UserCount = userCount;
            ViewBag.CategoryCount = categoryCount;
            ViewBag.ProductCount = productCount;

            return View(userList);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Genel");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error deleting user.");
                return View("Genel", _userManager.Users.ToList());
            }
        }
    }
}
