using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
namespace ZLMClaims.ViewModels
{
    /*
     * [AlsoNotifyChangeFor] => [NotifyPropertyChangedFor]
     * [AlsoNotifyCanExecuteFor] => [NotifyCanExecuteChangedFor]
     * [AlsoValidateProperty] => [NotifyDataErrorInfo] 
     * [AlsoBroadcastChange] => [NotifyPropertyChangeRecipients]
     * [ICommand] => [RelayCommand]
     */
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))]
        bool isLoading;

        [ObservableProperty]
        string title;

        public bool IsNotLoading => !isLoading;
    }
}
