using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class CartItemEndpoints
{
    public static void MapCartItemEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/cart-items", async Task<Results<NotFound, Ok<List<CartItemDto>>>> 
        (IGenericRepository<CartItem> repo) =>
        {
            var items = await repo.GetAll();
            if (items is null || !items.Any()) return TypedResults.NotFound();

            var result = items.Select(i => new CartItemDto
            {
                Id = i.Id,
                CartId = i.CartId,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/cart-items/{id:int}", async Task<Results<NotFound, Ok<CartItemDto>>> 
        (IGenericRepository<CartItem> repo, int id) =>
        {
            var i = await repo.GetById(id);
            if (i is null) return TypedResults.NotFound();

            return TypedResults.Ok(new CartItemDto
            {
                Id = i.Id,
                CartId = i.CartId,
                ProductId = i.ProductId,
                Quantity = i.Quantity
            });
        });

        // Create
        app.MapPost("/cart-items", async (IGenericRepository<CartItem> repo, CartItemDto dto) =>
        {
            var item = new CartItem
            {
                CartId = dto.CartId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            await repo.AddAsync(item);
            await repo.saveChanges();

            return TypedResults.Created($"/cart-items/{item.Id}");
        });

        // Update
        app.MapPut("/cart-items/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<CartItem> repo, int id, CartItemDto dto) =>
        {
            var item = await repo.GetById(id);
            if (item is null) return TypedResults.NotFound();

            item.Quantity = dto.Quantity;
            item.ProductId = dto.ProductId;
            item.CartId = dto.CartId;

            repo.Update(item);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/cart-items/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<CartItem> repo, int id) =>
        {
            var item = await repo.GetById(id);
            if (item is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}