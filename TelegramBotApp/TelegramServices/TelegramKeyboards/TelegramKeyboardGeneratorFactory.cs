using System;
using System.Collections.Concurrent;
using TeelgramBotSupporter.TelegramServices.Const;
using Telegram.Bot.Types.ReplyMarkups;

namespace TeelgramBotSupporter.TelegramServices.TelegramKeyboards
{
    internal abstract class TelegramKeyboardGeneratorFactory
    {
        private static readonly ConcurrentDictionary<CommandsEnum, Lazy<ITelegramKeyBoard>> keyboardCache = new ConcurrentDictionary<CommandsEnum, Lazy<ITelegramKeyBoard>>();

        public static ITelegramKeyBoard GenerateKeyBoard(CommandsEnum command)
        {
            Lazy<ITelegramKeyBoard> lazyKeyboard = keyboardCache.GetOrAdd(command, CreateLazyKeyboard);
            return lazyKeyboard.Value;
        }

        private static Lazy<ITelegramKeyBoard> CreateLazyKeyboard(CommandsEnum command)
        {
            return new Lazy<ITelegramKeyBoard>(() =>
            {
                return command switch
                {
                    CommandsEnum.Start => new TelegramKeyboardStart(),
                    CommandsEnum.SendUsername => new TelegramKeyboardSendUserName(),
                    CommandsEnum.SendPassword => new TelegramKeyboardSendUserPassword(),
                    CommandsEnum.ConvertAccount=> new TelegramKeyboardConvertAccount(),
                    CommandsEnum.ChangeServer => new TelegramKeyboardChangeServer(),
                    CommandsEnum.SelecteNewServer=> new TelegramKeyboardSelectServer(),
                    CommandsEnum.ChangePassword => new TelegramKeyboardChangePassword(),
                    CommandsEnum.CommitChangePassword => new TelegramKeyboardCommitChangePassword(),
                    _ => null,
                };
            });
        }
    }
}