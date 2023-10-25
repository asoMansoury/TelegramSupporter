using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.Databases.DataLayer
{
    public class MongoContext : IMongoContext
    {

        public IMongoDatabase GetMongoDatabaseInstance(string dbName)
        {
            var connString = "mongodb://administrator:lasoA45Egg99@135.181.107.1:27017/admin?retryWrites=true&serverSelectionTimeoutMS=5000&connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-256";
            //var connString = "mongodb+srv://asomansoury:lasoA45Egg99@cluster0.luuqh0d.mongodb.net/admin?retryWrites=true&replicaSet=atlas-mbhaa9-shard-0&readPreference=primary&srvServiceName=mongodb&connectTimeoutMS=10000&authSource=admin&authMechanism=SCRAM-SHA-1";
            var client = new MongoClient(connString);
            var db = client.GetDatabase(dbName);
            return db;
        }

        private IMongoCollection<T> GetCollection<T>(string dbName, string collectionName)
        {
            return GetMongoDatabaseInstance(dbName).GetCollection<T>(collectionName);
        }

        public async Task CreateDocument<T>(string dbName, string collectionName, T document)
        {
            await GetCollection<T>(dbName, collectionName).InsertOneAsync(document);
        }

        public async Task DeleteDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter)
        {
            await GetCollection<T>(dbName,collectionName).DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllDocuments<T>(string dbName, string collectionName)
        {
            var collection = GetCollection<T>(dbName, collectionName);
            return await collection.Find(x => true).ToListAsync();
        }

        public async Task<List<T>> GetFilteredDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter)
        {
            return await GetCollection<T>(dbName, collectionName).Find(filter).ToListAsync();
        }

        public async Task UpdateDocument<T>(string dbName, string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> document)
        {
            await GetCollection<T>(dbName, collectionName).UpdateOneAsync(filter, document);
        }
    }
}
