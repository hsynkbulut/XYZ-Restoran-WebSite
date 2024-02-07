using OnlineRestaurantProject.Models.Domain;
using OnlineRestaurantProject.Models.DTO;

namespace OnlineRestaurantProject.Services.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        void CreateOrEditCategory(CreateOrEditCategoryDto update);
        void Delete(int id);
        Category GetCategoryById(int id);
    }
}
