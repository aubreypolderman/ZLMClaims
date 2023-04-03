using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ZLMClaims.ViewModels;

using System.ComponentModel;
using System.Diagnostics;
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
        Console.WriteLine("[UserViewModel] [OnPropertyChanged] [==============] start with propertyname " + propertyName);
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task GetUser(int userId)
        {
        Debug.WriteLine("[UserViewModel] [GetUser] [==============] Debug GetUser with id " + userId);
        Console.WriteLine("[UserViewModel] [GetUser] [==============] GetUser with id " + userId);
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"https://jsonplaceholder.typicode.com/users/1");
        Console.WriteLine("[UserViewModel] [GetUser] [==============] reponse 1: " + response);
        response.EnsureSuccessStatusCode();
        Console.WriteLine("[UserViewModel] [GetUser] [==============] reponse 2 " + response);
        string json = await response.Content.ReadAsStringAsync();
        User = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json);
        }
    }
