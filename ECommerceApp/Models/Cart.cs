// using ECommerceApp.Models;
namespace ECommerceApp.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User user { get; set; } = new User();
    public DateTime CreatedAt { get; set; }
    public  List<CartItem> cartItems = new List<CartItem>();
}

// - Cart: Id, UserId, CreatedAt