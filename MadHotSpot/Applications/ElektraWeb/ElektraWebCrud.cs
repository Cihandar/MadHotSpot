using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MadHotSpot.Applications.ElektraWeb
{
    public class ElektraWebCrud : IElektraWebCrud
    {
        public OtelAppDbContext context;
        private IConfiguration config;
        private IMemoryCache memoryCache;
        public string baseUrl { get; set; }

        public ElektraWebCrud(OtelAppDbContext context, IConfiguration config, IMemoryCache memoryCache, string baseUrl)
        {
            this.context = context;
            this.config = config;
            this.memoryCache = memoryCache;
            this.baseUrl = baseUrl;
        }

        public async Task<string> GetCashFolioIdContent()
        {
          return new StringContent("{\"Parameters\":{\"RESID\":\"@aaa\",\"DEPID\":87415,\"REVID\":53188,\"DEPID_PAYMENT\":58301,\"CTOTAL\":\"10\",\"CURRENCYID\":44,\"NOTES\":\"deneme\",\"HOTELID\":20854},\"Action\":\"Execute\",\"Object\":\"SP_EASYPMS_CHARGECASHFOLIO\",\"BaseObject\":\"HOTEL_FOLIOTRANS\",\"BaseIds\":[43023763],\"ActionTitle\":\"Cash Posting & Payment\",\"LoginToken\":\"5b57ef63d3c6b58343c4acd15a96e907962f1a9037b45e81e533ae69a53b2f6739b2291b3c08995fd3d8fdbd98354f600db1bb9d7a323b5348f0b6e98731c86b08722c713f6abc39b64f6dfbcf0f360234a2f10b9d777c6f7a13e0296fee6bd6dba89aa98af6957702e6ae85bcc7eba2aa708fd45fafce2a5ced777187418042ac69fcde20a4625698459ca41ffde83ae0470159c0ef4940e2f9e46ebc1da2a343eafccbddb6344b14e7fee1dc45a5049aae112ea1c2df6265a15cdda272e6efa3f7c2437408c660c7edcd6e95f88e50440f6efd8cdf9d18690b66bf29bd2c66e95dc4be842c16aa42667c29ac5e082c\"}", null, "application/json").ToString();
        }

        public async Task<string> GetCashPostingContent()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetCurrenciesContent()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetDepartmansContent()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRevenuesContent()
        {
            throw new NotImplementedException();
        }

        private string GetLoginToken()
        {
            string key = "LoginToken";
            string token;
            if (!memoryCache.TryGetValue("LoginToken", out token))
            {
                token = Login();
                memoryCache.Set(key, token, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    Priority = CacheItemPriority.Normal
                });
            }
            return token;
        }

        public string Login()
        {
            var response = ElektraSend<ElektraLoginResponse>(new ElektraGlobalRequest
            {
                Action = "",
                Password = "",
                Tenant = "",
                Usercode = ""
            });

            if (response != null && response.Success && !string.IsNullOrEmpty(response.LoginToken))
                return response.LoginToken;

            return null;
        }

        public T ElektraSend<T>(ElektraGlobalRequest request) where T : class
        {
            var client = new RestClient(baseUrl);
            request.LoginToken = GetLoginToken();
            var response = client.Execute<T>(new RestRequest("", Method.Post) { RequestFormat = DataFormat.Json }
                .AddBody(request));
            return response.Data;

        }
    }
}
