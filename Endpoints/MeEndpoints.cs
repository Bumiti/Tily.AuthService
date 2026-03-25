using System.Security.Claims;

namespace Tily.AuthService.Endpoints;

public static class MeEndpoints
{
    public static IEndpointRouteBuilder MapMeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").RequireAuthorization();

        group.MapGet("/me", (ClaimsPrincipal user) =>
        {
            return Results.Ok(new
            {
                userId = user.FindFirstValue(ClaimTypes.NameIdentifier),
                userName = user.FindFirstValue(ClaimTypes.Name),
                email = user.FindFirstValue(ClaimTypes.Email),
                roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value).ToArray(),
                claims = user.Claims.Select(x => new { x.Type, x.Value })
            });
        });

        return app;
    }
}

