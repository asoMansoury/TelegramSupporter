using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices.Const
{
    public class Messages
    {
        public const string WelcomePack = "";
        public string WelcomPack(string username)
        {
            StringBuilder sb = new StringBuilder();
            if(username!=null)
                sb.AppendLine($"کاربر ${username.Replace("$", "")} به ربات پشتیبانی خوش آمدید.".ToString().Replace("$", ""));
            else
                sb.AppendLine($"کاربر گرامی به ربات پشتیبانی خوش آمدید.".ToString());
            sb.AppendLine("لطفا نام کاربری خود را وارد نمایید");
            return sb.ToString() ;
        }

        public string WrongUserName(string username)
        {
            StringBuilder sb = new StringBuilder();
            if(username!=null)
                sb.AppendLine($"اطلاعات کاربر ${username.Replace("$", "")} به درستی وارد نشده است.".ToString().Replace("$", ""));
            sb.AppendLine("لطفا نام کاربری خود را وارد نمایید");
            return sb.ToString();
        }

        public string AfterSendingUserName()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"لطفا کلمه عبور اکانت خود را وارد نمایید.");

            return sb.ToString();
        }

        public string AfterSendingPassword()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"لطفا عملیات مورد نظر خود را انتخاب نمایید.");

            return sb.ToString();
        }

        public string ChangingServer()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"لطفا از بین سرور های زیر سرور مورد نظر خود را انتخاب نمایید");

            return sb.ToString();
        }


        public string ConvertAccountMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"لطفا از بین سرویس های زیر سرویس مورد نظر خود را انتخاب نمایید");

            return sb.ToString();
        }

        public string ChangingPassword()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"لطفا کلمه عبور جدید برای اکانت خود را وارد نمایید(انتخاب کاربر جدید روی شروع مجدد کلیک کنید).");

            return sb.ToString();
        }
        public string UnexpectedError()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"عملیات به دلایل نامعلوم انجام نگردید، لطفا بعدا تلاش نمایید.");

            return sb.ToString();
        }
        public string SuccessOperation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"عملیات با موفقیت انجام گردید.");

            return sb.ToString();
        }

        public string SuccessChangingServerOperation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"عملیات تغییر سرور با موفقیت انجام گردید. اطلاعات اکانت به ایمیل شما ارسال گردید.");

            return sb.ToString();
        }

        public string ChangingServer2()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"برای انتخاب کاربر دیگر روی شروع مجدد کلیک کنید");

            return sb.ToString();
        }
    }
}
