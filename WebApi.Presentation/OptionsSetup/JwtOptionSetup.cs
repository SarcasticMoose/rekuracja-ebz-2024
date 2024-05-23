using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using WebApi.Infrastructure.Auth.Jwt;

namespace WebApi.OptionsSetup;

public class JwtOptionSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}