using Telegram.Bot.Types.InputFiles;

namespace TelegramBotApp.TelegramServices.ManagingServices
{
    public interface IFileReader
    {
        InputOnlineFile ReadFile(string filePath);
    }
}
