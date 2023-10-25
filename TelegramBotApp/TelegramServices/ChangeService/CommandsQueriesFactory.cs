using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices.ChangeService
{
    internal class CommandsQueriesFactory
    {
        private static readonly Lazy<ICommandQuery> changeServerCommand = new Lazy<ICommandQuery>(() => new ChangeServiceCommands());
        private static readonly Lazy<ICommandQuery> changePasswordCommand = new Lazy<ICommandQuery>(() => new ChangePasswordServiceCommands());
        private static readonly Lazy<ICommandQuery> convertAccountCommand = new Lazy<ICommandQuery>(() => new ConvertAccountCommands());
        private static readonly Lazy<ICommandQuery> selectSelcerTransferCommand = new Lazy<ICommandQuery>(() => new SelectServerForTransferCommands());
        public static ICommandQuery CreateCommandInstance(CommandTypesEnum commandTypes)
        {
            ICommandQuery commandQuery = null;
            switch (commandTypes)
            {
                case CommandTypesEnum.ChangeServer:
                    return changeServerCommand.Value;
                case CommandTypesEnum.ChangePassword:
                    return changePasswordCommand.Value;
                case CommandTypesEnum.ConvertAccount:
                    return convertAccountCommand.Value;
                case CommandTypesEnum.SelectServerAccount:
                    return selectSelcerTransferCommand.Value;
                default:
                    return null;
            }
        }
    }
}
