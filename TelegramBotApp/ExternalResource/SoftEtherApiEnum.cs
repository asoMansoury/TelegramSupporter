using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.ExternalResource
{
    public enum SoftEtherApiEnum
    {
        [Description("/api/telegram/changeserver/")]
        ChangeServerApi,
        [Description("/api/telegram/changeuserpassword/")]
        ChangeUserPassworApi,
        [Description("/api/telegram/convertAccounts/")]
        ConvertAccountsApi,
        [Description("/api/telegram/RestartUserConnection/")]
        RestartAccount
    }
}
