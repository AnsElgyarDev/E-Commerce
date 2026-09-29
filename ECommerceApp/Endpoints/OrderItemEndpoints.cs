using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class OrderItemEndpoints
{
    public static void MapOrderItemEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/order-items", async Task<Results<NotFound, Ok<List<OrderItemDto>>>> 
        (IGenericRepository<OrderItem> repo) =>
        {
            var items = await repo.GetAll();
            if (items is null || !items.Any()) return TypedResults.NotFound();

            var result = items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/order-items/{id:int}", async Task<Results<NotFound, Ok<OrderItemDto>>> 
        (IGenericRepository<OrderItem> repo, int id) =>
        {
            var i = await repo.GetById(id);
            if (i is null) return TypedResults.NotFound();

            return TypedResults.Ok(new OrderItemDto
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            });
        });

        // Create
        app.MapPost("/order-items", async (IGenericRepository<OrderItem> repo, OrderItemDto dto) =>
        {
            var item = new OrderItem
            {
                OrderId = dto.OrderId,
                ProductId = dto.ProductId,
                UnitPrice = dto.UnitPrice,
                Quantity = dto.Quantity
            };

            await repo.AddAsync(item);
            await repo.saveChanges();

            return TypedResults.Created($"/order-items/{item.Id}");
        });

        // Update
        app.MapPut("/order-items/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<OrderItem> repo, int id, OrderItemDto dto) =>
        {
            var item = await repo.GetById(id);
            if (item is null) return TypedResults.NotFound();

            item.Quantity = dto.Quantity;
            item.UnitPrice = dto.UnitPrice;

            repo.Update(item);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/order-items/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<OrderItem> repo, int id) =>
        {
            var item = await repo.GetById(id);
            if (item is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}