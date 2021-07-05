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
    public class KullanicilarController : BaseController
    {

        public OtelAppDbContext context;
        private readonly UserManager<AppUser> _userManager;

        public KullanicilarController(OtelAppDbContext _context, UserManager<AppUser> userManager)
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
            var data = _userManager.Users.Where(x => x.FirmaId == FirmaId).ToList();
            return Json(data);
        }


        [HttpGet]
        public async Task<IActionResult> Create(Guid Id)
        {
            var data = context.H_Kullanicilar.FirstOrDefault(x => x.Id == Id);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kullanicilar request)
        {

            var checkuser = _userManager.Users.FirstOrDefault(x => (x.Email == request.Email) && x.FirmaId == FirmaId);
            if(checkuser !=null) return Json(new Response { Success = false, Message = "Sistemde aynı kullanıcı adı veya mail adresi mevcut" });

            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                Yetki = request.Yetki,
                FirmaId = FirmaId
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            return Json(new Response { Success = result.Succeeded, Message = "Kayıt Başarılı" });
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == Id.ToString() && x.FirmaId == FirmaId);
            return PartialView("_FormPartial", new Kullanicilar
            {
                Id = Guid.Parse(user.Id),
                KullaniciKodu =  user.UserName,
                Yetki =  user.Yetki,
                Email =  user.Email,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(Kullanicilar data)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == data.Id.ToString() && x.FirmaId == FirmaId);
            user.Yetki = data.Yetki;
             await _userManager.UpdateAsync(user);


            return Json(new Response { Success = true, Message = "Kayıt Başarılı" });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {

            var user = _userManager.Users.FirstOrDefault(x => x.Id == Id.ToString()  && x.FirmaId == FirmaId);
            if (user != null && user.UserName != HttpContext.User.Identity.Name)
            {
                var response = await _userManager.DeleteAsync(user);
                return Ok(new Response {Success = response.Succeeded, Message = "Kayıt Başarılı"});
            }

            if (user.UserName == HttpContext.User.Identity.Name)
            {
                return Ok(new Response { Success = false, Message = "Online User Kendini Silemez!" });
            }
            return Ok(new Response { Success = false, Message = "Bir Sorun Oluştu" });
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
