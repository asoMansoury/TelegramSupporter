using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardSelectingSoftwareType : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;

        public TelegramKeyboardSelectingSoftwareType()
        {
            _serverCollectionRepository = new ServerCollectionRepository();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            throw new NotImplementedException();
        }

        public ReplyKeyboardMarkup GenerateKeyBoard(object T = null)
        {
            KeyboardButton[] row1 = {  new KeyboardButton(CommandsEnum.Cisco.GetDescription()), new KeyboardButton(CommandsEnum.OpenVPN.GetDescription()), new KeyboardButton(CommandsEnum.Start.GetDescription()) };
            _replyKeyboardMarkup = new ReplyKeyboardMarkup(row1)
            {
                OneTimeKeyboard = true,
                ResizeKeyboard = true
            };
            return _replyKeyboardMarkup;
        }

        public ValidateKeyboard ValidationInlineKeyBoard(object T = null)
        {
            var User = (T as UserChangeServer);
            var UserIsValid = _userRepository.GetUserEntityDocument(User.username, User.password).Result;
            if (UserIsValid != null)
            {
                _botTelegramRepository.CreateDocument(User.username, User.telegramID, User.chatId);
                return new ValidateKeyboard { isValid = true };
            }


            return new ValidateKeyboard { isValid = false, Message = "اطلاعات کاربر وارد شده معتبر نمی باشد، لطفا مجددا نام کاربری را وارد نمایید." };
        }

        public ValidateKeyboard ValidationKeyBoard(object T = null)
        {
            throw new NotImplementedException();
        }
    }
}
