using IdentityModel.OidcClient.Browser;
using System.Net.Http.Headers;

namespace ZLMClaims.Auth0;

public class TokenHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [SendAsync] Start");
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [SendAsync] Add Token to Bearer header => " + TokenHolder.AccessToken);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [SendAsync] Return request.Headers.Authorization => " + request.Headers.Authorization);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [SendAsync] Return request => " + request.ToString());
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [SendAsync] Return cancellationToken => " + cancellationToken);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [SendAsync] End. Return");
        return await base.SendAsync(request, cancellationToken);
    }
}
