using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.Databases.Models
{
    public class BotTelegramEntity
    {
        [BsonId]
        public Object _id { get; set; }
        public string username { get; set; }
        public string telegramId { get; set; }
        public string chatId { get; set; }
    }
}
