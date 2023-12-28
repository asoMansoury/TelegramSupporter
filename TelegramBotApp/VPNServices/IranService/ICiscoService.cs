using TeelgramBotSupporter.Databases.Models;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.ExternalResource;
using TeelgramBotSupporter.TelegramServices.Const;
using TelegramBotApp.RenciServiceSSH.Cisco;

namespace TelegramBotApp.VPNServices.IranService
{
    public interface ICiscoService
    {
        Task RegisterNewUserService(UserEntity userEntity,string servercode);
        Task ChangeUserServer(UserEntity userEntity, string oldServer,string newServer);
    }

    public class CiscoService : ICiscoService
    {
        private  ICiscoSSHService _ciscoSSHService;
        private readonly IServerCollectionRepository _serverRepository;
        private readonly string _serverType = ServerTypes.Cisco.GetDescription();


        public CiscoService(ICiscoSSHService ciscoSSHService, IServerCollectionRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task ChangeUserServer(UserEntity userEntity, string oldServer, string newServer)
        {
            var ciscoEntities =await _serverRepository.GetFilteredTypeDocument(_serverType);
            var prevSelectedServer = ciscoEntities.FirstOrDefault(z=>z.servercode == oldServer);
            var newSelectedServer = ciscoEntities.FirstOrDefault(z => z.servercode == newServer);
            userEntity.currentservercode = newServer;
            _ciscoSSHService = new CiscoSSHService(prevSelectedServer.host,prevSelectedServer.port, prevSelectedServer.username, prevSelectedServer.password);
            _ciscoSSHService.RemoveUserCommand(userEntity.username);

            _ciscoSSHService = new CiscoSSHService(newSelectedServer.host, newSelectedServer.port, newSelectedServer.username, newSelectedServer.password);
            _ciscoSSHService.CreateNewUserCommand(userEntity.username,userEntity.password);
        }

        public Task RegisterNewUserService(UserEntity userEntity, string servercode)
        {
            throw new NotImplementedException();
        }
    }
}
