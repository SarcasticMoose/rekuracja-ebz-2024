using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(eb =>
        {
            eb
                .HasKey(x => x.Id);
            eb.HasOne<UserDetails>(x => x.UserDetails)
                .WithOne(x => x.User)
                .HasForeignKey<UserDetails>(x => x.UserId);
        });
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserDetails>(eb =>
            {
                eb
                    .HasKey(x => x.Id);
                eb
                    .HasOne<Gender>(x => x.Gender)
                    .WithMany(g => g.Users);
            });
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Gender> Genders => Set<Gender>();
}