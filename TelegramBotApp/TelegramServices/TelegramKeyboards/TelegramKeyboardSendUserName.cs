using System;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal class TelegramKeyboardSendUserName : ITelegramKeyBoard
    {
        private ReplyKeyboardMarkup _replyKeyboardMarkup;
        public TelegramKeyboardSendUserName()
        {

        }

        public InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null)
        {
            throw new NotImplementedException();
        }

        public ReplyKeyboardMarkup GenerateKeyBoard(object T = null)
        {
            KeyboardButton[] row1 = {  new KeyboardButton(CommandsEnum.Start.GetDescription()) };
            _replyKeyboardMarkup = new ReplyKeyboardMarkup(row1)
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
            throw new NotImplementedException();
        }
    }
}
