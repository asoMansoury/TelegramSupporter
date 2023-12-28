using TeelgramBotSupporter.Databases.Models;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public interface IUserServices
    {
        UserEntity GetUserEntity(string username, string password);

        Task SendServerInformation(Update Item, string message, User? From, long chatId,string username,string password);
        Task SendCurrentServerInformation(Update Item, string message, User? From, long chatId, string username, string password);
    }
}
