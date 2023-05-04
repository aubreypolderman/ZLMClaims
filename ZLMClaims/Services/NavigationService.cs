namespace ZLMClaims.Services
{
    public partial class NavigationService : INavigationService
    {
        public Task GoToAsync(string route)
        {
            if (Shell.Current is null)
            {
                throw new NotSupportedException($"Navigation with the '{nameof(GoToAsync)}' method is currently supported only with a Shell-enabled application.");
            }

            return Shell.Current.GoToAsync(new ShellNavigationState(route));
        }

        public Task GoToAsync(string route, bool animate, IDictionary<string, object> parameters)
        {
            if (Shell.Current is null)
            {
                throw new NotSupportedException($"Navigation with the '{nameof(GoToAsync)}' method is currently supported only with a Shell-enabled application.");
            }

            return Shell.Current.GoToAsync(new ShellNavigationState(route), true, new Dictionary<string, object>
            {
                { nameof(parameters), parameters}
            });
    
        }

        public Task GoBackAsync()
        {
            if (Shell.Current is null)
            {
                throw new NotSupportedException($"Navigation with the '{nameof(GoBackAsync)}' method is currently supported only with a Shell-enabled application.");
            }

            return Shell.Current.GoToAsync(new ShellNavigationState(".."));
        }

    }
}
