using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

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
    public List<CartItem> cartItems = new List<CartItem>();
    public List<OrderItem> orderItems = new List<OrderItem>();
    public bool isDeleted { get; set; } = false;
}

// Product: Id, Name, CategoryId, Price, Stock, isDeleted