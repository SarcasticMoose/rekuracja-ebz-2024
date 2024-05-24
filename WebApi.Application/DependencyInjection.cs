using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Repository;

namespace WebApi.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGenderRepository, GenderRepository>();
    }
}