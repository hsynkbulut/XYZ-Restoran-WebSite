using Microsoft.AspNetCore.Mvc;
using OnlineRestaurantProject.Models;

namespace OnlineRestaurantProject.ViewComponents
{
    public class Authors : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var authorList = new List<SiteAuthors>
            {
                new SiteAuthors
                {
                    AuthorId = 1,
                    AuthorName = "Hüseyin Karabulut & Bayram Bayraktar",
                }
            };
            return View(authorList);
        }

    }
}
