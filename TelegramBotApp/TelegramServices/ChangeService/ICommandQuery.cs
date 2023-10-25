using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices.ChangeService
{
    public interface ICommandQuery
    {
        Task<object> Execute(object value);
    }
}
