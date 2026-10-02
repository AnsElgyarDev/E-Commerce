using ECommerceApp.Dto;
using ECommerceApp.Helpers;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Distributed;

namespace ECommerceApp.Endpoint;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        // Get All With Caching
        app.MapGet("/categories", async Task<Ok<PagedList<CategoryDto>>> 
            (IGenericRepository<Category> repo, IDistributedCache cache, int pageNumber = 1, int pageSize = 10) =>
        {
            string cacheKey = $"categories_page_{pageNumber}_size_{pageSize}";

            var cachedData = await cache.GetAsync<PagedList<CategoryDto>>(cacheKey);
            if (cachedData is not null)
            {
                return TypedResults.Ok(cachedData); 
            }

            var categories = await repo.GetPagedAsync(pageNumber, pageSize);

            var dtoList = categories.Items.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            var result = new PagedList<CategoryDto>(
                dtoList, 
                categories.TotalCount, 
                categories.PageIndex, 
                categories.PageSize
            );

            await cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(15));
            return TypedResults.Ok(result);
        });

        app.MapGet("/categories/{id:int}", async Task<Results<NotFound, Ok<CategoryDto>>> 
            (IGenericRepository<Category> repo, IDistributedCache cache, int id) =>
        {
            string cacheKey = $"category_{id}";

            var cachedCategory = await cache.GetAsync<CategoryDto>(cacheKey);
            if (cachedCategory is not null)
            {
                return TypedResults.Ok(cachedCategory);
            }

            var c = await repo.GetById(id);
            if (c is null) return TypedResults.NotFound();

            var dto = new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            };

            await cache.SetAsync(cacheKey, dto, TimeSpan.FromHours(1));
            return TypedResults.Ok(dto);
        });

        // Create
        app.MapPost("/categories", async (IGenericRepository<Category> repo, CategoryDto dto) =>
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await repo.AddAsync(category);
            await repo.saveChanges();

            return TypedResults.Created($"/categories/{category.Id}");
        });

        // Update
        app.MapPut("/categories/{id:int}", async Task<Results<NotFound, NoContent>> 
            (IGenericRepository<Category> repo, IDistributedCache cache, int id, CategoryDto dto) =>
        {
            var category = await repo.GetById(id);
            if (category is null) return TypedResults.NotFound();

            category.Name = dto.Name;
            category.Description = dto.Description;

            await repo.Update(category);
            await repo.saveChanges();

            await cache.RemoveAsync($"category_{id}");

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/categories/{id:int}", async Task<Results<NotFound, NoContent>> 
            (IGenericRepository<Category> repo, IDistributedCache cache, int id) =>
        {
            var category = await repo.GetById(id);
            if (category is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            await cache.RemoveAsync($"category_{id}");

            return TypedResults.NoContent();
        });
    }
}