﻿using TeelgramBotSupporter.Databases.DataLayer;
using TeelgramBotSupporter.Databases.RepositoryLayers;
using TeelgramBotSupporter.Databases.RepositoryLayers.TelegramToken;
using TeelgramBotSupporter.Databases.RepositoryLayers.UserRepository;
using TeelgramBotSupporter.ExternalResource;
using TeelgramBotSupporter.ExternalResource.SoftEtherApi;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using TelegramBotApp.Databases.RepositoryLayers.BotTelegram;
using TelegramBotApp.TelegramServices.Models;

namespace TeelgramBotSupporter.TelegramServices.ChangeService
{
    internal class ConvertAccountCommands : ICommandQuery
    {
        private IMongoContext _mongoContext;
        private readonly ITelegramTokenRepository _telegramTokenRepository;
        private readonly ISoftetherApi _softetherApi;
        private readonly IUserRepository _userRepository;
        private readonly IBotTelegramRepository _botTelegramRepository;
        private readonly IServerCollectionRepository _serverCollectionRepository;
        public ConvertAccountCommands()
        {
            _mongoContext = new MongoContext();
            _telegramTokenRepository = new TelegramTokenRepository();
            _softetherApi = new SoftetherApi();
            _userRepository = new UserRepository();
            _botTelegramRepository = new BotTelegramRepository();
            _serverCollectionRepository = new ServerCollectionRepository();
        }
        public async Task<object> Execute(object value)
        {
            var obj = (UserChangeServer)value;
            var currentUser = _userRepository.GetUserEntityDocument(obj.username, obj.password).Result;
            obj.CurrentServercode = currentUser.currentservercode;
            var newServerCode = await _serverCollectionRepository.GetFilteredTypeDocument(currentUser.type, obj.ServerCode);
            var Token = await _telegramTokenRepository.GetTelegramTokenEntity();
            var result = await _softetherApi.ChangeUserServer<ChangeCiscoModel>(obj, Token.token, ExternalResource.SoftEtherApiEnum.ChangeServerApi);
            if (currentUser.type == ServerTypes.Cisco.GetDescription())
            {
                result.serverUrl = newServerCode.ciscourl + ":" + newServerCode.ciscoPort;
                result.serverType = ServerTypes.Cisco;
                return result;
            }
            else if (currentUser.type == ServerTypes.OpenVPN.GetDescription())
            {
                result.serverUrl = newServerCode.ovpTelegramPath;
                result.serverType = ServerTypes.OpenVPN;
            }
            return result;
        }
    }
}
