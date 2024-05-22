using Core.Models;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(eb =>
        {
            eb.HasKey(x => x.Id);
            eb.HasOne<GenderEntity>(x => x.Gender)
                .WithOne(g => g.User)
                .HasForeignKey<GenderEntity>();
            
        });
        
        modelBuilder.Entity<GenderEntity>(eb =>
        {
            eb.HasKey(x => x.Id);
        });
            
        
        
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<GenderEntity> Genders => Set<GenderEntity>();
}