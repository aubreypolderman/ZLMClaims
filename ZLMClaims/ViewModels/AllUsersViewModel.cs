using Newtonsoft.Json;
using System.Collections.ObjectModel;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    public class AllUsersViewModel : BindableObject   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public Command GetUsersCommand { get; }

        public AllUsersViewModel()
        {
            Console.WriteLine("[AllUsersViewModel] Constructor");
        }

       public async Task LoadPostsAsync()
        {
            Console.WriteLine("[AllUsersViewModel] LoadPostsAsync");
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/users");
            Console.WriteLine("[AllUsersViewModel] Response: " + response);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("[AllUsersViewModel] Content: " + content);
            Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(content);
        }
    }
}
