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
            Console.WriteLine("[MVM] Console: MainViewModel");
        }

       public async Task LoadPostsAsync()
        {
            Console.WriteLine("[MVM] LoadPostsAsync");
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/users");
            var content = await response.Content.ReadAsStringAsync();
            Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(content);
        }
    }
}
