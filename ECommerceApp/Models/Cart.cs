namespace ECommerceApp.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}

// - Cart: Id, UserId, CreatedAt