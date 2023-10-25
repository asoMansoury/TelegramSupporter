using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.Models;

namespace TeelgramBotSupporter.Databases.RepositoryLayers.TelegramToken
{
    public class TelegramTokenRepository : ITelegramTokenRepository
    {
        private readonly IMongoContext _context;
        private const string _databaseName = "SoftEther";
        private const string _collectionName = "TelegramTokens";
        public TelegramTokenRepository()
        {
            _context = new MongoContext();
        }

        public async Task<TelegramTokenEntity> GetTelegramTokenEntity()
        {
            TelegramTokenEntity result = null;

            var data = await _context.GetAllDocuments<TelegramTokenEntity>(_databaseName, _collectionName);
            if (data != null && data.Count > 0)
                result = data[0];
            return result;
        }
    }
}
