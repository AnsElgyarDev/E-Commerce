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

        app.MapGet("/Users/{Id:int}", async Task<Results<NotFound, Ok<UserResponseDto>>> 
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

        app.MapPost("Users", async 
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

        app.MapPut("/Users/{id:int}", async Task<Results<NotFound, NoContent>> 
        (IGenericRepository<User> userRepo, int id, UpdateUserDto updateUserDto) =>
        {
            var user = await userRepo.GetById(id);
            if (user is null)
            {
                return TypedResults.NotFound();
            }

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.PasswordHash = updateUserDto.PasswordHash;

            userRepo.Update(user);
            await userRepo.saveChanges();

            return TypedResults.NoContent();
        });

        app.MapDelete("Users", async 
                   (IGenericRepository<User> userRepo,int userId) =>
        {
            await userRepo.Delete(userId);
            await userRepo.saveChanges();
            return TypedResults.NoContent();
        } );
        
    
    }
}