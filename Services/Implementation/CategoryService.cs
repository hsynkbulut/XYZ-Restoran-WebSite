using OnlineRestaurantProject.Data;
using OnlineRestaurantProject.Models.Domain;
using OnlineRestaurantProject.Models.DTO;
using OnlineRestaurantProject.Services.Abstract;

namespace OnlineRestaurantProject.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<Category> GetCategories()
        {
            var categories = _context.Categories
            .OrderBy(x => x.Name)
            .ToList();

            return categories;
        }

        public void CreateOrEditCategory(CreateOrEditCategoryDto update)
        {
            if (!update.Id.HasValue)
            {
                CreateCategory(update);
            }
            else
            {
                UpdateCategory(update);
            }
        }

        private void CreateCategory(CreateOrEditCategoryDto input)
        {
            _context.Categories.Add(new Category
            {
                Name = input.CategoryName,
            });

            _context.SaveChanges();
        }

        private void UpdateCategory(CreateOrEditCategoryDto input)
        {
            var currentCategory = _context.Categories
                .Where(x => x.Id == input.Id.Value)
                .FirstOrDefault();

            if (currentCategory != null)
            {
                currentCategory.Name = input.CategoryName;
                _context.Categories.Update(currentCategory);
                _context.SaveChanges();
            }
        }
        
        public void Delete(int id)
        {
            var categories = _context.Categories.Find(id);

            if (categories != null)
            {
                _context.Categories.Remove(categories);
                _context.SaveChanges();
            }
        }
        
    }
}
