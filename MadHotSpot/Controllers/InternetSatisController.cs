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
            var firma = context.H_Firmalar.FirstOrDefault(x => x.Id == FirmaId);
            ViewBag.FirmaAdi = firma.FirmaAdi;

            return View();
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var data = context.H_InternetSatis.Where(x => x.FirmaId == FirmaId && x.BitisTarihi >= DateTime.Now ).OrderBy(x => x.BaslamaTarihi).ToList();
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
            try
            {


                satis.InternetSatis.FirmaId = FirmaId;
                satis.InternetSatis.BaslamaTarihi = DateTime.Now;
                satis.InternetSatis.BitisTarihi = DateTime.Now.AddDays(satis.InternetSatis.Gun);
                satis.InternetSatis.Iade = false;


                if (AddUserMikrotik(context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId), satis.InternetSatis))
                {

                    context.H_InternetSatis.Add(satis.InternetSatis);
                    context.SaveChanges();
                    return Ok(new Response { Success = true, Message = "Kayıt Başarılı" });
                }
                else
                {
                    return Ok(new Response { Success = false, Message = "Mikrotikden Hata Döndü. Kullanıcı Eklenemedi" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = "Hata Döndü. " + ex.Message }); ;
            }
        }

        public async Task<IActionResult> IadeHesapla(InternetSatis satis)
        {
            return Ok(Prvt_IadeHesapla(satis));
        }


        public async Task<IActionResult> Iade(InternetSatis satis)
        {

            var sonuc = Prvt_IadeHesapla(satis); 

            if (sonuc.Success)
            {
                var data = context.H_InternetSatis.FirstOrDefault(x => x.Id == satis.Id);

                if (MikrotikIade(data.Sifre))
                {

                    data.Iade = true;
                    data.IadeEdilenTutar = sonuc.IadeTutar;
                    context.SaveChanges();

                    return Ok(new Response { Success = true, Message = "Iade Yapıldı." });
                }
                else
                {
                    return Ok(new Response { Success = false, Message = "Iade Yapılamadı. Hata!!" });
                }
            }
            else
            {
                return Ok(new Response { Success = false, Message = "Iade Yapılamadı. Hata!!" });
            }

        }

        
        private Response Prvt_IadeHesapla(InternetSatis satis)
        {
            var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
            if(! ayar.IadeAktif) return new Response { Success = false, Message = "Iade Aktif Değil.. Iade yapılmadı !.. " };

            var data = context.H_InternetSatis.FirstOrDefault(x => x.Id == satis.Id);

            if (DateTime.Now.Date == data.BitisTarihi.Date)  //Son gün İade yapılmaz. Kontrol..
            {
                return  new Response { Success = false, Message = "Bitiş Tarihinde İade yapamazsınız" };
            }

            TimeSpan kalangun = data.BitisTarihi.Date - DateTime.Now.Date;

            double gunluktutar = data.Tutar / data.Gun;

            double iadetutar = kalangun.Days * gunluktutar;

            return new Response { Success = true, IadeGun = kalangun.Days, IadeTutar = iadetutar, Doviz = data.Doviz };
        }
        public bool ActiveUserDelete(string sifre)
        {
            try
            {
                var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
                {
                    var active = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotActive>().SingleOrDefault(ha => ha.UserName == sifre);
                    if (active != null)
                    {
                        ITikCommand cmd = conn.CreateCommand("/ip/hotspot/active/remove",
                        conn.CreateParameter(".id", active.Id));
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
   
        public bool MikrotikIade(string sifre)
        {
            try
            {

                var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);



                    using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
                    {

                  

                    ActiveUserDelete(sifre); 
                    //conn.Delete(active);


                    var user =   conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotUser>(conn.CreateParameter("name", sifre)).FirstOrDefault();
                    conn.Delete(user);



               

                    //cmd = conn.CreateCommand("/ip/hotspot/cookies/removee",
                    //conn.CreateParameter("numbers", sifre));
                    //cmd.ExecuteNonQuery();


                }

             
                    context.SaveChanges();

                return true;
            




            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool AddUserMikrotik(Ayarlar ayar, InternetSatis satis)
        {
            try
            {
                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
                {

                    var sure = satis.Gun * 24;
                    var user = new tik4net.Objects.Ip.Hotspot.HotspotUser()
                    {
                        Profile = ayar.MikrotikProfilAdi,
                        Server = ayar.MikrotikHotspotAdi,
                        Name = satis.Sifre,
                        Password = ayar.MikrotikDefaultSifre,
                        LimitUptime = sure.ToString() + ":00:00"
                    };

                    conn.Save(user);




                    // if (!conn.IsOpened) conn.Open(ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass);
                    // ITikCommand cmd = conn.CreateCommand("/ip/hotspot/user/add",
                    //conn.CreateParameter("server", ayar.MikrotikHotspotAdi),
                    //conn.CreateParameter("profile", ayar.MikrotikProfilAdi),
                    //conn.CreateParameter("name", satis.Sifre),
                    //conn.CreateParameter("password", ayar.MikrotikDefaultSifre));
                    // conn.CreateParameter("LimitUptime", "01:00:00");
                    // cmd.ExecuteNonQuery();

                    // conn.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
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
