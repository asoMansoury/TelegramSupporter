using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices
{
    internal interface ITelegramBotService
    {
        void RunBot();
        void StopBot();
    }
}
