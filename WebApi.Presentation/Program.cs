using System.Text.Json.Serialization;
using Carter;
using Core.Serializer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using WebApi.Application;
using WebApi.Configuration;
using WebApi.DI;
using WebApi.Infrastructure;
using WebApi.Infrastructure.Persistence;
using WebApi.Infrastructure.Utils;
using WebApi.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});

builder.Services.AddCors();
builder.Services.AddDatabaseContext(builder);
builder.Services.AddJwtAuthentication();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
builder.Services.ConfigureOptions<JwtOptionSetup>();
builder.Services.AddHashingService();
builder.Services.AddJwtService();
builder.Services.AddApplication();
builder.Services.AddSerializer();
builder.Services.AddDatabaseLuncher();
builder.Services.AddFileReader();
builder.Services.AddScoped<DatabaseLuncher>();
builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using var scope = app.Services.CreateAsyncScope();
var databaseCreator = scope.ServiceProvider.GetRequiredService<DatabaseLuncher>();
await databaseCreator.Startup(default!);

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));// Allow any origin  

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapCarter();
app.Run();