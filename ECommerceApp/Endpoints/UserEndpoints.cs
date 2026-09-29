using System.Net.Http.Headers;
using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Endpoint;

public static class UserEndpoints
{

    public static void MapUserEndpoint(this WebApplication app)
    {
        app.MapGet("/Users", async Task<Results<NotFound, Ok<List<UserResponseDto>>>> 
        (IGenericRepository<User> userRepo) =>
        {
            var users = await userRepo.GetAll();

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

        app.MapGet("/Users/Id", async Task<Results<NotFound, Ok<UserResponseDto>>> 
        (IGenericRepository<User> userRepo, int Id) =>
        {
            var user = await userRepo.GetById(Id);

            if(user is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email   
            });
        });

        app.MapPost("Users/", async 
        (IGenericRepository<User> userRepo, CreateUserDto createUserDto) =>
        {
            await userRepo.AddAsync(new User
            {
                Name= createUserDto.Name,
                PasswordHash = createUserDto.PasswordHash
            });

            await userRepo.saveChanges();

            return TypedResults.Created();
        });

        app.MapPut("Users", async (IGenericRepository<User> userRepo, UpdateUserDto updateUserDto) =>
        {
            userRepo.Update(new User
            {
                Id = updateUserDto.Id,
                Name = updateUserDto.Name,
                Email = updateUserDto.Email,
                PasswordHash = updateUserDto.PasswordHash
            });
    
            return TypedResults.NoContent();
        });

        app.MapDelete("Users", async 
                   (IGenericRepository<User> userRepo,[FromBody] User user) =>
        {
            userRepo.Delete(user);
            return TypedResults.NoContent();
        } );
        
    
    }
}