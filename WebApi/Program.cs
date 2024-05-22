using Carter;
using Microsoft.EntityFrameworkCore;
using WebApi.DI;
using WebApi.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddSerializer();
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
    
}


app.UseHttpsRedirection();
app.MapCarter();
app.Run();