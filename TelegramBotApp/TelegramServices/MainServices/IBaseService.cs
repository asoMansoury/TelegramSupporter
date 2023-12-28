using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public interface IBaseService
    {
        Task Run(Stack<Update> stackForMessage, Update Item,string message, User? From,long chatId);
    }
}
