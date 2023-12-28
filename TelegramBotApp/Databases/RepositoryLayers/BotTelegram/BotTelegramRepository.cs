using MongoDB.Bson;
using MongoDB.Driver;
using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.Models;

namespace TelegramBotApp.Databases.RepositoryLayers.BotTelegram
{
    public class BotTelegramRepository : IBotTelegramRepository
    {
        private readonly IMongoContext _context;
        private const string _databaseName = "SoftEther";
        private const string _collectionName = "BotTelegram";
        public BotTelegramRepository()
        {
            _context = new MongoContext();
        }
        public async Task CreateDocument(string username, string telegramId, string chatId)
        {
            var botEntities = await GetFilteredDocument(username, telegramId);
            if (botEntities.Count > 0)
            {
                botEntities[0].chatId = chatId;
                var filter = Builders<BsonDocument>.Filter.Eq("username", username) &
                             Builders<BsonDocument>.Filter.Eq("telegramId", telegramId);
                if (!string.IsNullOrEmpty(chatId))
                {
                    var update = Builders<BsonDocument>.Update.Set("chatId", chatId);
                    await _context.UpdateDocument(_databaseName, _collectionName, filter, update);
                }
                return;
            }

            var BotTelegramEntity = new BotTelegramEntity()
            {
                _id = ObjectId.GenerateNewId(),
                telegramId = telegramId,
                username = username,
                chatId = chatId
            };
            _context.CreateDocument<BotTelegramEntity>(_databaseName, _collectionName, BotTelegramEntity);
        }

        public Task DeleteDocument(FilterDefinition<BotTelegramEntity> filter)
        {
            throw new NotImplementedException();
        }

        public Task<List<BotTelegramEntity>> GetAllDocuments()
        {
            throw new NotImplementedException();
        }

        public async Task<List<BotTelegramEntity>> GetFilteredDocument(string username, string telegramId, string chatId = "")
        {

            var filter = Builders<BotTelegramEntity>.Filter.Eq("username", username) &
                                                    Builders<BotTelegramEntity>.Filter.Eq("telegramId", telegramId);
            var collection = await _context.GetFilteredDocument<BotTelegramEntity>(_databaseName, _collectionName, filter);
            return collection.ToList();
        }

        public Task UpdateDocument(FilterDefinition<BotTelegramEntity> filter, UpdateDefinition<BotTelegramEntity> document)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BotTelegramEntity>> GetFilteredDocument(string username)
        {
            var filter = Builders<BotTelegramEntity>.Filter.Eq("username", username);
            var collection = await _context.GetFilteredDocument<BotTelegramEntity>(_databaseName, _collectionName, filter);
            return collection.ToList();
        }
    }
}
