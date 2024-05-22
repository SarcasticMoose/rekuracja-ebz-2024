using Core.Serializer;
using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.Domain.Models;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Infrastructure.Utils;

public class DatabaseLuncher(DataContext dataContext, IFileReader fileReader, ISerializer serializer) : IDatabaseLuncher
{
    public async Task Startup(CancellationToken ct)
    {
        if (await IsDatabaseExists())
        {
            await dataContext.Database.MigrateAsync();
        }

        if (IsUserTableIsEmpty())
        {
            var readedSeedFileResult = await fileReader.ReadFileAsyncToString("Seed/data_seed.json", ct);
        
            if (readedSeedFileResult.IsFailed)
            {
                throw new NotImplementedException();
            }

            var readedSeedFile = readedSeedFileResult.Value;

            var deserializedSeedFromFileResult = await serializer.DeserializeAsync<IList<User>>(readedSeedFile,ct);
            
            if (deserializedSeedFromFileResult.IsFailed)
            {
                throw new NotImplementedException();
            }
            
            var deserializedSeed = deserializedSeedFromFileResult.Value;
            
            if (deserializedSeed is null)
            {
                throw new NotImplementedException();
            }
        
            var genders = deserializedSeed
                .Select(x => x.Gender)
                .Distinct()
                .Select(x =>
                {
                    return new WebApi.Infrastructure.Entities.Gender()
                    {
                        Name = x
                    };
                })
                .ToList();
            
            await dataContext.Users.AddRangeAsync(deserializedSeed.Select(x => new WebApi.Infrastructure.Entities.User()
            {
                City = x.City,
                Country = x.Country,
                Created = x.Created,
                Description = x.Description,
                Gender = genders.First(y => y.Name == x.Gender),
                Interests = x.Interests,
                Skills = x.Skills,
                Username = x.Username,
                LastActive = x.LastActive,
                DateOfBirth = x.DateOfBirth
            }), cancellationToken: ct);
            
            await dataContext.SaveChangesAsync(ct);
        }
    }
    private async Task<bool> IsDatabaseExists()
    {
        var pendingMigration =  await dataContext.Database.GetPendingMigrationsAsync();
        
        return !pendingMigration.Any();
    }
    private bool IsUserTableIsEmpty()
    {
        return !dataContext.Users.Any();
    }
    
}