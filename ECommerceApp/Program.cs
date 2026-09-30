using ECommerceApp.Data;
using ECommerceApp.Endpoint;
using ECommerceApp.Middleware;
using ECommerceApp.Models;
using ECommerceApp.Repository;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddIdentity<AppliactionUser, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
}


app.MapGet("/", () =>
{
    return TypedResults.Redirect("/Scalar/V1");
}).ExcludeFromDescription();

app.MapUserEndpoints();
app.MapProductEndpoints();
app.MapCategoryEndpoints();
app.MapCartEndpoints();
app.MapOrderEndpoints();
app.MapPaymentInfoEndpoints();
app.Run();
