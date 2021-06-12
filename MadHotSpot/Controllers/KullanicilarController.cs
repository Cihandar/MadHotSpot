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
        public async Task<IActionResult> Create(Guid KullanicilarId)
        {
            var data = context.H_Kullanicilar.FirstOrDefault(x => x.Id == KullanicilarId);
            return PartialView("_FormPartial", data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Kullanicilar request)
        {
            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.KullaniciKodu,
                Yetki = request.Yetki,
                FirmaId = FirmaId
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            return Json("True");
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid KullaniciId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == KullaniciId.ToString() && x.FirmaId == FirmaId);
            return PartialView("_FormPartial", new Kullanicilar
            {
                KullaniciKodu =  user.UserName,
                Yetki =  user.Yetki,
                Email =  user.Email,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(Kullanicilar data)
        {
            var snc = context.H_Kullanicilar.FirstOrDefault(x => x.Id == data.Id);

            snc.KullaniciKodu = data.KullaniciKodu;
            snc.Password = data.Password;
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
