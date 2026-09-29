// using ECommerceApp.Models;
namespace ECommerceApp.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; } 
    public  List<CartItem> cartItems = new List<CartItem>();
}

// - Cart: Id, UserId, CreatedAt