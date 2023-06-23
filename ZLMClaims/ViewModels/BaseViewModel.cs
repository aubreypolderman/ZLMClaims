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
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;
    }
}
