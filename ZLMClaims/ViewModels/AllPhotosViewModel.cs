using Newtonsoft.Json;
using System.Collections.ObjectModel;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    public class AllPhotosViewModel : BindableObject   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Photo> _photos;

        public ObservableCollection<Photo> Photos
        {
            get => _photos;
            set
            {
                _photos = value;
                OnPropertyChanged();
            }
        }

        public Command GetUsersCommand { get; }

        public AllPhotosViewModel()
        {
            Console.WriteLine("[AllPhotosViewModel] [==============] Constructor");
        }

       public async Task LoadPostsAsync()
        {
            Console.WriteLine("[AllPhotosViewModel] [==============] LoadPostsAsync");
            var response = await _client.GetAsync("https://jsonplaceholder.typicode.com/photos");
            Console.WriteLine("[AllPhotosViewModel] [==============] reponse 1 " + response);
            var content = await response.Content.ReadAsStringAsync();
           // Console.WriteLine("[AllProAllAlbumsViewModelductsViewModel] [==============] content 1 " + content);
            Photos = JsonConvert.DeserializeObject<ObservableCollection<Photo>>(content);
        }
    }
}
