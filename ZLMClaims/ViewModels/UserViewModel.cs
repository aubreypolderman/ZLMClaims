using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ZLMClaims.ViewModels;

using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using ZLMClaims.Models;
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
        Console.WriteLine("[UserViewModel] [==============] OnPropertyChanged");
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetUser(int userId)
        {
        Console.WriteLine("[UserViewModel] [==============] GetUser with id " + userId);
        HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/1");
        Console.WriteLine("[UserViewModel] [==============] reponse 1 " + response);
        response.EnsureSuccessStatusCode();
        Console.WriteLine("[UserViewModel] [==============] reponse 2 " + response);
        string json = await response.Content.ReadAsStringAsync();
        Console.WriteLine("[UserViewModel] [==============] string json " + json);
        User = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
        }
    }
