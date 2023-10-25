using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.Models;

namespace TeelgramBotSupporter.Databases.RepositoryLayers.TelegramToken
{
    public interface ITelegramTokenRepository
    {
        Task<TelegramTokenEntity> GetTelegramTokenEntity();
    }
}
