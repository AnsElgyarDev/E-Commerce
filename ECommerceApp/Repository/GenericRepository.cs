using System.Linq.Expressions;
using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace ECommerceApp.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task Delete(int USerId)
    {
        var entity = await GetById(USerId);

        _context.Set<T>().Remove(entity!);
    }

    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int Id)
    {
        return await _context.Set<T>().FindAsync(Id);
    }

    public async Task saveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<PagedList<T>> GetPagedAsync(int pageNumber = 1, int pageSize = 10)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize switch
        {
            < 1 => 10,
            > 50 => 50, 
            _ => pageSize
        };

        var totalCount = await _dbSet.CountAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (pageNumber > totalPages && totalPages > 0)
        {
            pageNumber = totalPages;
        }

        var items = await _dbSet
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }
}