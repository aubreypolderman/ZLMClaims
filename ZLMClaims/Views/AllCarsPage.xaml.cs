using ZLMClaims.ViewModels;

namespace ZLMClaims.Views
{
    public partial class AllCarsPage : ContentPage
    {
        public AllCarsPage(CarListViewModel carListViewModel)
        {
            InitializeComponent();
            BindingContext = carListViewModel;
        }
    }
}
