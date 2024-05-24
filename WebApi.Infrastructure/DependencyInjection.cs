using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Infrastructure.Auth;
using WebApi.Infrastructure.Auth.Hashing;
using WebApi.Infrastructure.Auth.Jwt;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Infrastructure;

public static class DependencyInjection
{
    public static void AddHashingService(
        this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();
    }
    
    public static void AddJwtService(
        this IServiceCollection services)
    {
        services.AddScoped<IJwtService,JwtService>();
    }
    public static void AddDatabaseContext(
        this IServiceCollection services, IHostApplicationBuilder builder)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(
                builder.Configuration.GetConnectionString("DefaultConnection"));
        });
    }
    
}