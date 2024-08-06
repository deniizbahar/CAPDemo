using ProductAPI.Model;

namespace ProductAPI.Service;

public interface IProductService
{
    List<Product> GetProducts();
    string UpdateCategoryName(UpdateCategoryNameDto updateCategoryNameDto);
}