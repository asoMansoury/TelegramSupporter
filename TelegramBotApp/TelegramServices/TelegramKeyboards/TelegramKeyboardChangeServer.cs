using System;
using System.Linq;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.TelegramServices.ChangeService;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardChangeServer : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;

        public TelegramKeyboardChangeServer()
        {
            _serverCollectionRepository = new ServerCollectionRepository();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            var prefix = CommandTypesEnum.ChangeServer.GetDescription();
            var model = (T as UserChangeServer);
            var currentUser = _userRepository.GetUserEntityDocument(model.username,model.password).Result;

            var servers = _serverCollectionRepository.GetFilteredTypeDocument(currentUser.type).Result
                                                       .Where(z=>z.servercode!=currentUser.currentservercode).ToList();

            float result = (float)servers.Count / 4;
            Int32 rowLength = (int)Math.Ceiling(result);
            int maxColumns = rowLength * 4;

            InlineKeyboardButton[][] buttons = new InlineKeyboardButton[rowLength][];

            int rowNum = 0;
            for (int i = 0; i < rowLength; i++)
            {
                buttons[i] = new InlineKeyboardButton[3];
                var column = 4;
                if(i+1 == rowLength)
                {
                    column = Math.Abs((maxColumns - servers.Count) - 4);
                }//سطر آخر است و باید ببینیم آیا سطر آخر هم باید 4ستونه باشد یا کمتر!

                InlineKeyboardButton[] data = new InlineKeyboardButton[column];
                for (int j = 0; j < column; j++)
                {
                    if (rowNum < servers.Count)
                    {
                        data[j] = InlineKeyboardButton.WithCallbackData(servers[rowNum].title, $"{prefix}{servers[rowNum].servercode}");
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
            throw new NotImplementedException();
        }

        public ValidateKeyboard ValidationInlineKeyBoard(object T = null)
        {
            var User = (T as UserChangeServer);
            var UserIsValid = _userRepository.GetUserEntityDocument(User.username,User.password).Result;
            if (UserIsValid != null)
            {
                _botTelegramRepository.CreateDocument(User.username, User.telegramID, User.chatId);
                return new ValidateKeyboard { isValid = true };
            }


            return new ValidateKeyboard { isValid=false, Message="اطلاعات کاربر وارد شده معتبر نمی باشد، لطفا مجددا نام کاربری را وارد نمایید." };
        }
    }
}
