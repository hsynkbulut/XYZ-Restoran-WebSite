using Microsoft.AspNetCore.Mvc;

namespace OnlineRestaurantProject.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
