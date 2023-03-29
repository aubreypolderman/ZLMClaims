using Newtonsoft.Json;
using System.Collections.ObjectModel;
using ZLMClaims.Models;

namespace ZLMClaims.ViewModels
{
    public class AllProductsViewModel : BindableObject   
    {
        private readonly HttpClient _client = new HttpClient();
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public Command GetUsersCommand { get; }

        public AllProductsViewModel()
        {
            Console.WriteLine("[AllProductsViewModel] [==============] Constructor");
        }

       public async Task LoadPostsAsync()
        {
            Console.WriteLine("[AllProductsViewModel] [==============] LoadPostsAsync");
            var response = await _client.GetAsync("https://dummyjson.com/products");
            Console.WriteLine("[AllProductsViewModel] [==============] reponse 1 " + response);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("[AllProductsViewModel] [==============] content 1 " + content);
            Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(content);
        }
    }
}
