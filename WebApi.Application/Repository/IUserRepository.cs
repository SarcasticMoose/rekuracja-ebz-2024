
using WebApi.Infrastructure.Entities;

namespace WebApi.Application.Repository;

public interface IUserRepository
{
    Task<IList<User>?> GetAllUsersAsync(CancellationToken ct);
    Task<User?> GetUserByIdAsync(int id,CancellationToken ct);
    Task<User?> GetUserByNameAsync(string username, CancellationToken ct);
    Task AddUserAsync(User newUser,CancellationToken ct);
    void DeleteUser(User user);
    void UpdateUser(User user);
    Task<int> SaveChangesAsync();
}