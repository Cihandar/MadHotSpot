using MadHotSpot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;

namespace MadHotSpot.Controllers
{
    public class ElektraEntegrasyonController : Controller
    {

        public OtelAppDbContext context;
        private IConfiguration config;
        private IMemoryCache memoryCache;
        public string baseUrl { get; set; }

        public ElektraEntegrasyonController(OtelAppDbContext _context, IConfiguration _configuration, IMemoryCache _memoryCache)
        {
            context = _context;
            config = _configuration;
            baseUrl = config.GetValue<string>("Settings:ElektraUrl");
            memoryCache = _memoryCache;
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

        public IActionResult HotSpot()
        {

            var response = ElektraSend<ElektraLoginResponse>(new ElektraGlobalRequest
            {
                LoginToken = "",
                Object = "FN_EASYPMS_HOTSPOT_GUESTS_GROUP",
                Parameters = new Parameters { HOTELID = 0 }
            });
            return Ok(response);
        }


        public T ElektraSend<T>(ElektraGlobalRequest request) where T : class
        {
            var client = new RestClient(baseUrl);
            request.LoginToken = GetLoginToken();
            var response = client.Execute<T>(new RestRequest("", Method.Post) { RequestFormat = DataFormat.Json }
                .AddBody(request));
            return response.Data;

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

    }


    #region Models

    #region Login

    public class ElektraLoginResponse
    {
        public bool Success { get; set; }
        public string LoginToken { get; set; }

    }
    #endregion

    #region Global Request
    public class ElektraGlobalRequest
    {
        public string LoginToken { get; set; }
        public Parameters Parameters { get; set; }
        public string Action { get; set; } = "Function";
        public string Object { get; set; }
        public List<OrderBy> OrderBy { get; set; }
        public Paging Paging { get; set; }
        public List<object> Where { get; set; }
        public string Tenant { get; set; }
        public string Usercode { get; set; }
        public string Password { get; set; }
    }

    public class OrderBy
    {
        public string Column { get; set; }
        public object Direction { get; set; }
    }

    public class Paging
    {
        public int ItemsPerPage { get; set; } = 100;
        public int Current { get; set; } = 1;
    }

    public class Parameters
    {
        public int HOTELID { get; set; }
    }
    #endregion



    #endregion

}
