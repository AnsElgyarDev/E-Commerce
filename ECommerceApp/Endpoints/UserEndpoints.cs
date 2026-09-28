using System.Net.Http.Headers;
using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ECommerceApp.Endpoint;

public static class UserEndpoints
{

    public static void MapUserEndpoint(this WebApplication app)
    {
        app.MapGet("/Users", async Task<Results<NotFound, Ok<List<UserResponseDto>>>> 
        (IGenericRepository<User> user) =>
        {
            var users = await user.GetAll();

            if(users is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            }).ToList());

        });
    }
}