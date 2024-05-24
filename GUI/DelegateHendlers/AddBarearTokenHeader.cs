using Blazored.LocalStorage;

namespace Core.DelegateHendlers;

public class AddBarearTokenHeader(ILocalStorageService localStorage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string? token = await localStorage.GetItemAsStringAsync("token");
        if (token == null)
        {
            return await base.SendAsync(request, cancellationToken);
        }
        string bearerToken = $"Bearer {token.Trim('"')}";
        request.Headers.Add("Authorization",bearerToken);
        return await base.SendAsync(request, cancellationToken);
    }
}