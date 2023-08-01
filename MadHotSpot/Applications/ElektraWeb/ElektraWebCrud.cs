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
            this.baseUrl = config.GetValue<string>("Settings:ElektraUrl"); ;
        }

        public Task<string> SetCashFolio(CashFolioParameter cashFolioParameters, Guid FirmaId)
        {
            var request = new ElektraGlobalRequest
            {
                Parameters = cashFolioParameters,
                Object = "SP_EASYPMS_CHARGECASHFOLIO",
            };

            // var result = ElektraSend<ElektraGlobalRequest>(request);

            return null;
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

        private string GetLoginToken(Guid FirmaId)
        {
            string key = "LoginToken-" + FirmaId;
            string token;
            if (!memoryCache.TryGetValue(key, out token))
            {
                token = Login(FirmaId);
                memoryCache.Set(key, token, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddDays(1),
                    Priority = CacheItemPriority.Normal
                });
            }
            return token;
        }

        public string Login(Guid FirmaId)
        {
            var setting = context.H_ElektraWebSetting.Where(x => x.FirmaId == FirmaId).FirstOrDefault();
            if (setting != null)
            {
                var response = ElektraSend<ElektraLoginResponse>(new ElektraGlobalRequest
                {
                    Action = "Login",
                    Password = setting.Password,
                    Tenant = setting.HotelId.ToString(),
                    Usercode = setting.UserName
                }, Guid.Empty);

                if (response != null && response.Success && !string.IsNullOrEmpty(response.LoginToken))
                    return response.LoginToken;
            }
            return null;
        }

        public T ElektraSend<T>(ElektraGlobalRequest request, Guid FirmaId) where T : class
        {
            var client = new RestClient(baseUrl);

            if (FirmaId != Guid.Empty)
                request.LoginToken = GetLoginToken(FirmaId);
            else
                request.LoginToken = "";

            var response = client.Execute<T>(new RestRequest("", Method.Post) { RequestFormat = DataFormat.Json }
                .AddBody(request));
            return response.Data;
        }


    }
}
