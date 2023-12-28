using System.Text;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class RemainTimeService : BaseServiceAbstract, IBaseService
    {
        private readonly IUserRepository _userRepository;
        public RemainTimeService(TelegramBotClient client) : base(client)
        {
            _userRepository = new UserRepository();
        }

        public async Task Run(Stack<Update> stackForMessage, Update Item, string message, User? From, long chatId)
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

                var userEntity =await _userRepository.GetUserEntityDocument(changeServerModel.username, changeServerModel.password);
                var ExpireTime = DateTime.Parse(userEntity.expires);
                var remainDays = ExpireTime.Subtract(DateTime.Now);
                var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                SendMessage(chatId, staticMessages.CalculateRemainDay(remainDays.Days,ExpireTime), KeyBoard);
                stackForMessage.Clear();
            }
        }
    }

}
