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

namespace MadHotSpot.Controllers
{
    public class HotSpotAyarController : BaseController
    {

        public OtelAppDbContext context;
        private IConfiguration config;

        public HotSpotAyarController(OtelAppDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            config = _configuration;
        }

        public IActionResult Index()
        {
            var data = context.H_HotSpotAyar.FirstOrDefault(x => x.FirmaId == FirmaId);
            return View(data);
        }

        public IActionResult FirmaAl()
        {
            var firma = context.H_Firmalar.FirstOrDefault(x => x.Id == FirmaId);
            return Json(firma);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HotSpotAyar _ayar)
        {
            var retVal = new Response();

            try
            {
                var ayar = context.H_HotSpotAyar.FirstOrDefault(x => x.FirmaId == FirmaId);

                if (ayar != null)
                {
                    ayar.ArkaPlanUrl = _ayar.ArkaPlanUrl;
                    ayar.KvkkMetni = _ayar.KvkkMetni;
                    ayar.LogoUrl = _ayar.LogoUrl;
                    ayar.MisafirEmail = _ayar.MisafirEmail;
                    ayar.MisafirEmailZorunlu = _ayar.MisafirEmailZorunlu;
                    ayar.MisafirTelefon = _ayar.MisafirTelefon;
                    ayar.MisafirTelefonZorunlu = _ayar.MisafirTelefonZorunlu;
                    ayar.PersonelGirisi = _ayar.PersonelGirisi;
                    ayar.ToplantiEmail = _ayar.ToplantiEmail;
                    ayar.ToplantiEmailZorunlu = _ayar.ToplantiEmailZorunlu;
                    ayar.ToplantiGirisi = _ayar.ToplantiGirisi;
                    ayar.ToplantiTelefon = _ayar.ToplantiTelefon;
                    ayar.ToplantiTelefonZorunlu = _ayar.ToplantiTelefonZorunlu;
                    ayar.UcretsizHotspot = _ayar.UcretsizHotspot;
                }
                else
                {
                    context.H_HotSpotAyar.Add(_ayar);
                }
                context.SaveChanges();

                return Ok(new Response { Success = true, Message = "Kayıt Başarılı" });

            }
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = "Hata Döndü. " + ex.Message });
            }




        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
