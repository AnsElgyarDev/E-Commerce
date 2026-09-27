namespace ECommerceApp.Models;

public class Order
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
}

// Order: Id, UserId, CreatedAt, Status, TotalPrice