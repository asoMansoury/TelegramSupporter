using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.Databases.DataLayer
{
    public interface IMongoContext
    {
        Task<List<T>> GetAllDocuments<T>(string dbName, string collectionName);
        Task<List<T>> GetFilteredDocument<T>(string dbName,string collectionName,FilterDefinition<T> filter);
        Task UpdateDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter,UpdateDefinition<T> document);
        Task CreateDocument<T>(string dbName,string collectionName,T document);
        Task DeleteDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter);
    }
}
