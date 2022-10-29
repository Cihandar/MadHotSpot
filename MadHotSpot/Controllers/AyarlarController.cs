using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MadHotSpot.Interfaces;

namespace MadHotSpot.Controllers
{
    public class AyarlarController : BaseController
    {

        public OtelAppDbContext context;
        private IConfiguration config;
        public IMikrotikSettings _mikrotikSettings;

        public AyarlarController(OtelAppDbContext _context, IConfiguration _configuration, IMikrotikSettings mikrotikSettings)
        {
            context = _context;
            config = _configuration;
            _mikrotikSettings = mikrotikSettings;
        }

        public IActionResult Index()
        {
            var data = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
            data.MikrotikPass = null;
            return View(data);
        }

        public IActionResult FirmaAl()
        {
            var firma = context.H_Firmalar.FirstOrDefault(x => x.Id == FirmaId);
            return Json(firma);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Ayarlar ayarlar)
        {
            var retVal = new Response();

            try
            {
                var ayar = await context.H_Ayarlar.FirstOrDefaultAsync(x => x.FirmaId == FirmaId);

                ayar.GunlukFiyatEURO = ayarlar.GunlukFiyatEURO;
                ayar.GunlukFiyatTL = ayarlar.GunlukFiyatTL;
                ayar.GunlukFiyatUSD = ayarlar.GunlukFiyatUSD;
                ayar.MikrotikIp = ayarlar.MikrotikIp;
                ayar.MikrotikPort = ayarlar.MikrotikPort;
                ayar.MikrotikUser = ayarlar.MikrotikUser;
                if (!string.IsNullOrEmpty(ayarlar.MikrotikPass)) ayar.MikrotikPass = ayarlar.MikrotikPass;
                ayar.MikrotikDefaultSifre = ayarlar.MikrotikDefaultSifre;
                ayar.SinirsizAktif = ayarlar.SinirsizAktif;
                ayar.AdSoyadZorunlu = ayarlar.AdSoyadZorunlu;
                ayar.MikrotikHotspotAdi = ayarlar.MikrotikHotspotAdi;
                ayar.MikrotikProfilAdi = ayarlar.MikrotikProfilAdi;
                ayar.IadeAktif = ayarlar.IadeAktif;
                ayar.TarifeAktif = ayarlar.TarifeAktif;
                ayar.DiaEntegrasyonAktif = ayarlar.DiaEntegrasyonAktif;
                ayar.DiaUrl = ayarlar.DiaUrl;
                //TODO : DUGHAN
                context.SaveChanges();

                return Ok(new Response { Success = true, Message = "Kayıt Başarılı" });

            }
            catch (Exception)
            {
                return Ok(new Response { Success = false, Message = "Hata Döndü. Bir Dön bak kendine... " }); ;
            }
        }

        public IActionResult TestMikrotik(Ayarlar ayar)
        {
            try
            {
                var retVal = new Response();
                int port = 0;
                int.TryParse(ayar.MikrotikPort, out port);

                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, port, ayar.MikrotikUser, ayar.MikrotikPass))
                {
                    if (!conn.IsOpened) conn.Open(ayar.MikrotikIp, port, ayar.MikrotikUser, ayar.MikrotikPass);
                    conn.Close();
                    return Ok(new Response { Success = true, Message = "Erişim Sağlandı" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = ex.Message }); ;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteExpireUsers()
        {
            var result = await _mikrotikSettings.DeleteExpireTimeUsers(FirmaId);
            return Json(result);
        }
    }
}
