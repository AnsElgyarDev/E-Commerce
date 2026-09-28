using System.ComponentModel.DataAnnotations;
using ECommerceApp.Models;
namespace ECommerceApp.Dto;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public CartDto? Cart { get; set; }
    public List<OrderDto> Orders { get; set; } = new();
}