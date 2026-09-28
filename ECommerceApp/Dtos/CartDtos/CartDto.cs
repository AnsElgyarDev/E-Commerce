namespace ECommerceApp.Dto;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserResponseDto user { get; set; } = new UserResponseDto();
    public List<CartItemDto> cartItems = new List<CartItemDto>();
}