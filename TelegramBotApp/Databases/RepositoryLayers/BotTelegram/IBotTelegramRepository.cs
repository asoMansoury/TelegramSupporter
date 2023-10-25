using MongoDB.Driver;
using TeelgramBotSupporter.Databases.Models;

namespace TelegramBotApp.Databases.RepositoryLayers.BotTelegram
{
    public interface IBotTelegramRepository
    {
        Task<List<BotTelegramEntity>> GetAllDocuments();
        Task<List<BotTelegramEntity>> GetFilteredDocument(string username);
        Task<List<BotTelegramEntity>> GetFilteredDocument( string username, string telegramId, string chatId);
        Task UpdateDocument( FilterDefinition<BotTelegramEntity> filter, UpdateDefinition<BotTelegramEntity> document);
        Task CreateDocument(string username, string telegramId, string chatId);
        Task DeleteDocument( FilterDefinition<BotTelegramEntity> filter);
    }
}
