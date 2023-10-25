using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.Models;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.TelegramServices.ChangeService;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardChangePassword : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        private readonly IUserRepository _userRepository;
        public TelegramKeyboardChangePassword()
        {
            _serverCollectionRepository = new ServerCollectionRepository();
            _userRepository = new UserRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            var prefix = CommandTypesEnum.ChangeServer.GetDescription();
            InlineKeyboardButton[][] buttons = new[]
            {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("سرور آلمان", $"{prefix}o1"),
                InlineKeyboardButton.WithCallbackData("سرور آمریکا", $"{prefix}o2"),
                InlineKeyboardButton.WithCallbackData("سرور فنلاند", $"{prefix}o3")
            }
        };

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);
            return inlineKeyboardMarkup;
        }

        public ReplyKeyboardMarkup GenerateKeyBoard(object T = null)
        {
            KeyboardButton[] row1 = { new KeyboardButton(CommandsEnum.Start.GetDescription())};
            _replyKeyboardMarkup = new ReplyKeyboardMarkup(row1)
            {
                OneTimeKeyboard = true,
                ResizeKeyboard = true
            };
            return _replyKeyboardMarkup;
        }

        public ValidateKeyboard ValidationKeyBoard(object T = null)
        {
            var User = (T as UserChangeServer);
            var UserIsValid = _userRepository.GetUserEntityDocument(User.username, User.password).Result;
            if (UserIsValid != null)
                return new ValidateKeyboard { isValid = true };

            return new ValidateKeyboard { isValid = false, Message = "اطلاعات کاربر وارد شده معتبر نمی باشد، لطفا مجددا نام کاربری را وارد نمایید." };
        }

        public ValidateKeyboard ValidationInlineKeyBoard(object T = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
