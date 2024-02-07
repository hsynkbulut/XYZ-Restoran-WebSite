using Microsoft.EntityFrameworkCore;
using OnlineRestaurantProject.Data;
using OnlineRestaurantProject.Models.Domain;
using OnlineRestaurantProject.Models.DTO;
using OnlineRestaurantProject.Services.Abstract;

namespace OnlineRestaurantProject.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product GetProductById(int id)
        {
            return _context.Products
                           .Where(x => x.Id == id)
                           .FirstOrDefault();
        }

        public List<ProductDto> GetProductDtos(GetProducts getProducts, string sorting)
        {
            var productsQuery = _context.Products
                .Include(x => x.CategoryFk)
                .Where(x => (getProducts.CategoryId <= 0 || getProducts.CategoryId == x.CategoryId));

            switch (sorting)
            {
                case "Fiyat Artan":
                    productsQuery = productsQuery.OrderByDescending(x => x.Price);
                    break;
                case "Fiyat Azalan":
                    productsQuery = productsQuery.OrderBy(x => x.Price);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(x => x.Name);
                    break;
            }

            var products = productsQuery.Select(x => new ProductDto
            {
                Id = x.Id,
                ProductName = x.Name,
                Price = x.Price,
                ImageUrl = x.ImageUrl,
                CategoryName = x.CategoryFk.Name,
            }).ToList();

            return products;
        }

        public void CreateOrEditProduct(CreateOrEditProductDto productDto)
        {
            if (productDto.Id == null)
            {
                CreateProduct(productDto);
            }
            else
            {
                UpdateProduct(productDto);
            }
        }

        private void CreateProduct(CreateOrEditProductDto createOrEditDto)
        {
            Product products = new Product()
            {
                //Id = Guid.NewGuid(),
                Name = createOrEditDto.ProductName,
                Price = createOrEditDto.Price,
                CategoryId = createOrEditDto.CategoryId,
                ImageUrl = createOrEditDto.ImageUrl,
            };

            _context.Products.Add(products);
            _context.SaveChanges();
        }

        private void UpdateProduct(CreateOrEditProductDto createOrEditDto)
        {
            var currentProduct = _context.Products
                .Where(x => x.Id == createOrEditDto.Id)
                .FirstOrDefault();

            if (currentProduct != null)
            {
                currentProduct.Name = createOrEditDto.ProductName;
                currentProduct.Price = createOrEditDto.Price;
                currentProduct.CategoryId = createOrEditDto.CategoryId;
                currentProduct.ImageUrl = createOrEditDto.ImageUrl;

                _context.Products.Update(currentProduct);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public int GetTotalProductCount()
        {
            return _context.Products.Count();
        }

        public List<Product> GetProducts()
        {
            var products = _context.Products
            .OrderBy(x => x.Name)
            .ToList();

            return products;
        }  
    }
}
