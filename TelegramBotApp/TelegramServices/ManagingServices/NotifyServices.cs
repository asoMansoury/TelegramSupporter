using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using Telegram.Bot;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;

namespace TelegramBotApp.TelegramServices.ManagingServices
{
    public class NotifyServices : INotifyService
    {
        private readonly IBotTelegramRepository _botTelegramRepository;
        private readonly IUserRepository _userRepository;
        private Telegram.Bot.TelegramBotClient _client;

        public NotifyServices()
        {
            _botTelegramRepository = new BotTelegramRepository();
            _userRepository = new UserRepository();
            _client = new Telegram.Bot.TelegramBotClient(ConfigData.TelegramToken);
        }
        public async Task NotifyUsersCloseToExpired()
        {
            try
            {
                //var UsersToSendMessage = await _botTelegramRepository.GetFilteredDocument("psh106");
                var result = await _client.SendTextMessageAsync("@AsoMansouri", "aso hastam");
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();

            }
        }
    }
}
