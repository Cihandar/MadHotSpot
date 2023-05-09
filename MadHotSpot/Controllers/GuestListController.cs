using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using tik4net.Api;
using tik4net.Objects;
using tik4net.Objects.Ip;
using tik4net.Objects.Ip.Firewall;
using tik4net.Objects.Queue;
using tik4net.Objects.System;
using MadHotSpot.Interfaces;

namespace MadHotSpot.Controllers
{
    public class GuestListController : BaseController
    {

        public OtelAppDbContext context;
        public IMeetMikrotikCrud _meetMikrotikCrud;

        public GuestListController(OtelAppDbContext _context, IMeetMikrotikCrud meetMikrotikCrud)
        {
            context = _context;
            _meetMikrotikCrud = meetMikrotikCrud;
        }

        public IActionResult Index()
        {
            var firma = context.H_Firmalar.FirstOrDefault(x => x.Id == FirmaId);
            var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
            ViewBag.ManuelGuestAdd = ayar.ManuelGuestAdd ? "true" : "false";
            ViewBag.FirmaAdi = firma.FirmaAdi;

            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                var firma = context.H_Rezervasyonlar.Where(x => x.FirmaId == FirmaId && !x.Silindi).ToList();
                return Json(firma);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var n = new Rezervasyonlar();
            n.AyrilisTarihi = DateTime.Now.AddDays(7);
            n.DogumTarihi = DateTime.Parse("01.01.1990");
            return PartialView("_FormPartial",  n );
        }

        [HttpPost]
        public JsonResult Create(Rezervasyonlar rez)
        {
            try
            {
                rez.FirmaId = FirmaId;
                var result = _meetMikrotikCrud.AddUser(rez).Result;
                if(! result.Success) return Json(new { Success = false, Message = result.Message });

                var firma = context.H_Rezervasyonlar.Add(rez);
                context.SaveChanges();
                return Json(new { Success = true,Message="Kayıt Başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            return PartialView("_FormPartial",context.H_Rezervasyonlar.FirstOrDefault(x => x.Id == Id));
        }

        [HttpPost]
        public JsonResult Update(Rezervasyonlar rez)
        {
            try
            {
                var rezervasyon = context.H_Rezervasyonlar.FirstOrDefault(x => x.Id == rez.Id);
                if(rezervasyon!=null)
                {
                    rezervasyon.KimlikNo = rez.KimlikNo;
                    rezervasyon.MusteriAdi = rez.MusteriAdi;
                    rezervasyon.Odano = rez.Odano;
                    rezervasyon.Tcno = rez.Tcno;
                    context.SaveChanges();
                }
                return Json(new { Success = true, Message = "Kayıt Başarılı" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        //[HttpPost]
        //public JsonResult Delete(Guid Id)
        //{
        //    try
        //    {
            
        //        var rezervasyon = context.H_Rezervasyonlar.FirstOrDefault(x => x.Id == Id);
        //        if (rezervasyon != null)
        //        {            
        //                rezervasyon.Silindi = true;
        //        }
        //        return Json(new { success = true, message = "Kayıt Başarılı" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
