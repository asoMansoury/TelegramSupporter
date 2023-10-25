using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.Databases.Models
{
    public class TelegramTokenEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string token { get; set; }
    }
}
