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
        ConvertAccountsApi
    }

    public enum ServerTypes
    {
        [Description("OC1")]
        Cisco,
        [Description("OP1")]
        OpenVPN
    }
}
