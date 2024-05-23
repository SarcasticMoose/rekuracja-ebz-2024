using FluentResults;
using WebApi.Infrastructure.Auth.Hashing;

namespace WebApi.Infrastructure.Auth;

public class HashService : IHashService
{
    private const int workFactor = 13;
    
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, workFactor);
    }

    public Result CheckPassword(string password,string passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash)
            ? Result.Ok()
            : Result.Fail("Verification Failed");
    }
}