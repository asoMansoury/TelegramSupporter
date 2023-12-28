using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class CommitChangePasswordService : BaseServiceAbstract, IBaseService
    {
        public CommitChangePasswordService(TelegramBotClient client) : base(client)
        {
        }

        public Task Run(Stack<Update> stackForMessage, Update Item, string message, User? From, long chatId)
        {
            Messages staticMessages = new Messages();
            if (stackForMessage.Count >= 3)
            {
                stackForMessage.Pop();
                var userPasswordItem = stackForMessage.Pop().Message.Text;
                var userNameItem = stackForMessage.Pop().Message.Text;
                var changeServerModel = new UserChangeServer
                {
                    username = userNameItem,
                    password = userPasswordItem
                };
                var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.CommitChangePassword).GenerateKeyBoard(changeServerModel);
                SendMessage(chatId, staticMessages.SuccessOperation(), KeyBoard);
                stackForMessage.Push(Item);
            }
            else
            {
                var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                SendMessage(chatId, staticMessages.WelcomPack(From.Username), KeyBoard);
                stackForMessage.Clear();
            }

            return Task.CompletedTask;
        }
    }

}
