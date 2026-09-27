using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category category { get; set; } = new Category();
    [Range(5, 1000000)]
    public decimal Price{ get; set; }
    public decimal Stock{ get; set; }
}

// Product: Id, Name, CategoryId, Price, Stock