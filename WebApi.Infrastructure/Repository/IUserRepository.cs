
using FluentResults;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Repository;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<Result>AddUser(User newUser);
    Task<Result> DeleteUser(User user);
    Task<Result> UpdateUser(User user);
}