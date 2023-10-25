using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.Databases.Models
{
    public class UserEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public Int32 usernumber { get; set; }
        public string tariffplancode { get; set; }
        public Int32 usercounter { get; set; }
        public bool isCreatedInServer { get; set; }
        public string policy { get; set; }
        public string email { get; set; }
        public string agentcode { get; set; }
        public bool isfromagent { get; set; }
        public string expires { get; set; }
        public bool isSendToOtherEmail { get; set; }
        public string currentservercode { get; set; }
        public string type { get; set; }
        public bool removedFromServer { get; set; }
        public bool isRevoked { get; set; }
        public string uuid { get; set; }
        public object serverId { get; set; }
        public object HubName { get; set; }
        public bool removedByAdmin { get; set; }
        public bool removedByAgent { get; set; }
        public bool removedBySubAgent { get; set; }
    }
}
