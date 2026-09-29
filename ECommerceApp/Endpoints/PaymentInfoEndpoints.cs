using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class PaymentInfoEndpoints
{
    public static void MapPaymentInfoEndpoints(this WebApplication app)
    {
        // Get All
        app.MapGet("/payments", async Task<Results<NotFound, Ok<List<PaymentInfoDto>>>> 
        (IGenericRepository<PaymentInfo> repo) =>
        {
            var payments = await repo.GetAll();
            if (payments is null || !payments.Any()) return TypedResults.NotFound();

            var result = payments.Select(p => new PaymentInfoDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                PaymentMethod = p.PaymentMethod,
                TransactionId = p.TransactionId,
                Status = p.Status
            }).ToList();

            return TypedResults.Ok(result);
        });

        // Get By Id
        app.MapGet("/payments/{id:int}", async Task<Results<NotFound, Ok<PaymentInfoDto>>> 
        (IGenericRepository<PaymentInfo> repo, int id) =>
        {
            var p = await repo.GetById(id);
            if (p is null) return TypedResults.NotFound();

            return TypedResults.Ok(new PaymentInfoDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                PaymentMethod = p.PaymentMethod,
                TransactionId = p.TransactionId,
                Status = p.Status
            });
        });

        // Create
        app.MapPost("/payments", async (IGenericRepository<PaymentInfo> repo, PaymentInfoDto dto) =>
        {
            var payment = new PaymentInfo
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                TransactionId = dto.TransactionId,
                Status = dto.Status
            };

            await repo.AddAsync(payment);
            await repo.saveChanges();

            return TypedResults.Created($"/payments/{payment.Id}");
        });

        // Update
        app.MapPut("/payments/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<PaymentInfo> repo, int id, PaymentInfoDto dto) =>
        {
            var payment = await repo.GetById(id);
            if (payment is null) return TypedResults.NotFound();

            payment.PaymentMethod = dto.PaymentMethod;
            payment.TransactionId = dto.TransactionId;
            payment.Status = dto.Status;

            repo.Update(payment);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });

        // Delete
        app.MapDelete("/payments/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<PaymentInfo> repo, int id) =>
        {
            var payment = await repo.GetById(id);
            if (payment is null) return TypedResults.NotFound();

            await repo.Delete(id);
            await repo.saveChanges();

            return TypedResults.NoContent();
        });
    }
}