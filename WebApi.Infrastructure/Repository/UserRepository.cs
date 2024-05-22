using FluentResults;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> AddUser(User newUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}