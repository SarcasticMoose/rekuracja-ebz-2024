using System.Text.Json;
using System.Text.Json.Serialization;
using Blazor.App.Core.UI;
using Blazored.LocalStorage;
using Core.DataAccess;
using Core.DelegateHendlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GUI;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddTransient<AddBarearTokenHeader>();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<IRestApiService, RestApiService>();
builder
    .Services
    .AddRefitClient<IDataAccess>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7296/"))
    .AddHttpMessageHandler<AddBarearTokenHeader>();;
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
await builder.Build().RunAsync();