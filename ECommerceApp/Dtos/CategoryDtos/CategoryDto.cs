namespace ECommerceApp.Dto;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<ProductDto> products = new List<ProductDto>();
}