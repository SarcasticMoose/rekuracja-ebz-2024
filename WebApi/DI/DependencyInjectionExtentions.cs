using Core.Serializer;
using WebApi.Configuration;

namespace WebApi.DI;

public static class DependencyInjectionExtentions
{
    public static void AddSerializer(this IServiceCollection sevices)
    {
        sevices.AddScoped<ISerializer, JsonSerializer>();
    }

    public static void AddSeedReader(this IServiceCollection services)
    {
        services.AddScoped<ISeedReader, SeedReader>();
    }
}