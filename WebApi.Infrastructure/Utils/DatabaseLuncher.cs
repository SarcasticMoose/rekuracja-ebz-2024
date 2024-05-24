using Core.Serializer;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.Domain.Models;
using WebApi.Infrastructure.Auth.Hashing;
using WebApi.Infrastructure.Entities;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Infrastructure.Utils;

public class DatabaseLuncher(DataContext dataContext, IFileReader fileReader, ISerializer serializer, IHashService hashService) : IDatabaseLuncher
{
    public async Task<Result> Startup(CancellationToken ct)
    {
        if (!await IsDatabaseExists())
        {
            await dataContext.Database.MigrateAsync(cancellationToken: ct);
        } 

        if (IsUserTableIsEmpty())
        {
            var readedSeedFileResult = await fileReader.ReadFileAsyncToString(filePath: $"../WebApi.Infrastructure/Seed/data_seed.json", ct: ct);
        
            if (readedSeedFileResult.IsFailed)
            {
                return Result.Fail(readedSeedFileResult.Errors);
            }

            var readedSeedFile = readedSeedFileResult.Value;

            var deserializedSeedFromFileResult = await serializer.DeserializeAsync<IList<UserDto>>(readedSeedFile,ct);
            
            if (deserializedSeedFromFileResult.IsFailed)
            {
                return Result.Fail(deserializedSeedFromFileResult.Errors);
            }
            
            var deserializedSeed = deserializedSeedFromFileResult.Value;
            
            if (deserializedSeed is null)
            {
                return Result.Fail(new Error("Deserialized seed is null"));
            }
        
            var genders = deserializedSeed
                .Select(x => x.Gender)
                .Distinct()
                .Select(x => new Gender()
                {
                    Name = x
                })
                .ToList();
            
            await dataContext.Users.AddRangeAsync(deserializedSeed.Select(x => new WebApi.Infrastructure.Entities.User()
            {
                Username = x.Username,
                Password = hashService.Hash("1234567"),
                UserDetails = new UserDetails()
                {
                    City = x.City,
                    Country = x.Country,
                    Created = x.Created,
                    Description = x.Description,
                    Gender = genders.First(y => y.Name == x.Gender),
                    Interests = x.Interests,
                    Skills = x.Skills,
                    LastActive = x.LastActive,
                    DateOfBirth = x.DateOfBirth
                }
            }), cancellationToken: ct);
            
            await dataContext.SaveChangesAsync(ct);
            return Result.Ok();
        }
        
        return Result.Ok();
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