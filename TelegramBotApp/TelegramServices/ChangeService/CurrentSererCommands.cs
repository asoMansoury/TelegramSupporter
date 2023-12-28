using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.Databases.RepositoryLayers.TelegramToken;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.ExternalResource;
using TeelgramBotSupporter.ExternalResource.SoftEtherApi;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;
using TelegramBotApp.RenciServiceSSH.Cisco;
using TelegramBotApp.RenciServiceSSH.OpenVPN;
using TelegramBotApp.TelegramServices.Models;

namespace TeelgramBotSupporter.TelegramServices.ChangeService
{
    internal class CurrentSererCommands : ICommandQuery
    {
        private IMongoContext _mongoContext;
        private readonly ITelegramTokenRepository _telegramTokenRepository;
        private readonly ISoftetherApi _softetherApi;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        public CurrentSererCommands()
        {
            _mongoContext = new MongoContext();
            _telegramTokenRepository = new TelegramTokenRepository();
            _softetherApi = new SoftetherApi();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
            _serverCollectionRepository = new ServerCollectionRepository();
        }
        public async Task<object> Execute(object value)
        {
            var result = new ChangeCiscoModel();
            var obj = (UserChangeServer)value;
            var currentUser = _userRepository.GetUserEntityDocument(obj.username, obj.password).Result;
            obj.CurrentServercode = currentUser.currentservercode;
            var currentServerInformation = await _serverCollectionRepository.GetAllFilteredTypeDocument(currentUser.type, obj.ServerCode);
            if (currentServerInformation.type.ToLower() == ServerTypes.Cisco.GetDescription().ToLower())
            {
                ICiscoSSHService ciscoSSHService = new CiscoSSHService(currentServerInformation.host, currentServerInformation.port,currentServerInformation.username,currentServerInformation.password);
                ciscoSSHService.RestartUserCommand(obj.username, obj.password);
                result.serverUrl = currentServerInformation.ciscourl + ":" + currentServerInformation.ciscoPort;
                result.serverType = ServerTypes.Cisco;
                return result;
            }
            else if (currentUser.type.ToLower() == ServerTypes.Iran.GetDescription().ToLower())
            {
                ICiscoSSHService ciscoSSHService = new CiscoSSHService(currentServerInformation.host, currentServerInformation.port, currentServerInformation.username, currentServerInformation.password);
                ciscoSSHService.RestartUserCommand(obj.username, obj.password);
                result.serverUrl = currentServerInformation.ciscourl + ":" + currentServerInformation.ciscoPort;
                result.serverType = ServerTypes.Cisco;
                return result;
            }
            else if (currentUser.type.ToLower() == ServerTypes.OpenVPN.GetDescription().ToLower())
            {
                IOpenVPNCommandService openVPNCommandService = new OpenVPNCommandService(currentServerInformation.host, currentServerInformation.port, currentServerInformation.username, currentServerInformation.password);
                openVPNCommandService.RestartUserCommand(obj.username, obj.password);
                result.serverUrl = currentServerInformation.ovpTelegramPath;
                result.serverType = ServerTypes.OpenVPN;
            }
            var Token = await _telegramTokenRepository.GetTelegramTokenEntity();
            _softetherApi.RestartUserAccount(new UserChangeServer
            {
                username = obj.username
            }, Token.token,SoftEtherApiEnum.RestartAccount);
            return result;
        }
    }
}
