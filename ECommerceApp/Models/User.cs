using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [RegularExpression(@"")]
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Cart cart = new Cart();
    public List<Order> orders = new List<Order>();
}
