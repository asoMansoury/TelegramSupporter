using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeelgramBotSupporter.TelegramServices.Const
{

    public enum CommandsEnum
    {
        [Description("شروع مجدد")]
        Start,
        [Description("راه اندازی مجدد")]
        Restart,
        [Description("ارسال نام اکانت")]
        SendUsername,
        [Description("ارسال کلمه عبور")]
        SendPassword,
        [Description("تغییر سرور")]
        ChangeServer,
        [Description("تغییر سرویس")]
        ConvertAccount,
        [Description("انتخاب سرور جدید")]
        SelecteNewServer,
        [Description("تغییر کلمه عبور")]
        ChangePassword,
        [Description("ثبت تغییرات")]
        CommitChangePassword,
        [Description("دانلود نرم افزار")]
        DownloadSoftwares,
        [Description("اپن وی پی ان")]
        OpenVPN,
        [Description("سیسکو")]
        Cisco,
        [Description("آندروید")]
        Android,
        [Description("آیفون")]
        IPhone,
        [Description("ویندوز")]
        Windows,
        [Description("مشاهده اعتبار")]
        RemainTime
    }
}
