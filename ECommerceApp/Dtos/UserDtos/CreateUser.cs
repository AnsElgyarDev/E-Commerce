using System.ComponentModel.DataAnnotations;
namespace ECommerceApp.Dto;

public class CreateUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [RegularExpression(@"")]
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
}