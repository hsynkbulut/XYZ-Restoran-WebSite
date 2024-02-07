using OnlineRestaurantProject.Models.Domain;
using OnlineRestaurantProject.Models.DTO;

namespace OnlineRestaurantProject.Services.Abstract
{
    public interface IProductService
    {
        List<ProductDto> GetProductDtos(GetProducts getProducts, string sorting);
        List<Product> GetProducts();
        void CreateOrEditProduct(CreateOrEditProductDto productDto);
        void Delete(int id);
        int GetTotalProductCount();
        Product GetProductById(int id);
    }
}
