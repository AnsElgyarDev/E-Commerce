using ECommerceApp.Data;
using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Endpoint;

public static class CartEndpoints
{
    public static void MapCartEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/carts");

        // 1. Get Cart Details
        group.MapGet("/{id:int}", async Task<Results<NotFound, Ok<CartDto>>> (ICartServices cartService, int id) =>
        {
            var c = await cartService.GetCartById(id);
            if (c is null) return TypedResults.NotFound();

            return TypedResults.Ok(new CartDto
            {
                Id = c.Id,
                UserId = c.UserId
            });
        });

        // 2. Get Cart Items Only
        group.MapGet("/{id:int}/items", async Task<Results<NotFound, Ok<List<CartItem>>>> (ICartServices cartService, int id) =>
        {
            var cartItems = await cartService.GetCartItems(id);
            if (cartItems is null) return TypedResults.NotFound();

            return TypedResults.Ok(cartItems);
        });

        // 3. Add / Update Item in Cart
        group.MapPost("/{cartId:int}/items", async (AppDbContext db, CreateCartItemDto dto, int cartId) =>
        {
            var cartExists = await db.carts.AnyAsync(c => c.Id == cartId);
            if (!cartExists) return Results.NotFound($"Cart with ID {cartId} not found.");

            var existingItem = await db.cartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                db.cartItems.Add(new CartItem
                {
                    CartId = cartId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                });
            }

            await db.SaveChangesAsync();
            return Results.Ok(new { Message = "Item added to cart successfully" });
        });

        // 4. Delete Entire Cart
        group.MapDelete("/{id:int}", async Task<Results<NotFound, NoContent>> (IGenericRepository<Cart> repo, int id) =>
        {
            var cart = await repo.GetById(id);
            if (cart is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}