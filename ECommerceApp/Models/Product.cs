namespace ECommerceApp.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Price{ get; set; }
    public decimal Stock{ get; set; }
}

// Product: Id, Name, CategoryId, Price, Stock