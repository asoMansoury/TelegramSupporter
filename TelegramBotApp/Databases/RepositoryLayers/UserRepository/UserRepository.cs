using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.Models;

namespace TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoContext _context;
        private const string _databaseName = "SoftEther";
        private const string _collectionName = "Users";
        public UserRepository()
        {
            _context = new MongoContext();
        }
        public async Task<UserEntity> GetUserEntityDocument(string username, string password)
        {
            UserEntity result = null;
            var usernameRegex = new BsonRegularExpression(username, "i");

            var userFilter = Builders<UserEntity>.Filter.Regex("username", usernameRegex) 
                                                            & Builders<UserEntity>.Filter.Eq("password", password)
                                                            & Builders<UserEntity>.Filter.Eq("removedFromServer",false);
            var data = await _context.GetFilteredDocument(_databaseName, _collectionName, userFilter);
            if (data != null && data.Count > 0)
                result = data[0];
            return result;
        }
    }
}
