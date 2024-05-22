using System.Text.Json;
using Core;
using Core.FileReader;
using Core.Serializer;
using WebApi.Configuration;
using WebApi.Infrastructure.Utils;
using JsonSerializer = Core.Serializer.JsonSerializer;

namespace WebApi.DI;

public static class DependencyInjectionExtentions
{
    public static void AddSerializer(this IServiceCollection services)
    {
        services.AddScoped<ISerializer>(sp => new JsonSerializer(
            new JsonSerializerOptions()
        {
            Converters = { new DateTimeConverter() }
        }));
    }

    public static void AddFileReader(this IServiceCollection services)
    {
        services.AddScoped<IFileReader, FileReader>();
    }

    public static void AddDatabaseLuncher(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseLuncher, DatabaseLuncher>();
    }
}