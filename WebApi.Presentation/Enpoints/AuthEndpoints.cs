using Carter;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Application.Repository;
using WebApi.Domain.Models;
using WebApi.Infrastructure.Auth.Hashing;
using WebApi.Infrastructure.Auth.Jwt;

namespace WebApi.Enpoints;

public class AuthEndpoints() : CarterModule("/auth")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async Task<Results<Ok<string>,NotFound,UnauthorizedHttpResult>>(IUserRepository userRepository, IHashService hashService, IJwtService jwtService,LoginUserDto userLoginDto, CancellationToken ct) =>
        {
            var user = await userRepository.GetUserByNameAsync(userLoginDto.Username, ct);
            
            if (user is null)
            {
                return TypedResults.NotFound();
            }
        
            Result correctPassword = hashService.CheckPassword(userLoginDto.Password, user.Password);
        
            if (correctPassword.IsFailed)
            {
                return TypedResults.Unauthorized();
            }
            
            return TypedResults.Ok(jwtService.Generate(user));
        });
    }
}