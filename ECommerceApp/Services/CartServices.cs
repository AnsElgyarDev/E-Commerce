using ECommerceApp.Models;
using ECommerceApp.Repository;

namespace ECommerceApp.Services;

public class CartServices : ICartServices
{
    private readonly IGenericRepository<Cart> _cartRepo;
    public CartServices(IGenericRepository<Cart> cartRepo)
    {
        this._cartRepo = cartRepo;
    }

    public async Task<Cart> GetCartById(int id)
    {
        var cart =  await _cartRepo.GetById(id);

        if(cart is null)
        {
            return null!;
        }
        
        return cart;
    }

    public async Task<List<CartItem>> GetCartItems(int cartId)
    {
        var cart = await _cartRepo.GetById(cartId);
        
        if(cart is null)
        {
            return null!;
        }

        return cart.cartItems;
    }
}