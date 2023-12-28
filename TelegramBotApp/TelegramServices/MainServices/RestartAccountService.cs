using TeelgramBotSupporter.Databases.RepositoryLayers.TelegramToken;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.ExternalResource.SoftEtherApi;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class RestartAccountService : BaseServiceAbstract, IBaseService
    {
        private readonly ISoftetherApi _softetherApi;
        private readonly IUserRepository _userRepository;
        private readonly ITelegramTokenRepository _telegramTokenRepository;
        public RestartAccountService(TelegramBotClient client) : base(client)
        {
            _userRepository = new UserRepository();
            _softetherApi = new SoftetherApi();
            _telegramTokenRepository = new TelegramTokenRepository();
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

                var token = _telegramTokenRepository.GetTelegramTokenEntity().Result.token;
                _softetherApi.RestartUserAccount(new UserChangeServer
                {
                    username = userNameItem.Message.Text,
                },token,TeelgramBotSupporter.ExternalResource.SoftEtherApiEnum.RestartAccount);
                var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                SendMessage(chatId, staticMessages.SuccessOperation(), KeyBoard);
                stackForMessage.Clear();
            }
        }
    }

}
