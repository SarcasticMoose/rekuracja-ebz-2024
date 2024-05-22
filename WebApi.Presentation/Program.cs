using Carter;
using Microsoft.EntityFrameworkCore;
using WebApi.Configuration;
using WebApi.DI;
using WebApi.Infrastructure.Persistence;
using WebApi.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddSerializer();
builder.Services.AddDatabaseLuncher();
builder.Services.AddFileReader();
builder.Services.AddScoped<DatabaseLuncher>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using var scope = app.Services.CreateAsyncScope();
var databaseCreator = scope.ServiceProvider.GetRequiredService<DatabaseLuncher>();
await databaseCreator.Startup(default!);

app.UseHttpsRedirection();
app.MapCarter();
app.Run();