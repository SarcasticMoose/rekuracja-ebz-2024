using FluentResults;

namespace WebApi.Infrastructure.Auth.Hashing;

public interface IHashService
{
    string Hash(string password);
    Result CheckPassword(string password,string hashedPassword);
}