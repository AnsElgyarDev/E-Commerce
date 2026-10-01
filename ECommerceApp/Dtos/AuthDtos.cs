namespace ECommerceApp.Dto;

public class RegisterDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

public record LoginDto(string Email, string Password);
public record AuthReponseDto(bool isSuccess, string Message, string? token = null);