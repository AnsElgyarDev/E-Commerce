using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Dto;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public CategoryDto category { get; set; } = new CategoryDto();

    [Range(5, 1000000)]
    public decimal Price{ get; set; }
    public decimal Stock{ get; set; }
    public List<CartItemDto> cartItems = new List<CartItemDto>();
    public List<OrderItemDto> orderItems = new List<OrderItemDto>();    
}