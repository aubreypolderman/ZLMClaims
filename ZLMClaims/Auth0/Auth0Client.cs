﻿using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser; 
using IdentityModel.Client;
using System.Security.Claims;

namespace ZLMClaims.Auth0;

public class Auth0Client
{
    private readonly OidcClient oidcClient;
    private string audience;

    public Auth0Client(Auth0ClientOptions options)
    {
        oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = $"https://{options.Domain}",
            ClientId = options.ClientId,
            Scope = options.Scope,
            RedirectUri = options.RedirectUri,
            Browser = options.Browser
        });

        audience = options.Audience;
    }

    public IdentityModel.OidcClient.Browser.IBrowser Browser
    {
        get
        {
            return oidcClient.Options.Browser;
        }
        set
        {
            oidcClient.Options.Browser = value;
        }
    }

    public async Task<LoginResult> LoginAsync()
    {
        LoginRequest loginRequest = null;

        if (!string.IsNullOrEmpty(audience))
        {
            loginRequest = new LoginRequest
            {
                FrontChannelExtraParameters = new Parameters(new Dictionary<string, string>()
            {
              {"audience", audience}
            })
            };            
        }
        // return await oidcClient.LoginAsync(loginRequest);
        var loginResult = await oidcClient.LoginAsync(loginRequest);

        if (!loginResult.IsError)
        {
            await SecureStorage.Default.SetAsync("access_token", loginResult.AccessToken);
            await SecureStorage.Default.SetAsync("id_token", loginResult.IdentityToken);
        }
        return loginResult;        
    }

    // logout
    public async Task<BrowserResult> LogoutAsync()
    {
        var logoutParameters = new Dictionary<string, string>
        {
            {"client_id", oidcClient.Options.ClientId },
            {"returnTo", oidcClient.Options.RedirectUri }
        };

        var logoutRequest = new LogoutRequest();
        var endSessionUrl = new RequestUrl($"{oidcClient.Options.Authority}/v2/logout")
          .Create(new Parameters(logoutParameters));
        var browserOptions = new BrowserOptions(endSessionUrl, oidcClient.Options.RedirectUri)
        {
            Timeout = TimeSpan.FromSeconds(logoutRequest.BrowserTimeout),
            DisplayMode = logoutRequest.BrowserDisplayMode
        };

        var browserResult = await oidcClient.Options.Browser.InvokeAsync(browserOptions);
        SecureStorage.Default.RemoveAll();
        return browserResult;
    }

    public async Task<ClaimsPrincipal> GetAuthenticatedUser2()
    {
        ClaimsPrincipal user = null;      
        var idToken = await SecureStorage.Default.GetAsync("id_token");

        if (idToken != null)
        {            
            var doc = await new HttpClient().GetDiscoveryDocumentAsync(oidcClient.Options.Authority);
            var validator = new JwtHandlerIdentityTokenValidator();
            var options = new OidcClientOptions
            {
                ClientId = oidcClient.Options.ClientId,
                ProviderInformation = new ProviderInformation
                {
                    IssuerName = doc.Issuer,
                    KeySet = doc.KeySet
                }
            };
            var validationResult = await validator.ValidateAsync(idToken, options);

            if (!validationResult.IsError) user = validationResult.User;
        }
        return user;
    }

    public async Task<ClaimsPrincipal> GetAuthenticatedUser()
    {
        ClaimsPrincipal user = null;
        var idToken = await SecureStorage.Default.GetAsync("id_token");

        if (idToken != null)
        {
            var doc = await new HttpClient().GetDiscoveryDocumentAsync(oidcClient.Options.Authority);
            var validator = new JwtHandlerIdentityTokenValidator();
            var options = new OidcClientOptions
            {
                ClientId = oidcClient.Options.ClientId,
                ProviderInformation = new ProviderInformation
                {
                    IssuerName = doc.Issuer,
                    KeySet = doc.KeySet
                }
            };
            try
            {
                var validationResult = await validator.ValidateAsync(idToken, options);

                if (!validationResult.IsError)
                {
                    user = validationResult.User;
                    // Save AccessToken 
                    TokenHolder.AccessToken = idToken;
                }
                else
                {
                    // Token is expired or invalid
                    // Clear token data and show an error message to the user
                    await SecureStorage.Default.SetAsync("id_token", null);
                    await SecureStorage.Default.SetAsync("access_token", null);
                }
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException)
            {
                LogoutAsync();
            }
            catch (Exception ex)
            {
                // Handle other exceptions that may occur during token validation
                Console.WriteLine("An error occurred during token validation: " + ex.Message);
            }
        }
        return user;
    }
}