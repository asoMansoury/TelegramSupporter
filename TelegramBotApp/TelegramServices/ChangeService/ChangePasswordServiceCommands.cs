using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.RepositoryLayers.TelegramToken;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.ExternalResource.SoftEtherApi;
using TeelgramBotSupporter.TelegramServices.Models;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;

namespace TeelgramBotSupporter.TelegramServices.ChangeService
{
    internal class ChangePasswordServiceCommands : ICommandQuery
    {
        private IMongoContext _mongoContext;
        private readonly ITelegramTokenRepository _telegramTokenRepository;
        private readonly ISoftetherApi _softetherApi;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;
        public ChangePasswordServiceCommands()
        {
            _mongoContext = new MongoContext();
            _telegramTokenRepository = new TelegramTokenRepository();
            _softetherApi = new SoftetherApi();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
        }
        public async Task<object> Execute(object value)
        {
            var obj = (UserChangePassword)value;
            var currentUser = _userRepository.GetUserEntityDocument(obj.username, obj.password).Result;
            var Token = await _telegramTokenRepository.GetTelegramTokenEntity();
            await _softetherApi.ChangeUserPassword(obj, Token.token, ExternalResource.SoftEtherApiEnum.ChangeUserPassworApi);
            return null;
        }
    }
}
