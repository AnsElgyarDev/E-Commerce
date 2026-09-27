namespace ECommerceApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Product> products = new List<Product>();
}

// Category: Id, Name, Description
