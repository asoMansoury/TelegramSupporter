using TeelgramBotSupporter.Databases.Models;
using TeelgramBotSupporter.TelegramServices.Const;

namespace TelegramBotApp.TelegramServices.Utility
{
    public static class CommonUtility
    {
        public static string ShowRemainTime(string expiredTime)
        {
            var ExpireTime = DateTime.Parse(expiredTime);
            var remainDays = ExpireTime.Subtract(DateTime.Now);
            Messages staticMessages = new Messages();
            return staticMessages.CalculateRemainDay(remainDays.Days, ExpireTime);
        }
    }
}
