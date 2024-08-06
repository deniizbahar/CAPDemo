using CategoryAPI.Model;
using CategoryAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace CategoryAPI.Controllers;

[Route("api/[controller]")]
public class CategoriesController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public ActionResult<List<Category>> GetCategories()
    {
        return _categoryService.GetCategories();
    }

    [HttpPost]
    public async Task<ActionResult> UpdateCategoryName([FromBody]Category category)
    {
        await _categoryService.UpdateCategoryName(category);
        return Ok();
    }
}