using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Endpoint;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth");

        // 1. Register
        group.MapPost("/register", async Task<Results<BadRequest<AuthReponseDto>, Ok<AuthReponseDto>>> 
            (UserManager<AppliactionUser> userManager, RegisterDto dto) =>
        {
            var userExists = await userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
            {
                return TypedResults.BadRequest(new AuthReponseDto(false, "Email is already registered."));
            }

            var user = new AppliactionUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return TypedResults.BadRequest(new AuthReponseDto(false, errors));
            }

            return TypedResults.Ok(new AuthReponseDto(true, "User registered successfully!"));
        });

        // 2. Login
        group.MapPost("/login", async Task<Results<BadRequest<AuthReponseDto>, Ok<AuthReponseDto>>> 
            (UserManager<AppliactionUser> userManager, ITokenServices tokenService, LoginDto dto) =>
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user is null)
            {
                return TypedResults.BadRequest(new AuthReponseDto(false, "Invalid email or password."));
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!isPasswordValid)
            {
                return TypedResults.BadRequest(new AuthReponseDto(false, "Invalid email or password."));
            }

            var token = await tokenService.CreateToken(user);

            return TypedResults.Ok(new AuthReponseDto(true, "Login successful", token));
        });
    }
}