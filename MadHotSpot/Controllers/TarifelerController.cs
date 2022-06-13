using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Identity;
using tik4net;

namespace MadHotSpot.Controllers
{
    public class TarifelerController : BaseController
    {

        public OtelAppDbContext context;
        private readonly UserManager<AppUser> _userManager;

        public TarifelerController(OtelAppDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var data = context.H_Tarifeler.Where(x => x.FirmaId == FirmaId).ToList();
            return Json(data);
        }

        [HttpGet]
        public JsonResult GetTarife(Guid Id)
        {
            var data = context.H_Tarifeler.FirstOrDefault(x => x.Id == Id);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid Id)
        {
            var data = context.H_Tarifeler.FirstOrDefault(x => x.Id == Id);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tarifeler request)
        {
            try
            {
                request.Aktif = true;
                request.FirmaId = FirmaId;
                context.H_Tarifeler.Add(request);
                context.SaveChanges();

                return Json(new Response { Success = true, Message = "Yeni Tarife Eklendi !" });
            }
            catch (Exception ex)
            {
                return Json(new Response { Success = false, Message = "Hata -> " + ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var data = context.H_Tarifeler.FirstOrDefault(x => x.Id == Id);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Tarifeler data)
        {
            try
            {
                var req = context.H_Tarifeler.FirstOrDefault(x => x.Id == data.Id);

                req.Aktif = data.Aktif;
                req.EUROFiyat = data.EUROFiyat;
                req.USDFiyat = data.USDFiyat;
                req.TLFiyat = data.TLFiyat;
                req.TarifeAdi = data.TarifeAdi;
                req.Gun = data.Gun;

                context.SaveChanges();

                return Json(new Response { Success = true, Message = "Tarife Düzenlendi !" });
            }
            catch (Exception ex)
            {
                return Json(new Response { Success = false, Message = "Hata -> " + ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
