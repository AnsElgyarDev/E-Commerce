namespace ECommerceApp.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

// CartItem: Id, CartId, ProductId, Quantity