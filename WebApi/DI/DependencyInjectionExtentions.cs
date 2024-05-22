using Core.Serializer;

namespace WebApi.DI;

public static class DependencyInjectionExtentions
{
    public static void AddSerializer(this IServiceCollection sevices)
    {
        sevices.AddScoped<ISerializer, JsonSerializer>();
    }
}