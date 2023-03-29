using ZLMClaims.ViewModels;

namespace ZLMClaims.Views;

public partial class UserPage : ContentPage
{      
    private readonly UserViewModel _viewModel;

    public UserPage() 
    {
        Console.WriteLine("[UserPage constructor] [==============] Before InitializeComponent");
        InitializeComponent();
        Console.WriteLine("[UserPage constructor] [==============] After InitializeComponent");

        Console.WriteLine("[UserPage constructor] [==============] Before instantiation of UserViewModel");
        _viewModel = new UserViewModel();
        Console.WriteLine("[UserPage constructor] [==============] After instantiation of UserViewModel");
        Console.WriteLine("[UserPage constructor] [==============] Before call GetUser with hardcoded id 1");
        _viewModel.GetUser(1);
        Console.WriteLine("[UserPage constructor] [==============] After call GetUser with hardcoded id 1");

        Console.WriteLine("[UserPage constructor] [==============] Before bindingContext with _viewmodel");
        BindingContext = _viewModel;
        Console.WriteLine("[UserPage constructor] [==============] After bindingContext with _viewmodel");
    }

    public UserPage(int userId)
    {
        Console.WriteLine("[UserPage] [==============] Before InitializeComponent");
        InitializeComponent();
        Console.WriteLine("[UserPage] [==============] After InitializeComponent");

        Console.WriteLine("[UserPage] [==============] Before instantiation of UserViewModel");
        _viewModel = new UserViewModel();
        Console.WriteLine("[UserPage] [==============] After instantiation of UserViewModel");
        Console.WriteLine("[UserPage] [==============] Before call GetUser with hardcoded id 1");
        _viewModel.GetUser(userId);
        Console.WriteLine("[UserPage] [==============] After call GetUser with hardcoded id 1");

        Console.WriteLine("[UserPage] [==============] Before bindingContext with _viewmodel");
        BindingContext = _viewModel;
        Console.WriteLine("[UserPage] [==============] After bindingContext with _viewmodel");
    }
}