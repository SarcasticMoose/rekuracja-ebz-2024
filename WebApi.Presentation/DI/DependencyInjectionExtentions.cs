using System.Text.Json;
using System.Text.Json.Serialization;
using Core;
using Core.FileReader;
using Core.Serializer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi.Configuration;
using WebApi.Infrastructure.Utils;
using JsonSerializer = Core.Serializer.JsonSerializer;

namespace WebApi.DI;

public static class DependencyInjectionExtentions
{
    public static void AddFileReader(this IServiceCollection services)
    {
        services.AddScoped<IFileReader, FileReader>();
    }
    

    public static void AddDatabaseLuncher(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseLuncher, DatabaseLuncher>();
    }
    
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
    }
}