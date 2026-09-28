using System.ComponentModel.DataAnnotations;
using ECommerceApp.Models;
namespace ECommerceApp.Dto;

public class UserResponseDto
{
    public string Name { get; set; } = string.Empty;
    [RegularExpression(@"[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?(?:\.[A-Za-z0-9](?:[A-Za-z0-9-]{0,61}[A-Za-z0-9])?)*$")]
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Cart cart = new Cart();
    public List<Order> orders = new List<Order>();
}