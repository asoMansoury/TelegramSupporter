namespace TeelgramBotSupporter.TelegramServices.Models
{
    public class UserChangePassword
    {
        public string username { get; set; }
        public string password { get; set; }
        public string newpassword { get; set; }
        public string chatId { get; set; }
        public string telegramID { get; set; }
    }
}
