using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class DownloadServices : BaseServiceAbstract, IBaseService
    {

        public DownloadServices(TelegramBotClient client) : base(client)
        {
        }

        public Task Run(Stack<Update> stackForMessage, Update Item, string message, User? From, long chatId)
        {
            Messages staticMessages = new Messages();
            var userPasswordItem = stackForMessage.Pop();
            var userNameItem = stackForMessage.Pop();
            var changeServerModel = new UserChangeServer
            {
                username = userNameItem.Message.Text,
                password = userPasswordItem.Message.Text,
                telegramID = From.Username,
                chatId = chatId.ToString()
            };
            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.DownloadSoftwares);
            var replyKeyboard = KeyBoard.GenerateKeyBoard();
            SendMessage(chatId, staticMessages.ChooseSoftwareType(), replyKeyboard);

            stackForMessage.Push(userNameItem);
            stackForMessage.Push(userPasswordItem);
            stackForMessage.Push(Item);
            return Task.CompletedTask;
        }
    }

}
