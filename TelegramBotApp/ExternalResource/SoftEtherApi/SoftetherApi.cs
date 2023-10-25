using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeelgramBotSupporter.TelegramServices.Const;
using TeelgramBotSupporter.TelegramServices.Models;
using ThirdParty.Json.LitJson;

namespace TeelgramBotSupporter.ExternalResource.SoftEtherApi
{
    public class SoftetherApi : ISoftetherApi
    {
        //private const string apiUrl = "http://localhost:3000";
        private const string apiUrl = "http://sale.lachom.ir";
        private readonly HttpClient _client;
        public SoftetherApi()
        {
            _client = new HttpClient();
        }

        public async Task ChangeUserPassword(UserChangePassword model, string Token, SoftEtherApiEnum api)
        {
            string desitionalUrl = apiUrl + api.GetDescription();
            var obj = new
            {
                username = model.username,
                newpassword = model.newpassword,
                token = Token
            };
            try
            {
                var jsonData = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(desitionalUrl, content);
                string responseJson = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> ChangeUserServer<T>(UserChangeServer model, string Token, SoftEtherApiEnum api)
        {
            string desitionalUrl = apiUrl + api.GetDescription();
            var obj = new
            {
                username = model.username,
                servercode = model.ServerCode,
                currentservercode = model.CurrentServercode,
                token = Token
            };
            try
            {
                var jsonData = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(desitionalUrl, content);
                string responseJson = await response.Content.ReadAsStringAsync();
                T? result = JsonConvert.DeserializeObject<T>(responseJson);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> ConvertAccount<T>(UserChangeServer model, string Token, SoftEtherApiEnum api)
        {
            string desitionalUrl = apiUrl + api.GetDescription();
            var obj = new
            {
                username = model.username,
                newType = model.newType,
                servercode = model.ServerCode,
                token = Token
            };
            try
            {
                var jsonData = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(desitionalUrl, content);
                string responseJson = await response.Content.ReadAsStringAsync();
                T? result = JsonConvert.DeserializeObject<T>(responseJson);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
