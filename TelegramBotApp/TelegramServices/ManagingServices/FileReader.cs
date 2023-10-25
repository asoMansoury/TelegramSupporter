using Telegram.Bot.Types.InputFiles;

namespace TelegramBotApp.TelegramServices.ManagingServices
{
    public class FileReader : IFileReader
    {
        public InputOnlineFile ReadFile(string filePath)
        {
            InputOnlineFile? inputFile = null;
            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                     inputFile = new InputOnlineFile(fileStream);

                }
            }
            return inputFile;
        }
    }
}
