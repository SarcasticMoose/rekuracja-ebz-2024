using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor.App.Core.UI;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private const string localStorageTokenString = "token";

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsStringAsync(localStorageTokenString);
        ClaimsIdentity identity = new();
        
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();
            var readabletoken = handler.ReadJwtToken(token.Trim('"'));
            identity = new(readabletoken.Claims,"jwt");
        }
        
        ClaimsPrincipal user = new ClaimsPrincipal(identity);
        AuthenticationState state = new AuthenticationState(user);
        Task<AuthenticationState> stateTask = Task.FromResult(state);
        NotifyAuthenticationStateChanged(stateTask);

        return state;
    }

}