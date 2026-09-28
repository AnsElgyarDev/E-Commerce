namespace ECommerceApp.Dto;

public class CartItemDto
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public ProductDto ProductDto { get; set; } = new ProductDto();
    public CartDto carDto { get; set; } = new CartDto();
    public int Quantity { get; set; }
}