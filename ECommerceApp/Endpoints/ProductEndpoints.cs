using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/products", async Task<Results<NotFound, Ok<List<ProductDto>>>> 
        (IGenericRepository<Product> repo) =>
        {
            var products = await repo.GetAll();
            if (products is null || !products.Any()) return TypedResults.NotFound();

            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/products/{id:int}", async Task<Results<NotFound, Ok<ProductDto>>> 
        (IGenericRepository<Product> repo, int id) =>
        {
            var p = await repo.GetById(id);
            if (p is null) return TypedResults.NotFound();

            return TypedResults.Ok(new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                Price = p.Price,
                Stock = p.Stock
            });
        });

        // Create
        app.MapPost("/products", async (IGenericRepository<Product> repo, ProductDto dto) =>
        {
            var product = new Product
            {
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await repo.AddAsync(product);
            await repo.saveChanges();

            return TypedResults.Created($"/products/{product.Id}");
        });

        // Update
        app.MapPut("/products/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Product> repo, int id, ProductDto dto) =>
        {
            var product = await repo.GetById(id);
            if (product is null) return TypedResults.NotFound();

            product.Name = dto.Name;
            product.CategoryId = dto.CategoryId;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await repo.Update(product);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/products/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Product> repo, int id) =>
        {
            var product = await repo.GetById(id);
            if (product is null || product.isDeleted) return TypedResults.NotFound();

            product.isDeleted = true;
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}