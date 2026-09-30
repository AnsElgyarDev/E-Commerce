using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class CartEndpoints
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/carts", async Task<Results<NotFound, Ok<List<CartDto>>>> 
        (IGenericRepository<Cart> repo) =>
        {
            var carts = await repo.GetAll();
            if (carts is null || !carts.Any()) return TypedResults.NotFound();

            var result = carts.Select(c => new CartDto
            {
                Id = c.Id,
                UserId = c.UserId
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/carts/{id:int}", async Task<Results<NotFound, Ok<CartDto>>> 
        (IGenericRepository<Cart> repo, int id) =>
        {
            var c = await repo.GetById(id);
            if (c is null) return TypedResults.NotFound();

            return TypedResults.Ok(new CartDto
            {
                Id = c.Id,
                UserId = c.UserId
            });
        });

        // Create
        app.MapPost("/carts", async (IGenericRepository<Cart> repo, CartDto dto) =>
        {
            var cart = new Cart
            {
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await repo.AddAsync(cart);
            await repo.saveChanges();

            return TypedResults.Created($"/carts/{cart.Id}");
        });

        // Update
        app.MapPut("/carts/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Cart> repo, int id, CartDto dto) =>
        {
            var cart = await repo.GetById(id);
            if (cart is null) return TypedResults.NotFound();

            cart.UserId = dto.UserId;

            repo.Update(cart);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/carts/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Cart> repo, int id) =>
        {
            var cart = await repo.GetById(id);
            if (cart is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        app.MapGet("Cart/{Id:int}/Items", async Task<Results<NotFound, Ok<List<CartItem>>>>
                  (ICartServices cartService, int Id) =>
        {
            var CartItems = await cartService.GetCartItems(Id);
            
            if(CartItems is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(CartItems);
        });
        
    }
}