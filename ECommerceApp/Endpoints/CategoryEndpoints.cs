using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/categories", async Task<Results<NotFound, Ok<List<CategoryDto>>>> 
        (IGenericRepository<Category> repo) =>
        {
            var categories = await repo.GetAll();
            if (categories is null || !categories.Any()) return TypedResults.NotFound();

            var result = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/categories/{id:int}", async Task<Results<NotFound, Ok<CategoryDto>>> 
        (IGenericRepository<Category> repo, int id) =>
        {
            var c = await repo.GetById(id);
            if (c is null) return TypedResults.NotFound();

            return TypedResults.Ok(new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            });
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
        (IGenericRepository<Category> repo, int id, CategoryDto dto) =>
        {
            var category = await repo.GetById(id);
            if (category is null) return TypedResults.NotFound();

            category.Name = dto.Name;
            category.Description = dto.Description;

            repo.Update(category);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/categories/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Category> repo, int id) =>
        {
            var category = await repo.GetById(id);
            if (category is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}