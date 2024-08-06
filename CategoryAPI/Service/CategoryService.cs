using CategoryAPI.Data;
using CategoryAPI.Model;
using DotNetCore.CAP;

namespace CategoryAPI.Service;

public class CategoryService: ICategoryService, ICapSubscribe
{
    private readonly ICapPublisher _capPublisher;
    private readonly ApiContext _apiContext;
    private List<Category> _categories = Categories.CategoryList;

    public CategoryService(ICapPublisher capPublisher, ApiContext apiContext)
    {
        _capPublisher = capPublisher;
        _apiContext = apiContext;
    }

    public List<Category> GetCategories()
    {
        return _categories;
    }

    public async Task UpdateCategoryName(Category category)
    {
        await using var transaction = _apiContext.Database.BeginTransaction(_capPublisher, autoCommit: true);

        var categoryEntity = _categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);

        if (categoryEntity == null)
        {
            await transaction.RollbackAsync();
            throw new Exception("Category Not Found");
        }
        
        var updateCategoryNameDto = new UpdateCategoryNameDto
        {
            OldCategoryName = categoryEntity!.CategoryName,
            NewCategoryName = category.CategoryName
        };
        
        await _capPublisher.PublishAsync("UpdateProductCategoryName", updateCategoryNameDto, callbackName: "categoryUpdateDone");
    }

    [CapSubscribe("categoryUpdateDone")]
    public Task UpdateComplete(string message)
    {
        return Task.FromResult(message);
    }
}