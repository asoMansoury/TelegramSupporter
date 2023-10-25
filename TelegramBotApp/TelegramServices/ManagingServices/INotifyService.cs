namespace TelegramBotApp.TelegramServices.ManagingServices
{
    public interface INotifyService
    {
        Task NotifyUsersCloseToExpired();
    }
}
