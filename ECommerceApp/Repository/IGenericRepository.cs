using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ECommerceApp.Repository;

public interface IGenericRepository<T> where T : class
{
    public Task<List<T>> GetAll();
    public Task<T?> GetById(int id);
    public Task AddAsync(T entity);
    public Task Delete(int UserId);
    public void Update(T entity);
    public Task saveChanges();
}