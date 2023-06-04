namespace ZLMClaims.Auth0;

public class Auth0ClientOptions
{
    public Auth0ClientOptions()
    {
        Console.WriteLine(DateTime.Now + "[..............] [Auth0ClientOptions] [Constructor] init");
        Scope = "openid";
        RedirectUri = "zlmclaims://callback";
        Browser = new WebBrowserAuthenticator();
        Audience = "";
    }

    public string Domain { get; set; }

    public string ClientId { get; set; }

    public string RedirectUri { get; set; }

    public string Scope { get; set; }
    public string Audience { get; set; }

    public IdentityModel.OidcClient.Browser.IBrowser Browser { get; set; }
}
