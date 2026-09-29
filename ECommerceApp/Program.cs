using ECommerceApp.Data;
using ECommerceApp.Endpoint;
using ECommerceApp.Middleware;
using ECommerceApp.Repository;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

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
app.MapCartItemEndpoints();
app.MapOrderEndpoints();
app.MapOrderItemEndpoints();
app.MapPaymentInfoEndpoints();
app.Run();
