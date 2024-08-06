using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IHttpClientFactory httpClientFactory, ILogger<ProductsController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        // Tüm ürünleri listelemek için bir eylem
        public IActionResult Index()
        {
            return View();
        }

        // Kategorileri listelemek için bir eylem
        public IActionResult ViewCategories()
        {
            return View();
        }

        // Kategori güncelleme formunu göstermek için bir eylem
        public IActionResult UpdateCategory()
        {
            return View();
        }

        // AJAX ile ürünleri almak için bir eylem
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7183/api/products/getall");
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadFromJsonAsync<List<Product>>();
                    return Json(products);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error fetching products: {errorContent}");
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                return Json(null);
            }
        }

        // AJAX ile kategorileri almak için bir eylem
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7258/api/categories/getall");
                if (response.IsSuccessStatusCode)
                {
                    var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
                    return Json(categories);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error fetching categories: {errorContent}");
                    return Json(null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching categories.");
                return Json(null);
            }
        }

        // Kategori güncelleme işlemini işlemek için bir eylem
        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7258/api/categories/update", updateCategoryDto);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Error updating category: {errorContent}");
                    return Json(new { success = false });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating category.");
                return Json(new { success = false });
            }
        }
    }

    // Ürün modeli
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
    }

    // Kategori modeli
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    // Kategori güncelleme DTO'su
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }
        public string NewCategoryName { get; set; }
    }
}
