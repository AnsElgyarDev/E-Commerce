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
    }
}