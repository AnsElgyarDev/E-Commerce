using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;

namespace ECommerceApp.Services;

public class ProdcutServices : IProdcutServices
{
    private readonly IGenericRepository<Product> _productRepository;

    public ProdcutServices(IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> GetProductById(int Id)
    {
        var product = await _productRepository.GetById(Id);

        if(product is null)
        {
            return null!;
        }

        return product;  
    }
}