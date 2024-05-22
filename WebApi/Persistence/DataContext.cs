using Core.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<User>()
            .HasKey(x => x.Id);

        modelBuilder
            .Entity<User>()
            .HasOne<Gender>(x => x.Gender)
            .WithOne(g => g.User)
            .HasForeignKey<Gender>();
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Gender> Genders => Set<Gender>();
}