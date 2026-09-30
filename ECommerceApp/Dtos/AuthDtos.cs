namespace ECommerceApp.Dto;

public record RegisterDto(string Email, string FullName, string Password);
public record LoginDto(string Email, string Password);
public record AuthReponseDto(bool isSuccess, string Message, string? token = null);