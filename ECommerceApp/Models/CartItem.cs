using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models;

public class CartItem
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public Product product = new Product();
    public Cart cart = new Cart();
    [Range(0, 10000)]
    public int Quantity { get; set; }
}

// CartItem: Id, CartId, ProductId, Quantity