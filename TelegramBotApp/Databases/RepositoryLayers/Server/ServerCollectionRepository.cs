using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.Models;

namespace TeelgramBotSupporter.Databases.RepositoryLayers
{
    internal class ServerCollectionRepository : IServerCollectionRepository
    {
        private readonly IMongoContext _context;
        private const string _databaseName = "SoftEther";
        private const string _collectionName ="Servers";
        public ServerCollectionRepository()
        {
            _context = new MongoContext();
        }
        public async Task<List<ServerEntity>> GetAllDocuments()
        {
            var collection =await _context.GetAllDocuments< ServerEntity>(_databaseName, _collectionName);
            return collection.ToList();
        }

        public async Task<List<ServerEntity>> GetFilteredTypeDocument(string type)
        {
            var filter = Builders<ServerEntity>.Filter
                .Eq("type", type) & Builders<ServerEntity>.Filter.Eq("isremoved", false);
            var collection =  await _context.GetFilteredDocument<ServerEntity>(_databaseName, _collectionName, filter);
            return collection.ToList();
        }

        public async Task<ServerEntity> GetFilteredTypeDocument(string type, string code)
        {
            var filter = Builders<ServerEntity>.Filter
                .Eq("type", type) & Builders<ServerEntity>.Filter.Eq("isremoved", false)
                & Builders<ServerEntity>.Filter.Eq("servercode",code);
            var collection = await _context.GetFilteredDocument<ServerEntity>(_databaseName, _collectionName, filter);
            return collection.ToList().FirstOrDefault();
        }

        public async Task<ServerEntity> GetAllFilteredTypeDocument(string type, string code)
        {
            var filter = Builders<ServerEntity>.Filter
                .Eq("type", type) & 
                Builders<ServerEntity>.Filter.Eq("servercode", code);
            var collection = await _context.GetFilteredDocument<ServerEntity>(_databaseName, _collectionName, filter);
            return collection.ToList().FirstOrDefault();
        }
    }
}
