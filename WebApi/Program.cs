using Carter;
using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.DI;
using WebApi.Entities;
using WebApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddSerializer();
builder.Services.AddSeedReader();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using var scope = app.Services.CreateAsyncScope();
var dbContext = scope.ServiceProvider.GetService<DataContext>();
var pendingMigration = await dbContext!.Database.GetPendingMigrationsAsync();

if (pendingMigration.Any())
{
    await dbContext.Database.MigrateAsync();
}

if (!dbContext.Users.Any())
{
    var seedReader = scope.ServiceProvider.GetService<ISeedReader>();
    var readedSeedResult = await seedReader!.ReadFileAsync<IEnumerable<Core.Models.User>>("Seed/data_seed.json");
    
    if (readedSeedResult.IsFailed)
    {
        throw new Exception();
    }
    
    var readedSeed = readedSeedResult.Value;
    await dbContext.Users.AddRangeAsync(readedSeed.Select(x => new UserEntity()
    {
        City = x.City,
        Country = x.Country,
        Created = x.Created,
        Description = x.Description,
        Gender = new GenderEntity()
        {
            Name = x.Gender
        },
        Interests = x.Interests,
        Skills = x.Skills,
        Username = x.Username,
        LastActive = x.LastActive,
        DateOfBirth = x.DateOfBirth
    }));
    await dbContext.SaveChangesAsync();
}   

app.UseHttpsRedirection();
app.MapCarter();
app.Run();