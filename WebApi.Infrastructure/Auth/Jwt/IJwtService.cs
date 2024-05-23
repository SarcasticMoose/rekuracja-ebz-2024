
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Auth.Jwt;

public interface IJwtService
{
    string Generate(User user);
}