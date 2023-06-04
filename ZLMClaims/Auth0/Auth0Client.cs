using IdentityModel.OidcClient;
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
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] init");
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

            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] na invoke oidcClient.LoginAsync met result " + loginRequest);
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] na invoke oidcClient.LoginAsync AccessToken " + loginRequest.ToString());
        }
        // return await oidcClient.LoginAsync(loginRequest);
        var loginResult = await oidcClient.LoginAsync(loginRequest);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] voor invoke oidcClient.LoginAsync");
        // var loginResult = await oidcClient.LoginAsync();
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] na invoke oidcClient.LoginAsync met result " + loginResult);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] na invoke oidcClient.LoginAsync AccessToken " + loginResult.AccessToken);
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LoginAsync] na invoke oidcClient.LoginAsync IdentityToken " + loginResult.IdentityToken);

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
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [LogoutAsync] init");
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
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] init");
        ClaimsPrincipal user = null;

        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Get idToken of SecureStorage");
        var idToken = await SecureStorage.Default.GetAsync("id_token");        

        if (idToken != null)
        {
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Retrieved idToken " + idToken);
            var doc = await new HttpClient().GetDiscoveryDocumentAsync(oidcClient.Options.Authority);
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] doc GetDiscoveryDocumentAsync " + doc);
            var validator = new JwtHandlerIdentityTokenValidator();
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] validator JwtHandlerIdentityTokenValidator " + validator);
            var options = new OidcClientOptions
            {
                ClientId = oidcClient.Options.ClientId,
                ProviderInformation = new ProviderInformation
                {
                    IssuerName = doc.Issuer,
                    KeySet = doc.KeySet
                }
            };
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] before validation of idToken iwth options " + options);
            var validationResult = await validator.ValidateAsync(idToken, options);
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] after validation with result " + validationResult);

            if (!validationResult.IsError) user = validationResult.User;
        }
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Return user");
        return user;
    }

    public async Task<ClaimsPrincipal> GetAuthenticatedUser()
    {
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] init");
        ClaimsPrincipal user = null;

        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Get idToken of SecureStorage");
        var idToken = await SecureStorage.Default.GetAsync("id_token");

        if (idToken != null)
        {
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Retrieved idToken " + idToken);
            var doc = await new HttpClient().GetDiscoveryDocumentAsync(oidcClient.Options.Authority);
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] doc GetDiscoveryDocumentAsync " + doc);
            var validator = new JwtHandlerIdentityTokenValidator();
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] validator JwtHandlerIdentityTokenValidator " + validator);
            var options = new OidcClientOptions
            {
                ClientId = oidcClient.Options.ClientId,
                ProviderInformation = new ProviderInformation
                {
                    IssuerName = doc.Issuer,
                    KeySet = doc.KeySet
                }
            };
            Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] before validation of idToken iwth options " + options);
            try
            {
                Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] validate token " + idToken + " with options " + options);
                Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] validate token with options " + options);
                var validationResult = await validator.ValidateAsync(idToken, options);
                Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] after validation with result " + validationResult);

                if (!validationResult.IsError)
                {
                    Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] !validationResult.IsError");
                    user = validationResult.User;
                }
                else
                {
                    // Token is expired or invalid
                    // Clear token data and show an error message to the user
                    Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Token is expired or invalid");
                    Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser]Set id_token and access_token to null");
                    await SecureStorage.Default.SetAsync("id_token", null);
                    await SecureStorage.Default.SetAsync("access_token", null);
                    Console.WriteLine("Token is expired or invalid. User needs to log in again.");
                }
            }
            catch (Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException)
            {
                Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Token is expired or invalid 22");
                // Token is expired
                // Clear token data and show an error message to the user
                //await SecureStorage.Default.SetAsync("id_token", string.Empty);
                //await SecureStorage.Default.SetAsync("access_token", string.Empty);
                Console.WriteLine("Token is expired. User needs to log in again.");
                LogoutAsync();
            }
            catch (Exception ex)
            {
                // Handle other exceptions that may occur during token validation
                Console.WriteLine("An error occurred during token validation: " + ex.Message);
            }
        }
        Console.WriteLine(DateTime.Now + "[..............] [Auth0Client] [GetAuthenticatedUser] Return user");
        return user;
    }


}
