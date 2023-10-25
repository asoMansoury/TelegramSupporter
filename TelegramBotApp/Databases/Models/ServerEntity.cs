using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.Databases.Models
{
    public class ServerEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public string password { get; set; }
        public string vpncmdpassword { get; set; }
        public string HubName { get; set; }
        public string username { get; set; }
        public string policy { get; set; }
        public int isactive { get; set; }
        public string ovpnpurl { get; set; }
        public string ovpnurl { get; set; }
        public bool isremoved { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public bool usedForTest { get; set; }
        public string servercode { get; set; }
        public string title { get; set; }
        public int ciscoPort { get; set; }
        public string iranIP { get; set; }
        public string ciscourl { get; set; }
        public string ovpTelegramPath { get; set; }

    }
}
