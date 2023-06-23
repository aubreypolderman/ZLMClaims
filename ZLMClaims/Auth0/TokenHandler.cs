﻿using IdentityModel.OidcClient.Browser;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace ZLMClaims.Auth0;

public class TokenHandler : DelegatingHandler
{    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Debug.WriteLine(DateTime.Now + "[..........] [TokenHandler] [SendAsync] init");
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", TokenHolder.AccessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}
