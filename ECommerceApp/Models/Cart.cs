namespace ECommerceApp.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User user { get; set; } = new User();
    public DateTime CreatedAt { get; set; }
}

// - Cart: Id, UserId, CreatedAt