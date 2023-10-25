using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TeelgramBotSupporter.Databases.Models
{
    public class CustomerEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string agentcode { get; set; }
        public bool isAdmin { get; set; }
    }
}
