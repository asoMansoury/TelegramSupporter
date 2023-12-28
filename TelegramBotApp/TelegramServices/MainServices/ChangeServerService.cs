using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class ChangeServerService : BaseServiceAbstract, IBaseService
    {
        public ChangeServerService(TelegramBotClient client) : base(client)
        {
        }

        public Task Run(Stack<Update> stackForMessage, Update Item, string message, User? From, long chatId)
        {
            Messages staticMessages = new Messages();
            if (stackForMessage.Count >= 2)
            {
                var userPasswordItem = stackForMessage.Pop();
                var userNameItem = stackForMessage.Pop();
                var changeServerModel = new UserChangeServer
                {
                    username = userNameItem.Message.Text,
                    password = userPasswordItem.Message.Text,
                    telegramID = From.Username,
                    chatId = chatId.ToString()
                };
                var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.ChangeServer);


                var validateUserKeyboard = KeyBoard.ValidationInlineKeyBoard(changeServerModel);
                if (validateUserKeyboard.isValid == false)
                {
                    var mainKeyboard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                    SendMessage(chatId, validateUserKeyboard.Message, mainKeyboard);
                    stackForMessage.Clear();
                    return Task.CompletedTask;
                }

                var inlineKeyboard = KeyBoard.GenerateInlineKeyBoard(changeServerModel);
                SendMessageInline(chatId, staticMessages.ChangingServer(), inlineKeyboard);

                var replyKeyboard = KeyBoard.GenerateKeyBoard();
                SendMessage(chatId, staticMessages.ChangingServer2(), replyKeyboard);

                stackForMessage.Push(userNameItem);
                stackForMessage.Push(userPasswordItem);
                stackForMessage.Push(Item);
            }

            return Task.CompletedTask;
        }
    }

}
