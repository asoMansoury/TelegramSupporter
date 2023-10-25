using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.TelegramServices.ChangeService;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardCommitChangePassword : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        public TelegramKeyboardCommitChangePassword()
        {
            _serverCollectionRepository = new ServerCollectionRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            var prefix = CommandTypesEnum.ChangeServer.GetDescription();
            InlineKeyboardButton[][] buttons = new[]
            {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("سرور آلمان", $"{prefix}o1")
            }
        };

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);
            return inlineKeyboardMarkup;
        }

        public ReplyKeyboardMarkup GenerateKeyBoard(object T = null)
        {
            KeyboardButton[] row1 = { new KeyboardButton(CommandsEnum.Start.GetDescription()) };
            _replyKeyboardMarkup = new ReplyKeyboardMarkup(row1)
            {
                OneTimeKeyboard = true,
                ResizeKeyboard = true
            };
            return _replyKeyboardMarkup;
        }

        public ValidateKeyboard ValidationKeyBoard(object T = null)
        {
            throw new System.NotImplementedException();
        }

        public ValidateKeyboard ValidationInlineKeyBoard(object T = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
