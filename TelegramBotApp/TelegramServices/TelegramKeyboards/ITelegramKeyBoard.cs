using System.Threading.Tasks;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal interface ITelegramKeyBoard
    {
        ReplyKeyboardMarkup GenerateKeyBoard(object T = null);

        ValidateKeyboard ValidationKeyBoard(object T = null);

        InlineKeyboardMarkup GenerateInlineKeyBoard(object T = null);

        ValidateKeyboard ValidationInlineKeyBoard(object T = null);
    }
}