using CategoryAPI.Model;

namespace CategoryAPI.Service;

public interface ICategoryService
{
    List<Category> GetCategories();
    Task UpdateCategoryName(Category category);
    Task UpdateComplete(string message);
}