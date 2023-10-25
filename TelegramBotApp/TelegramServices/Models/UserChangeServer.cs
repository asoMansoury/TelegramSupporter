using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices.Models
{
    public class UserChangeServer
    {
        public string ServerCode { get; set; }
        public string username { get; set; }
        public string newType { get; set; }
        public string password { get; set; }
        public string telegramID { get; set; }
        public string chatId { get; set; }
        public string CurrentServercode { get; set; }

    }
}
