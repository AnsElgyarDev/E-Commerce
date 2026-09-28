namespace ECommerceApp.Dto;

public class OrderDto
{
 public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserResponseDto user { get; set; } = new UserResponseDto();
    public string Status { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public PaymentInfoDto paymentInfo = new PaymentInfoDto();
    public List<OrderItemDto> orderItems = new List<OrderItemDto>();         
}