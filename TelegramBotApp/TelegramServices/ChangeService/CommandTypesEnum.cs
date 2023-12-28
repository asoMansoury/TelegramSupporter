using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices.ChangeService
{
    internal enum CommandTypesEnum
    {
        [Description("changeS-")]
        ChangeServer,
        [Description("changeP-")]
        ChangePassword,
        [Description("convertS-")]
        ConvertAccount,
        [Description("selectS-")]
        SelectServerAccount,
        [Description("CiscoApp-")]
        DownloadCiscoApp,
        [Description("OpenVPNApp-")]
        DownloadOpenVPNApp,
    }
}
