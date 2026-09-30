using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/orders", async Task<Results<NotFound, Ok<List<OrderDto>>>> 
        (IGenericRepository<Order> repo) =>
        {
            var orders = await repo.GetAll();
            if (orders is null || !orders.Any()) return TypedResults.NotFound();

            var result = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalPrice = o.TotalPrice
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/orders/{id:int}", async Task<Results<NotFound, Ok<OrderDto>>> 
        (IGenericRepository<Order> repo, int id) =>
        {
            var o = await repo.GetById(id);
            if (o is null) return TypedResults.NotFound();

            return TypedResults.Ok(new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalPrice = o.TotalPrice
            });
        });

        // Create
        app.MapPost("/orders", async (IGenericRepository<Order> repo, OrderDto dto) =>
        {
            var order = new Order
            {
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                TotalPrice = dto.TotalPrice
            };

            await repo.AddAsync(order);
            await repo.saveChanges();

            return TypedResults.Created($"/orders/{order.Id}");
        });

        // Update Status
        app.MapPut("/orders/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Order> repo, int id, OrderDto dto) =>
        {
            var order = await repo.GetById(id);
            if (order is null) return TypedResults.NotFound();

            order.Status = dto.Status;
            order.TotalPrice = dto.TotalPrice;

            await repo.Update(order);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/orders/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<Order> repo, int id) =>
        {
            var order = await repo.GetById(id);
            if (order is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}