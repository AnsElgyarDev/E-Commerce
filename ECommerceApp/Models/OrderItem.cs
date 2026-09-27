namespace ECommerceApp.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

// OrderItem: Id, OrderId, ProductId, Quantity, UnitPrice

