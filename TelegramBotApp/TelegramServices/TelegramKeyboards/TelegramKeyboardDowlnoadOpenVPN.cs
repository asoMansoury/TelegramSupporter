using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;
using TeelgramBotSupporter.ExternalResource;
using TeelgramBotSupporter.TelegramServices.ChangeService;
using TelegramBotApp.TelegramServices.Models;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardDowlnoadOpenVPN : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;

        public TelegramKeyboardDowlnoadOpenVPN()
        {
            _serverCollectionRepository = new ServerCollectionRepository();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            var prefix = CommandTypesEnum.DownloadOpenVPNApp.GetDescription();
            var model = (T as UserChangeServer);

            var accounts = new List<AccountTypes>();
            accounts.Add(new AccountTypes
            {
                Code = OpenVPNAppTypes.Android.GetDescription(),
                Name = "آندروید"
            });
            accounts.Add(new AccountTypes
            {
                Code = OpenVPNAppTypes.IPhone.GetDescription(),
                Name = "آیفون"
            });
            accounts.Add(new AccountTypes
            {
                Code = OpenVPNAppTypes.Wdinwos.GetDescription(),
                Name = "ویندوز"
            });



            float result = (float)accounts.Count / 4;
            Int32 rowLength = (int)Math.Ceiling(result);
            int maxColumns = rowLength * 4;

            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rowLength][];

            int rowNum = 0;
            for (int i = 0; i < rowLength; i++)
            {
                buttons[i] = new InlineKeyboardButton[3];
                var column = 4;
                if (i + 1 == rowLength)
                {
                    column = Math.Abs((maxColumns - accounts.Count) - 4);
                }//سطر آخر است و باید ببینیم آیا سطر آخر هم باید 4ستونه باشد یا کمتر!

                InlineKeyboardButton[] data = new InlineKeyboardButton[column];
                for (int j = 0; j < column; j++)
                {
                    if (rowNum < accounts.Count)
                    {
                        data[j] = InlineKeyboardButton.WithCallbackData(accounts[rowNum].Name, $"{prefix}{accounts[rowNum].Code}");
                        rowNum++;
                    }

                }
                buttons[i] = data;
            }

            var inlineKeyboardMarkup = new InlineKeyboardMarkup(buttons);
            return inlineKeyboardMarkup;
        }

        public ReplyKeyboardMarkup GenerateKeyBoard(object T = null)
        {
            KeyboardButton[] row1 = { new KeyboardButton(CommandsEnum.Start.GetDescription()), new KeyboardButton(CommandsEnum.Android.GetDescription()), new KeyboardButton(CommandsEnum.IPhone.GetDescription()), new KeyboardButton(CommandsEnum.Windows.GetDescription()) };
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


    internal class TelegramKeyboardRemainTime : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;

        public TelegramKeyboardRemainTime()
        {
            _serverCollectionRepository = new ServerCollectionRepository();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            throw new Exception();
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
