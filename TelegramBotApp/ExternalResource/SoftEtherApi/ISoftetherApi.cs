using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeelgramBotSupporter.TelegramServices.Models;

namespace TeelgramBotSupporter.ExternalResource.SoftEtherApi
{
    internal interface ISoftetherApi
    {
        Task<T> ChangeUserServer<T>(UserChangeServer model,string Token, SoftEtherApiEnum api);

        Task ChangeUserPassword(UserChangePassword model, string Token, SoftEtherApiEnum api);

        Task<T> ConvertAccount<T>(UserChangeServer model, string Token, SoftEtherApiEnum api);

        Task RestartUserAccount(UserChangeServer model, string Token, SoftEtherApiEnum api);
    }
}
