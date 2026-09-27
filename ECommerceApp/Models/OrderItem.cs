namespace ECommerceApp.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public Order order = new Order();
    public Product product= new Product();
}

// OrderItem: Id, OrderId, ProductId, Quantity, UnitPrice

