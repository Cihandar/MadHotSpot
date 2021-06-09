using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;

namespace MadHotSpot.Controllers
{
    public class KullanicilarController : BaseController
    {

        public OtelAppDbContext context;

        public KullanicilarController(OtelAppDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
      
 
            return View();
 
        }


        [HttpGet]
        public JsonResult GetAll()
        {
            var data = context.H_Kullanicilar.Where(x => x.FirmaId == FirmaId).ToList();
   
            return Json(data);
        }


        [HttpGet]
        public async Task<IActionResult> Create(Guid KullanicilarId)
        {
            var data = context.H_Kullanicilar.FirstOrDefault(x => x.Id == KullanicilarId);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kullanicilar data)
        {
            context.H_Kullanicilar.Add(data);
            return Json("True");
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid KullanicilarId)
        {
            var data = context.H_Kullanicilar.FirstOrDefault(x => x.Id == KullanicilarId);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Kullanicilar data)
        {
            var snc = context.H_Kullanicilar.FirstOrDefault(x => x.Id == data.Id);

            snc.KullaniciKodu = data.KullaniciKodu;
            snc.Sifre = data.Sifre;
            snc.Telefon = data.Telefon;
            snc.Yetki = data.Yetki;
            snc.Email = data.Email;

            var result = context.SaveChanges();


            return Json(result);
        }



        public IActionResult TestMikrotik(Ayarlar ayar)
        {
            try
            {
                int port = 0;
                int.TryParse(ayar.MikrotikPort, out port);

                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, port, ayar.MikrotikUser, ayar.MikrotikPass))
                {
                    if (!conn.IsOpened) conn.Open(ayar.MikrotikIp, port, ayar.MikrotikUser, ayar.MikrotikPass);
                    conn.Close();
                    return Json("True");
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);                
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
