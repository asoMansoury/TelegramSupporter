using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class DownloadOpenVPNApp : BaseServiceAbstract, IBaseService
    {

        public DownloadOpenVPNApp(TelegramBotClient client) : base(client)
        {
        }

        public Task Run(Stack<Update> stackForMessage, Update Item, string message, User? From, long chatId)
        {
            Messages staticMessages = new Messages();
            var CiscoTypeSelected = stackForMessage.Pop();
            var userPasswordItem = stackForMessage.Pop();
            var userNameItem = stackForMessage.Pop();
            var changeServerModel = new UserChangeServer
            {
                username = userNameItem.Message.Text,
                password = userPasswordItem.Message.Text,
                telegramID = From.Username,
                chatId = chatId.ToString()
            };
            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.OpenVPN);
            var replyKeyboard = KeyBoard.GenerateInlineKeyBoard();
            base.SendMessageInline(chatId, staticMessages.ChooseSoftwareType(), replyKeyboard);


            stackForMessage.Push(userNameItem);
            stackForMessage.Push(userPasswordItem);
            stackForMessage.Push(CiscoTypeSelected);
            stackForMessage.Push(Item);
            return Task.CompletedTask;
        }
    }



}
