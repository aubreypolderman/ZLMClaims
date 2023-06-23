using ZLMClaims.ViewModels;
namespace ZLMClaims.Views;

public partial class LoadingPage : ContentPage
{
    public LoadingPage()
    {
        InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await isAuthenticated())
        {
            Console.WriteLine(DateTime.Now + "[.........][LoadingPage] OnNavigatedTo: User authenticated. Go to home");
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            Console.WriteLine(DateTime.Now + "[.........][LoadingPage] OnNavigatedTo: User not authenticated. Go to home");
            await Shell.Current.GoToAsync("login");
        }
        base.OnNavigatedTo(args);
    }

    async Task<bool> isAuthenticated()
    {
        await Task.Delay(2000);
        var hasAuth = await SecureStorage.GetAsync("hasAuth");
        Console.WriteLine(DateTime.Now + "[.........][LoadingPage] isAuthenticated: hasAuth => " + hasAuth);
        return !(hasAuth == null);
    }
}

