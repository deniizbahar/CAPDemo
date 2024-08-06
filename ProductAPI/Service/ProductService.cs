using DotNetCore.CAP;
using ProductAPI.Model;

namespace ProductAPI.Service;

public class ProductService: IProductService, ICapSubscribe
{
    private static List<Product> _products = Products.ProductList;
    public List<Product> GetProducts()
    {
        return _products;
    }
    
    [CapSubscribe("UpdateProductCategoryName")]
    public string UpdateCategoryName(UpdateCategoryNameDto updateCategoryNameDto)
    {
        var products = _products.Where(x => x.CategoryName == updateCategoryNameDto.OldCategoryName).ToList();
        products.ForEach(product =>
        {
            product.CategoryName = updateCategoryNameDto.NewCategoryName;
        });

        return "Successfully Updated.";
    }
}