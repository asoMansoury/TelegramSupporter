using Microsoft.AspNetCore.Components.Forms;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.ExternalResource;
using TeelgramBotSupporter.TelegramServices.ChangeService;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TeelgramBotSupporter.TelegramServices.TelegramKeyboards;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.TelegramServices.MainServices;
using TelegramBotApp.TelegramServices.ManagingServices;
using TelegramBotApp.TelegramServices.Models;

namespace TeelgramBotSupporter.TelegramServices
{
    internal class TelegramBotService : ITelegramBotService
    {
        private string _Token;
        private Thread _botThread;
        private Telegram.Bot.TelegramBotClient _client;
        public TelegramBotService(string Token)
        {
            _Token = Token;
            _botThread = new Thread(new ThreadStart(RunBot));
            _botThread.Start();

        }


        private void SendMessage(long chatId, string text, ReplyKeyboardMarkup replyKeyboardMarkup)
        {
            //_client.SendTextMessageAsync(chatId, text, ParseMode.Markdown, null, false, false, 0, false, replyKeyboardMarkup);
            _client.SendTextMessageAsync(chatId, text, ParseMode.Html, null, false, false, false, null, null, replyKeyboardMarkup);
            //_client.SendTextMessageAsync(chatId, text, null, ParseMode.Markdown, null, false, false, false, null, null, replyKeyboardMarkup);
        }

        private void SendMessageInline(long chatId, string text, InlineKeyboardMarkup replyKeyboardMarkup)
        {
            //_client.SendTextMessageAsync(chatId, text, ParseMode.Markdown, null, false, false, 0, false, replyKeyboardMarkup);
            _client.SendTextMessageAsync(chatId, text, ParseMode.Html, null, false, false, false, null, null, replyKeyboardMarkup);
            //_client.SendTextMessageAsync(chatId, text, null, ParseMode.Markdown, null, false, false, false, null, null, replyKeyboardMarkup);
        }

        public async void RunBot()
        {
            _client = new Telegram.Bot.TelegramBotClient(_Token);

            int offset = 0;
            Messages staticMessages = new Messages();
            Stack<Update> stackForMessage = new Stack<Update>();
            while (true)
            {
                Update[] updates = _client.GetUpdatesAsync(offset).Result;

                foreach (var item in updates)
                {

                    if (item.CallbackQuery != null)
                    {
                        try
                        {
                            if (stackForMessage.Count > 0)
                            {
                                if (item.CallbackQuery.Data.Contains(CommandTypesEnum.ChangeServer.GetDescription()))
                                {
                                    if (stackForMessage.Count >= 3)
                                    {
                                        stackForMessage.Pop();
                                        var userPasswordItem = stackForMessage.Pop();
                                        var userNameItem = stackForMessage.Pop();
                                        var serverCode = item.CallbackQuery.Data.Replace(CommandTypesEnum.ChangeServer.GetDescription(), "");
                                        ICommandQuery commandQuery = CommandsQueriesFactory.CreateCommandInstance(CommandTypesEnum.ChangeServer);
                                        var obj = new UserChangeServer
                                        {
                                            ServerCode = serverCode,
                                            password = userPasswordItem.Message.Text,
                                            username = userNameItem.Message.Text
                                        };

                                        var KeyBoardRestart = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                                        try
                                        {
                                            var result = await commandQuery.Execute(obj) as ChangeCiscoModel;
                                            if (result.serverType == ExternalResource.ServerTypes.Cisco)
                                            {
                                                SendMessage(item.CallbackQuery.Message.Chat.Id, (result as ChangeCiscoModel).serverUrl, KeyBoardRestart);
                                            }
                                            else
                                            {
                                                IFileReader fileReader = new FileReader();
                                                FileStream fileStream;
                                                lock (fileReader)
                                                {
                                                    fileStream = new FileStream(result.serverUrl, FileMode.Open);
                                                }
                                                var inputFile = new InputOnlineFile(fileStream);
                                                inputFile.FileName = result.serverUrl.Split("/").LastOrDefault();
                                                var result2 = await _client.SendDocumentAsync(item.CallbackQuery.Message.Chat.Id, inputFile);
                                                
                                                SendMessage(item.CallbackQuery.Message.Chat.Id, CommandsEnum.Start.GetDescription(), KeyBoardRestart);
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                            stackForMessage.Clear();
                                            offset = item.Id + 1;
                                            throw new Exception(ex.Message);
                                        }
                                        stackForMessage.Clear();
                                        offset = item.Id + 1;
                                    }
                                }
                                else if (item.CallbackQuery.Data.Contains(CommandTypesEnum.ConvertAccount.GetDescription()))
                                {
                                    if (stackForMessage.Count == 3)
                                    {
                                        var updateToConvert = stackForMessage.Pop();
                                        var userPasswordItem = stackForMessage.Pop();
                                        var userNameItem = stackForMessage.Pop();
                                        var newType = item.CallbackQuery.Data.Replace(CommandTypesEnum.ConvertAccount.GetDescription(), "");
                                        var obj = new UserChangeServer
                                        {
                                            newType = newType,
                                            password = userPasswordItem.Message.Text,
                                            username = userNameItem.Message.Text
                                        };

                                        var Keyboard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SelecteNewServer);
                                        try
                                        {
                                            SendMessageInline(item.CallbackQuery.Message.Chat.Id, staticMessages.ChangingServer(), Keyboard.GenerateInlineKeyBoard(obj));
                                        }
                                        catch (Exception ex)
                                        {

                                            offset = item.Id + 1;
                                            stackForMessage.Clear();
                                            throw new Exception(ex.Message);
                                        }
                                        stackForMessage.Push(userNameItem);
                                        stackForMessage.Push(userPasswordItem);
                                        stackForMessage.Push(updateToConvert);
                                        stackForMessage.Push(item);
                                        offset = item.Id + 1;
                                    }
                                }
                                else if (item.CallbackQuery.Data.Contains(CommandTypesEnum.SelectServerAccount.GetDescription()))
                                {
                                    if (stackForMessage.Count == 4)
                                    {
                                        var newServerType = stackForMessage.Pop();
                                        var updateToConvert = stackForMessage.Pop().Message;
                                        var userPasswordItem = stackForMessage.Pop().Message;
                                        var userNameItem = stackForMessage.Pop().Message;
                                        var newType = newServerType.CallbackQuery.Data.Replace(CommandTypesEnum.ConvertAccount.GetDescription(), "");
                                        var serverCode = item.CallbackQuery.Data.Replace(CommandTypesEnum.SelectServerAccount.GetDescription(), "");
                                        var obj = new UserChangeServer
                                        {
                                            newType = newType,
                                            ServerCode = serverCode,
                                            password = userPasswordItem.Text,
                                            username = userNameItem.Text,

                                        };

                                        var Keyboard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start);
                                        ICommandQuery commandQuery = CommandsQueriesFactory.CreateCommandInstance(CommandTypesEnum.SelectServerAccount);
                                        var result = await commandQuery.Execute(obj) as ChangeCiscoModel;
                                        if (result.serverType == ExternalResource.ServerTypes.Cisco)
                                        {
                                            SendMessage(item.CallbackQuery.Message.Chat.Id, (result as ChangeCiscoModel).serverUrl, Keyboard.GenerateKeyBoard());
                                        }
                                        else
                                        {
                                            IFileReader fileReader = new FileReader();
                                            FileStream fileStream;
                                            lock (fileReader)
                                            {
                                                fileStream = new FileStream(result.serverUrl, FileMode.Open);
                                            }
                                            var inputFile = new InputOnlineFile(fileStream);
                                            inputFile.FileName = result.serverUrl.Split("/").LastOrDefault();
                                            var result2 = await _client.SendDocumentAsync(item.CallbackQuery.Message.Chat.Id, inputFile);
                                            SendMessage(item.CallbackQuery.Message.Chat.Id, staticMessages.SuccessOperation(), Keyboard.GenerateKeyBoard());
                                        }
                                        stackForMessage.Clear();
                                        offset = item.Id + 1;
                                    }
                                }
                                else if (item.CallbackQuery.Data.Contains(CommandTypesEnum.DownloadCiscoApp.GetDescription()))
                                {
                                    if (stackForMessage.Count == 4)
                                    {
                                        var selectedAppType = stackForMessage.Pop();
                                        var updateToConvert = stackForMessage.Pop().Message;
                                        var userPasswordItem = stackForMessage.Pop().Message;
                                        var userNameItem = stackForMessage.Pop().Message;
                                        var newType = selectedAppType.Message.Text;
                                        var serverCode = item.CallbackQuery.Data.Replace(CommandTypesEnum.DownloadCiscoApp.GetDescription(), "");

                                        IFileReader fileReader = new FileReader();
                                        FileStream fileStream;
                                        string fileName = string.Empty;
                                        var Keyboard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start);
                                        if (serverCode == CiscoAppTypes.IPhone.GetDescription())
                                        {
                                            fileName = "https://apps.apple.com/us/app/cisco-secure-client/id1135064690?platform=iphone";
                                            SendMessage(item.CallbackQuery.Message.Chat.Id, fileName, Keyboard.GenerateKeyBoard());
                                            SendMessage(item.CallbackQuery.Message.Chat.Id, staticMessages.IphoneLinkDownlad(), Keyboard.GenerateKeyBoard());
                                        }
                                        else
                                        {
                                            lock (fileReader)
                                            {

                                                if (serverCode == CiscoAppTypes.Android.GetDescription())
                                                    fileName = "./ciscoandroid.apk";
                                                else if (serverCode == CiscoAppTypes.Wdinwos.GetDescription())
                                                    fileName = "./ciscowindows.msi";
                                                fileStream = new FileStream(fileName, FileMode.Open);
                                            }
                                            var inputFile = new InputOnlineFile(fileStream);
                                            inputFile.FileName = fileName;

                                            SendMessage(item.CallbackQuery.Message.Chat.Id, staticMessages.IsLoadingFile(), Keyboard.GenerateKeyBoard());
                                            var result2 = await _client.SendDocumentAsync(item.CallbackQuery.Message.Chat.Id, inputFile);

                                        }

                                        stackForMessage.Clear();
                                        offset = item.Id + 1;
                                    }
                                }
                                else if (item.CallbackQuery.Data.Contains(CommandTypesEnum.DownloadOpenVPNApp.GetDescription()))
                                {
                                    if (stackForMessage.Count == 4)
                                    {
                                        var selectedAppType = stackForMessage.Pop();
                                        var updateToConvert = stackForMessage.Pop().Message;
                                        var userPasswordItem = stackForMessage.Pop().Message;
                                        var userNameItem = stackForMessage.Pop().Message;
                                        var newType = selectedAppType.Message.Text;
                                        var serverCode = item.CallbackQuery.Data.Replace(CommandTypesEnum.DownloadOpenVPNApp.GetDescription(), "");

                                        IFileReader fileReader = new FileReader();
                                        FileStream fileStream;
                                        string fileName = string.Empty;
                                        var Keyboard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start);
                                        if (serverCode == OpenVPNAppTypes.IPhone.GetDescription())
                                        {
                                            fileName = "https://apps.apple.com/us/app/openvpn-connect-openvpn-app/id590379981";
                                            SendMessage(item.CallbackQuery.Message.Chat.Id, fileName, Keyboard.GenerateKeyBoard());
                                            SendMessage(item.CallbackQuery.Message.Chat.Id, staticMessages.IphoneLinkDownlad(), Keyboard.GenerateKeyBoard());
                                        }
                                        else
                                        {
                                            lock (fileReader)
                                            {
                                                if (serverCode == OpenVPNAppTypes.Android.GetDescription())
                                                    fileName = "./openvpn-android.apk";
                                                else if (serverCode == OpenVPNAppTypes.Wdinwos.GetDescription())
                                                    fileName = "./openvpn-windows.msi";

                                                fileStream = new FileStream(fileName, FileMode.Open);
                                            }
                                            var inputFile = new InputOnlineFile(fileStream);
                                            inputFile.FileName = fileName;

                                            SendMessage(item.CallbackQuery.Message.Chat.Id, staticMessages.IsLoadingFile(), Keyboard.GenerateKeyBoard());
                                            var result2 = await _client.SendDocumentAsync(item.CallbackQuery.Message.Chat.Id, inputFile);

                                        }



                                        stackForMessage.Clear();
                                        offset = item.Id + 1;
                                    }
                                }
                                else
                                {
                                    offset = item.Id + 1;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                            Console.WriteLine("Erroorrrrrrr---------------------", ex.Message);
                           
                            SendMessage(item.CallbackQuery.Message.Chat.Id, "مشکلی در ربات پیش آمده است لطفا پیغام زیر را برای مدیریت ربات ارسال کنید", KeyBoard);
                            SendMessage(item.CallbackQuery.Message.Chat.Id, ex.Message, KeyBoard);
                            stackForMessage.Clear();
                            offset = item.Id + 1;
                        }

                    }
                    if (item.Message == null)
                    {
                        offset = item.Id + 1;
                        continue;
                    }
                    var message = item.Message.Text;
                    var from = item.Message.From;
                    var chatId = item.Message.Chat.Id;
                    try
                    {
                        if (message.ToLower().Contains(CommandsEnum.Start.GetDescription().ToLower()) || message.ToLower().Contains("again") || message.ToLower() == "/start" || message.ToLower() == "/شروع")
                        {
                            stackForMessage.Clear();
                            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                            offset = item.Id + 1;
                            SendMessage(chatId, staticMessages.WelcomPack(from.Username), KeyBoard);

                        }
                        else if (message.ToLower().Contains(CommandsEnum.SendUsername.GetDescription().ToLower()))
                        {
                            stackForMessage.Push(item);
                            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SendUsername).GenerateKeyBoard();
                            offset = item.Id + 1;
                            SendMessage(chatId, staticMessages.AfterSendingUserName(), KeyBoard);

                        }
                        else if (message.ToLower().Contains(CommandsEnum.SendPassword.GetDescription().ToLower()))
                        {
                            stackForMessage.Push(item);
                            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SendPassword).GenerateKeyBoard();
                            offset = item.Id + 1;
                            SendMessage(chatId, staticMessages.AfterSendingPassword(), KeyBoard);
                        }
                        else if (message.ToLower().Contains(CommandsEnum.RemainTime.GetDescription().ToLower()))
                        {
                            IBaseService baseService = new RemainTimeService(_client);
                            await baseService.Run(stackForMessage, item, message, from, chatId);
                            offset = item.Id + 1;
                        }
                        else if (message.ToLower().Contains(CommandsEnum.Restart.GetDescription().ToLower()))
                        {
                            IBaseService baseService = new RestartAccountService(_client);
                            await baseService.Run(stackForMessage, item, message, from, chatId);
                            offset = item.Id + 1;
                        }
                        else if (message.ToLower().Contains(CommandsEnum.ChangeServer.GetDescription().ToLower()))
                        {
                            IBaseService baseService = new ChangeServerService(_client);
                            await baseService.Run(stackForMessage, item, message, from, chatId);
                            offset = item.Id + 1;
                        }
                        else if (message.ToLower().Contains(CommandsEnum.ConvertAccount.GetDescription().ToLower()))
                        {
                            IBaseService baseService = new ConvertAccountService(_client);
                            await baseService.Run(stackForMessage, item, message, from, chatId);
                            offset = item.Id + 1;
                        }
                        else if (message.ToLower().Contains(CommandsEnum.DownloadSoftwares.GetDescription().ToLower()))
                        {
                            if (stackForMessage.Count >= 2)
                            {

                                IBaseService baseService = new DownloadServices(_client);
                                await baseService.Run(stackForMessage, item, message, from, chatId);
                            }
                            offset = item.Id + 1;

                        }
                        else if (message.ToLower().Contains(CommandsEnum.Cisco.GetDescription().ToLower()))
                        {
                            if (stackForMessage.Count >= 2)
                            {

                                IBaseService baseService = new DownloadCiscoApp(_client);
                                await baseService.Run(stackForMessage, item, message, from, chatId);
                            }
                            offset = item.Id + 1;

                        }
                        else if (message.ToLower().Contains(CommandsEnum.OpenVPN.GetDescription().ToLower()))
                        {
                            if (stackForMessage.Count >= 2)
                            {

                                IBaseService baseService = new DownloadOpenVPNApp(_client);
                                await baseService.Run(stackForMessage, item, message, from, chatId);
                            }
                            offset = item.Id + 1;

                        }
                        else if (message.ToLower().Contains(CommandsEnum.ChangePassword.GetDescription().ToLower()))
                        {
                            var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.ChangePassword).GenerateKeyBoard();

                            SendMessage(chatId, staticMessages.ChangingPassword(), KeyBoard);
                            stackForMessage.Push(item);
                            offset = item.Id + 1;
                        }
                        else if (message.ToLower().Contains(CommandsEnum.CommitChangePassword.GetDescription().ToLower()))
                        {
                            IBaseService baseService = new CommitChangePasswordService(_client);
                            await baseService.Run(stackForMessage, item, message, from, chatId);
                            offset = item.Id + 1;
                        }
                        else
                        {
                            if (stackForMessage.Count == 0)
                            {
                                var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SendUsername).GenerateKeyBoard();
                                SendMessage(chatId, staticMessages.AfterSendingUserName(), KeyBoard);
                                stackForMessage.Push(item);

                            }
                            else if (stackForMessage.Count == 1)
                            {
                                var username = stackForMessage.Peek().Message.Text;
                                var password = item.Message.Text;
                                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                                {
                                    var KeyBoardRestart = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                                    SendMessage(chatId, staticMessages.WrongUserName(username), KeyBoardRestart);
                                    stackForMessage.Clear();
                                }
                                else
                                {
                                    var changeServerModel = new UserChangeServer
                                    {
                                        username = username,
                                        password = password,
                                        telegramID = from.Username,
                                        chatId = item.Message.Chat.Id.ToString()

                                    };
                                    var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.SendPassword);
                                    var validateUserKeyboard = KeyBoard.ValidationKeyBoard(changeServerModel);
                                    if (validateUserKeyboard.isValid == false)
                                    {

                                        var mainKeyboard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                                        SendMessage(chatId, validateUserKeyboard.Message, mainKeyboard);
                                        stackForMessage.Clear();
                                        offset = item.Id + 1;
                                        continue;
                                    }
                                    IUserServices userServices = new UserServices(_client,new UserRepository());
                                    SendMessage(chatId, staticMessages.AfterSendingPassword(), KeyBoard.GenerateKeyBoard());
                                    SendMessage(chatId, staticMessages.ShowingRemainTime(userServices.GetUserEntity(username, password).expires), KeyBoard.GenerateKeyBoard());
                                    await userServices.SendCurrentServerInformation(item, "", from, chatId,username,password);
                                    stackForMessage.Push(item);
                                }
                            }
                            else if (stackForMessage.Count == 3)
                            {
                                var lastMessage = stackForMessage.Pop();
                                if (lastMessage.Message.Text == CommandsEnum.ChangePassword.GetDescription())
                                {
                                    var userPassworItem = stackForMessage.Pop();
                                    var userNameItem = stackForMessage.Pop();

                                    var model = new UserChangePassword
                                    {
                                        username = userNameItem.Message.Text,
                                        newpassword = message,
                                        password = userPassworItem.Message.Text,
                                        telegramID = from.Username,
                                        chatId = item.Message.Chat.Id.ToString()
                                    };
                                    ICommandQuery commandQuery = CommandsQueriesFactory.CreateCommandInstance(CommandTypesEnum.ChangePassword);
                                    commandQuery.Execute(model);
                                    var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                                    SendMessage(chatId, staticMessages.SuccessOperation(), KeyBoard);
                                    stackForMessage.Clear();
                                }
                            }

                            offset = item.Id + 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        var KeyBoard = TelegramKeyboardGeneratorFactory.GenerateKeyBoard(CommandsEnum.Start).GenerateKeyBoard();
                        SendMessage(chatId, "مشکلی در ربات پیش آمده است لطفا پیغام زیر را برای مدیریت ربات ارسال کنید", KeyBoard);
                        SendMessage(chatId, ex.Message, KeyBoard);
                        stackForMessage.Clear();
                        offset = item.Id + 1;
                    }
                }

            }
        }

        public void StopBot()
        {
            _botThread.Abort();
        }
    }
}
