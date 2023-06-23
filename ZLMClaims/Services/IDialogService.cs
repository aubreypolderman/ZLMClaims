namespace ZLMClaims.Services
{
    public interface IDialogService
    {
        Task DisplayAlertAsync(string title, string message, string cancel);

        Task DisplayAlertAsync(string title, string message, string accept, string cancel);
        Task<bool> DisplayConfirmationAlertAsync(string title, string message, string accept, string cancel);
    }
}
