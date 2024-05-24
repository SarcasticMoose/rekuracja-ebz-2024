using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Entities;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Application.Repository;

public class UserRepository(DataContext dataContext) : IUserRepository
{
    public async Task<IList<User>?> GetAllUsersAsync(CancellationToken ct)
    {
        return await dataContext.Users
            .Include(x => x.UserDetails)
            .ToListAsync(cancellationToken: ct);
    }

    public async Task<User?> GetUserByIdAsync(int id,CancellationToken ct = default!)
    {
        return await dataContext.Users
            .Where(x => x.Id == id)
            .Include(x => x.UserDetails)
            .FirstOrDefaultAsync(cancellationToken: ct);
    }

    public async Task<User?> GetUserByNameAsync(string username, CancellationToken ct)
    {
        return await dataContext.Users
            .Where(x => x.Username == username)
            .Include(x => x.UserDetails)
            .FirstOrDefaultAsync(cancellationToken: ct);
    }

    public async Task AddUserAsync(User newUser,CancellationToken ct = default!)
    { 
        await dataContext.Users.AddAsync(entity: newUser, cancellationToken: ct);
    }

    public void DeleteUser(User user)
    {
        dataContext.Users.Remove(entity: user);
    }

    public void UpdateUser(User user)
    {
        dataContext.Users.Update(user);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dataContext.SaveChangesAsync();
    }
}