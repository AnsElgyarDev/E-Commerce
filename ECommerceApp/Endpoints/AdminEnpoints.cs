using ECommerceApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApp.Endpoint;

public static class AdminEnpoints
{
    public static void MapAdminEnpoints(this WebApplication app)
    {
        var adminGroup = app.MapGroup("/admin")
                            .RequireAuthorization("AdminOnly");
        
        adminGroup.MapGet("", () =>
        {
            return TypedResults.Ok("Hello, From Admin!");
        }).RequireAuthorization();
    
        app.MapPost("/admin/assign-role", async Task<Results<NotFound<string>, Ok<string>, BadRequest<IEnumerable<IdentityError>>>>
                   (string userId, UserManager<AppliactionUser> userManager) =>
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                 return TypedResults.NotFound("User not found");

            var result = await userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
                return TypedResults.Ok("User promoted to Admin successfully!");

            return TypedResults.BadRequest(result.Errors);
        }).RequireAuthorization("AdminOnly");
    }
}