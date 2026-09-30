using System.Net.Http.Headers;
using ECommerceApp.Dto;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Endpoint;

public static class UserEndpoints
{

    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("Register", () =>
        {
            
        });

        app.MapPost("Login", () =>
        {
            
        });
    }
}