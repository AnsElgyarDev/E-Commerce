using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ECommerceApp.Repository;

public interface IGenericRepository<T> where T : class
{
    public Task<List<T?>> GetAll();
    public Task<T?> GetById(int id);
    public Task<T?> AddAsync();
    public void Delete();
    public void Update();
}