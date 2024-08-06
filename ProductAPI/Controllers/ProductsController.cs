using Microsoft.AspNetCore.Mvc;
using ProductAPI.Model;
using ProductAPI.Service;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("GetAll")]
    public ActionResult<List<Product>> GetAll()
    {
        return _productService.GetProducts();
    }

}