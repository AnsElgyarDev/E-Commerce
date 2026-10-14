using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;

namespace ECommerceApp.Services;

public interface IProdcutServices
{
    public  Task<Product> GetProductById(int Id);
}
/*
    List of anything in pages 
    not just products so it should be in the Helpers 
*/