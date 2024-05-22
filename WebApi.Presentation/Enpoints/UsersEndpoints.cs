using Carter;

namespace WebApi.Enpoints;

public class UsersEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", () =>
        {
            throw new NotImplementedException();
        });
        
        app.MapGet("/users/{id}", (int id) =>
        {
            throw new NotImplementedException();
        });

        app.MapPost("/users", () =>
        {
            throw new NotImplementedException();
        });
        
        app.MapPut("/users/{id}", (int id) =>
        {
            throw new NotImplementedException();
        });
        
        app.MapDelete("/users", () =>
        {
            throw new NotImplementedException();
        });

        app.MapPatch("/users/change-city/{id}", (int id) =>
        {
            throw new NotImplementedException();
        });
    }
}