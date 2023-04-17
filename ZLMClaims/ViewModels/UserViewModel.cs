using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ZLMClaims.ViewModels;

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using ZLMClaims.Models;
using ZLMClaims.Resources.Languages;
using ZLMClaims.Services;
public class UserViewModel : INotifyPropertyChanged
{

    private User _user;
    public User User
    {

        get { return _user; }
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        Console.WriteLine("[UserViewModel] [OnPropertyChanged] [==============] start with propertyname " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async Task GetUser(int userId)
    {
        Console.WriteLine("[..............] [UserViewModel] [GetUser] Userid: {userId}");
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/{userId}");
        Console.WriteLine("[..............] [UserViewModel] [GetUser] response 1: " + response);
        response.EnsureSuccessStatusCode();
        Console.WriteLine("[..............] [UserViewModel] [GetUser] response 2: " + response);
        string json = await response.Content.ReadAsStringAsync();
        User = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
    }

    public void OnThemeSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnThemeSwitchToggledAsyn] Current theme: " + Application.Current.RequestedTheme);

        if (Application.Current.RequestedTheme == AppTheme.Dark)
        {
            Console.WriteLine("[..............] [UserViewModel] [OnThemeSwitchToggledAsyn] Switch to theme Light");
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            Console.WriteLine("[..............] [UserViewModel] [OnThemeSwitchToggledAsyn] Switch to theme Dark");
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
    }

    public void OnLanguageSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnLanguageSwitchToggled] ");
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("nl", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("nl-NL");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);
    }

    public void OnEmailConfirmationSwitchToggled()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnEmailConfirmationSwitchToggled]");
    }

    public async Task OnEmailBtnClicked()
    {
        Console.WriteLine("[..............] [UserViewModel] [OnEmailBtnClicked]");
        if (Email.Default.IsComposeSupported)
        {
            var message = new EmailMessage
            {
                Subject = "Hi how are you?",
                Body = "Thanks for being here, nice to meet you!",
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(new[] { "aubreypolderman@gmail.com" })
            };
            message.Attachments.Add(new EmailAttachment(Path.Combine(FileSystem.CacheDirectory, "dotnet_bot.svg")));

            await Email.Default.ComposeAsync(message);
        }
    }
}
