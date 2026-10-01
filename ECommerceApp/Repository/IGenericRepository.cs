using System.Linq.Expressions;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ECommerceApp.Repository;

public interface IGenericRepository<T> where T : class
{
    public Task<List<T>> GetAll();
    public Task<T?> GetById(int id);
    public Task AddAsync(T entity);
    public Task Delete(int UserId);
    public Task Update(T entity);
    public Task saveChanges();
    public Task<PagedList<T>> GetPagedAsync(int pageNumber = 1, int pageSize = 10);
    public  Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate);
}