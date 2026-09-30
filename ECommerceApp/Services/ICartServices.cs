using ECommerceApp.Models;

namespace ECommerceApp.Services;

public interface ICartServices
{
    public Task<List<CartItem>> GetCartItems(int cartId);
    public Task<Cart> GetCartById(int id);
}