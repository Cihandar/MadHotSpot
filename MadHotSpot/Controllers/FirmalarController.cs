using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Identity;
using tik4net;
using MadHotSpot.Extentions;
using Microsoft.EntityFrameworkCore;

namespace MadHotSpot.Controllers
{
    public class FirmalarController : BaseController
    {

        public OtelAppDbContext context;
        private readonly UserManager<AppUser> _userManager;

        public FirmalarController(OtelAppDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (!admin) return Redirect("/");
            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var data = context.H_Firmalar.Where(x => x.FirmaKodu != 6807).OrderBy(x => x.BitisTarihi).ToList();
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var data = await context.H_Firmalar.FirstOrDefaultAsync(x => x.Id == null);
            return PartialView("_FormPartial", data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Firmalar request)
        {
            request.Id = Guid.NewGuid();
            request.FirmaKodu = new Random().Next(1000, 9999);
            var firma = new Firmalar
            {
                Aktif = true,
                FirmaAdi = request.FirmaAdi,
                BaslamaTarihi = request.BaslamaTarihi,
                BitisTarihi = request.BitisTarihi,
                Email = request.Email,
                Id = request.Id,
                FirmaKodu = request.FirmaKodu,
                Password = request.Password,
                Telefon = request.Telefon,
                Silindi = false,
                YetkiliAdSoyad = request.YetkiliAdSoyad
            };

            context.H_Firmalar.Add(firma);
            await context.SaveChangesAsync();

            try
            {
                var user = new AppUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    PhoneNumber = request.Telefon,
                    FirmaId = request.Id
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    context.H_Ayarlar.Add(new Ayarlar { FirmaId = request.Id, GunlukFiyatTL = 10, GunlukFiyatEURO = 2, GunlukFiyatUSD = 3, SinirsizAktif = false, AdSoyadZorunlu = false });
                    await context.SaveChangesAsync();
                    
                    SendEmail mail = new SendEmail();
                    mail.Send(request.Email, "Online Hotspot Giriş Bilgileri", "", request.Email, request.FirmaKodu.ToString(), request.Password, "IlkKayit");
                }
            }
            catch (Exception ex)
            {
                return Json(new Response { Success = false, Message = ex.Message });
            }

            return Json(new Response { Success = true, Message = "Kayıt Başarılı" });
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var data = await context.H_Firmalar.FirstOrDefaultAsync(x => x.Id == Id);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Firmalar request)
        {
            var firma = await context.H_Firmalar.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (firma != null)
            {
                firma.Aktif = request.Aktif;
                firma.FirmaAdi = request.FirmaAdi;
                firma.BaslamaTarihi = request.BaslamaTarihi;
                firma.BitisTarihi = request.BitisTarihi;
                firma.Email = request.Email;
                firma.FirmaKodu = request.FirmaKodu;
                firma.Password = request.Password;
                firma.Telefon = request.Telefon;
                firma.Silindi = false;
                firma.YetkiliAdSoyad = request.YetkiliAdSoyad;
                //TODO:DUGHAN
                context.SaveChanges();
                
                return Json(new Response { Success = true, Message = "Kayıt Başarılı" });
            }
            else
                return Json(new Response { Success = false, Message = "Kayıt Başarısız" });
        }


        //[HttpPost]
        //public async Task<IActionResult> Delete(Guid Id)
        //{

        //    var user = _userManager.Users.FirstOrDefault(x => x.Id == Id.ToString() && x.FirmaId == FirmaId);
        //    if (user != null && user.UserName != HttpContext.User.Identity.Name)
        //    {
        //        var response = await _userManager.DeleteAsync(user);
        //        return Ok(new Response { Success = response.Succeeded, Message = "Kayıt Başarılı" });
        //    }

        //    if (user.UserName == HttpContext.User.Identity.Name)
        //    {
        //        return Ok(new Response { Success = false, Message = "Online User Kendini Silemez!" });
        //    }
        //    return Ok(new Response { Success = false, Message = "Bir Sorun Oluştu" });
        //}


        //public IActionResult TestMikrotik(Ayarlar ayar)
        //{
        //    try
        //    {
        //        int port = 0;
        //        int.TryParse(ayar.MikrotikPort, out port);

        //        using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, port, ayar.MikrotikUser, ayar.MikrotikPass))
        //        {
        //            if (!conn.IsOpened) conn.Open(ayar.MikrotikIp, port, ayar.MikrotikUser, ayar.MikrotikPass);
        //            conn.Close();
        //            return Json("True");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
