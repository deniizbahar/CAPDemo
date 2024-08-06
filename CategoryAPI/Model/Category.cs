namespace CategoryAPI.Model;

public class Category
{
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}

public static class Categories
{
    public static List<Category> CategoryList = new()
    {
        new Category { CategoryId = 1, CategoryName = "Game Console" },
        new Category { CategoryId = 2, CategoryName = "Mobile Phone" },
    };
}