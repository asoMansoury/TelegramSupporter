using MongoDB.Driver.Core.Servers;
using TeelgramBotSupporter.ExternalResource;

namespace TelegramBotApp.TelegramServices.Models
{
    public class ChangeCiscoModel
    {
        public string name   { get; set; }
        public string serverUrl { get; set; }
        public string port { get; set; }
        public ServerTypes serverType { get; set; }
    }
}
