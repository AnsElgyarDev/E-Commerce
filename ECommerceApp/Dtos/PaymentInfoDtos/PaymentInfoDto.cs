namespace ECommerceApp.Dto;

public class PaymentInfoDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int TransactionId { get; set; }
    public string Status { get; set; } = string.Empty; 
    public OrderDto order { get; set; } = new OrderDto();
}