namespace ZLMClaims.Services
{
    public interface INavigationService
    {
        Task GoToAsync(string route);
        Task GoToAsync(string route, bool animate, IDictionary<string, object> parameters);

        Task GoBackAsync();
    }
}
