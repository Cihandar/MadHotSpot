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
    public class InternetSatisController : BaseController
    {

        public OtelAppDbContext context;

        public InternetSatisController(OtelAppDbContext _context)
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
            var data = context.H_InternetSatis.Where(x => x.FirmaId == FirmaId && x.BitisTarihi >= DateTime.Now).OrderBy(x => x.BaslamaTarihi).ToList();
            // return Json(data);
            return Json(data);
        }



        [HttpGet]
        public async Task<IActionResult> Create(Guid InternetSatisId)
        {
            InternetSatisViewModel satis = new InternetSatisViewModel();
            satis.InternetSatis = context.H_InternetSatis.FirstOrDefault(x => x.Id == InternetSatisId);
            satis.Ayarlar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
            return PartialView("_FormPartial", satis);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InternetSatisViewModel satis)
        {
            try { 
            context.H_InternetSatis.Add(satis.InternetSatis);
            context.SaveChanges();
            return Ok(new Response { Success = true, Message = "Kayıt Başarılı" });
            }     
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = "Hata Döndü. " + ex.Message }); ;
            }
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
