namespace ProductAPI.Model;

public class Product
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? CategoryName { get; set; }
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}

public static class Products
{
    public static List<Product> ProductList = new() {
        new Product { ProductId = 1, ProductName = "Sony Playstation 2", CategoryName = "Game Console", Quantity = 5, UnitPrice = 299 },
        new Product { ProductId = 2, ProductName = "Sony Playstation 3", CategoryName = "Game Console", Quantity = 3, UnitPrice = 399 },
        new Product { ProductId = 3, ProductName = "Sony Playstation 4", CategoryName = "Game Console", Quantity = 10, UnitPrice = 499 },
        new Product { ProductId = 4, ProductName = "Sony Playstation 5", CategoryName = "Game Console", Quantity = 1, UnitPrice = 599 },
        new Product { ProductId = 5, ProductName = "Apple iPhone 12 Pro", CategoryName = "Mobile Phone", Quantity = 1, UnitPrice = 899 },
        new Product { ProductId = 6, ProductName = "Apple iPhone 13 Pro", CategoryName = "Mobile Phone", Quantity = 1, UnitPrice = 999 },
        new Product { ProductId = 7, ProductName = "Apple iPhone 12 Pro Max", CategoryName = "Mobile Phone", Quantity = 1, UnitPrice = 1299 },
    };
}