using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Serializer;

public static class DependencyInjection
{
    public static void AddSerializer(this IServiceCollection services)
    {
        services.AddScoped<ISerializer>(sp => new JsonSerializer(
            new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                Converters = { new DateTimeConverter() },
                PropertyNameCaseInsensitive = true 
            }));
    }
}