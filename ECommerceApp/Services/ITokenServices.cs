using ECommerceApp.Models;

namespace ECommerceApp.Services;

public interface ITokenServices
{
    public Task<string> CreateToken(AppliactionUser user); 
}