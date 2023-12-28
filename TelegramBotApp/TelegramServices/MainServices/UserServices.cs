using TeelgramBotSupporter.Databases.Models;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.ExternalResource.SoftEtherApi;
using TeelgramBotSupporter.TelegramServices.ChangeService;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using TelegramBotApp.RenciServiceSSH.Cisco;
using TelegramBotApp.RenciServiceSSH.OpenVPN;
using TelegramBotApp.TelegramServices.ManagingServices;
using TelegramBotApp.TelegramServices.Models;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public class UserServices : BaseServiceAbstract, IBaseService, IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly ISoftetherApi _softetherApi;
        public UserServices(TelegramBotClient client,IUserRepository userRepository) : base(client)
        {
            this._userRepository = userRepository;
            _softetherApi = new SoftetherApi();
        }

        public UserEntity GetUserEntity(string username, string password)
        {
            return _userRepository.GetUserEntityDocument(username, password).Result;
        }

        public Task Run(Stack<Update> stackForMessage, Update Item, string message, User? From, long chatId)
        {
            throw new NotImplementedException();
        }

        public Task SendCurrentServerInformation(Update Item, string message, User? From, long chatId, string username, string password)
        {
            var changeServerModel = new UserChangeServer
            {
                username = username,
                password = password,
                telegramID = From.Username,
                chatId = chatId.ToString()
            };
            ICommandQuery commandQuery = new CurrentSererCommands();
            var userEntityData = GetUserEntity(username, password);
            var obj = new UserChangeServer
            {
                ServerCode = userEntityData.currentservercode,
                password = password,
                username = username
            };
            var result = commandQuery.Execute(obj).Result as ChangeCiscoModel;
            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SendPassword);
            if (result.serverType == TeelgramBotSupporter.ExternalResource.ServerTypes.Cisco)
            {

                SendMessage(chatId, result.serverUrl, KeyBoard.GenerateKeyBoard());
            }
            else
            {
                IFileReader fileReader = new FileReader();
                FileStream fileStream;
                lock (fileReader)
                {
                    fileStream = new FileStream(result.serverUrl, FileMode.Open);
                }
                var inputFile = new InputOnlineFile(fileStream);
                inputFile.FileName = result.serverUrl.Split("/").LastOrDefault();
                var result2 = _client.SendDocumentAsync(chatId, inputFile).Result;
                fileStream.Close();
            }
            return Task.CompletedTask;
        }

        public Task SendServerInformation( Update Item, string message, User? From, long chatId, string username, string password)
        {
            var changeServerModel = new UserChangeServer
            {
                username = username,
                password = password,
                telegramID = From.Username,
                chatId = chatId.ToString()
            };
            ICommandQuery commandQuery = new ChangeServiceCommands();
            var userEntityData = GetUserEntity(username, password);
            var obj = new UserChangeServer
            {
                ServerCode = userEntityData.currentservercode,
                password = password,
                username = username
            };
            var result =  commandQuery.Execute(obj).Result as ChangeCiscoModel;
            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SendPassword);
            if (result.serverType == TeelgramBotSupporter.ExternalResource.ServerTypes.Cisco)
            {
                SendMessage(chatId, (result as ChangeCiscoModel).serverUrl, KeyBoard.GenerateKeyBoard());
            }
            else
            {
                IFileReader fileReader = new FileReader();
                FileStream fileStream;
                lock (fileReader)
                {
                    fileStream = new FileStream(result.serverUrl, FileMode.Open);
                }
                var inputFile = new InputOnlineFile(fileStream);
                inputFile.FileName = result.serverUrl.Split("/").LastOrDefault();
                var result2 =  _client.SendDocumentAsync(chatId, inputFile).Result;
                fileStream.Close();
            }
            return Task.CompletedTask;
        }
    }
}
