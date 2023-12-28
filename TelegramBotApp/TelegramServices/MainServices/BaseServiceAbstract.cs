using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotApp.TelegramServices.MainServices
{
    public abstract class BaseServiceAbstract
    {
        protected readonly Telegram.Bot.TelegramBotClient _client;
        public BaseServiceAbstract(TelegramBotClient client)
        {
            _client = client; 
        }

        protected void SendMessage(long chatId, string text, ReplyKeyboardMarkup replyKeyboardMarkup)
        {
            //_client.SendTextMessageAsync(chatId, text, ParseMode.Markdown, null, false, false, 0, false, replyKeyboardMarkup);
            _client.SendTextMessageAsync(chatId, text, ParseMode.Html, null, false, false, false, null, null, replyKeyboardMarkup);
            //_client.SendTextMessageAsync(chatId, text, null, ParseMode.Markdown, null, false, false, false, null, null, replyKeyboardMarkup);
        }

        protected void SendMessageInline(long chatId, string text, InlineKeyboardMarkup replyKeyboardMarkup)
        {
            //_client.SendTextMessageAsync(chatId, text, ParseMode.Markdown, null, false, false, 0, false, replyKeyboardMarkup);
            _client.SendTextMessageAsync(chatId, text, ParseMode.Html, null, false, false, false, null, null, replyKeyboardMarkup);
            //_client.SendTextMessageAsync(chatId, text, null, ParseMode.Markdown, null, false, false, false, null, null, replyKeyboardMarkup);
        }
    }
}
