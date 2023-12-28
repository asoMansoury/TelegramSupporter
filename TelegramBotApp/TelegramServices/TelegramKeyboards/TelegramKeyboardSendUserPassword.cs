using System;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardSendUserPassword : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;
        public TelegramKeyboardSendUserPassword()
        {
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            throw new NotImplementedException();
        }

        public ReplyKeyboardMarkup GenerateKeyBoard(object T = null)
        {
            KeyboardButton[] row1 = { new KeyboardButton(CommandsEnum.ChangePassword.GetDescription()), 
                                      new KeyboardButton(CommandsEnum.ChangeServer.GetDescription()), 
                                        new KeyboardButton(CommandsEnum.ConvertAccount.GetDescription()), 
                                        new KeyboardButton(CommandsEnum.Start.GetDescription()) };
            KeyboardButton[] row2 = { new KeyboardButton(CommandsEnum.RemainTime.GetDescription()) };
            KeyboardButton[] row3 = { new KeyboardButton(CommandsEnum.DownloadSoftwares.GetDescription()) };
            _replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                row1,
                row2,
                row3
            })
            {
                OneTimeKeyboard = true,
                ResizeKeyboard = true
            };
            return _replyKeyboardMarkup;
        }

        public ValidateKeyboard ValidationInlineKeyBoard(object T = null)
        {
            throw new NotImplementedException();
        }

        public ValidateKeyboard ValidationKeyBoard(object T = null)
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
    }
}
