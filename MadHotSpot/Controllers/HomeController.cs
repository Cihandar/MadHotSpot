using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Authorization;
using tik4net;
using tik4net.Api;
using tik4net.Objects;
using tik4net.Objects.Ip;
using tik4net.Objects.Ip.Firewall;
using tik4net.Objects.Queue;
using tik4net.Objects.System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MadHotSpot.Controllers
{
    public class HomeController : BaseController
    {

        public OtelAppDbContext context;
        private IConfiguration config;
        public HomeController(OtelAppDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            config = _configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> get_Hotspot_user(bool hepsi)
        {
            try
            {
                var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
                {

                   if(hepsi)
                    {
                        var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotUser>().ToList();
                        return Ok(new Response { Success = true, KullaniciSayisi = user.Count });
                    }
                    else
                    {
                        var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotActive>().ToList();
                        return Ok(new Response { Success = true, KullaniciSayisi = user.Count });
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = ex.Message,KullaniciSayisi=0 });             
            }

        }

        public async Task<IActionResult> get_dailycash()
        {
            var data = new List<ViewKasa>();
            try
            {
                SqlConnection con = new SqlConnection(config.GetConnectionString("OtelAppDatabase"));
                SqlCommand cmd = new SqlCommand("Select Doviz,SUM(Bakiye) Bakiye from QV_KASA where Tarih=CONVERT(date,getdate()) and FirmaId='" + FirmaId + "' Group by Doviz Order by Doviz Desc", con);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var kasa = new ViewKasa();
                    kasa.Doviz = dr.GetFieldValue<string>(dr.GetOrdinal("Doviz"));
                    kasa.Bakiye = dr.GetFieldValue<double>(dr.GetOrdinal("Bakiye"));

                    data.Add(kasa);
                }

                con.Close();
            }
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = ex.Message });
            }

            string tl = "0 ₺", usd = "0 $", euro = "0 €";
            foreach (var  x in data)
            {
                if (x.Doviz == "TL") tl = x.Bakiye.ToString() + " ₺";
                if (x.Doviz == "USD") usd = x.Bakiye.ToString() + " $";
                if (x.Doviz == "EURO") euro = x.Bakiye.ToString() + " €";

            }

            return Ok(new Response { Success = true, Message = tl + " / " + euro + " / " + usd });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestMikrotik()
        {
            try
            {
                //using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.ApiSsl_v2)) // Use TikConnectionType.Api for mikrotikversion prior v6.45
                //{
                //    connection.Open("185.117.122.10", 8910, "mduzgun", "Abera.-4207**");
                //    return Json("Bağlantı Başarılı !!!");
                //}
                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, "185.117.122.10", 18728, "mduzgun", "Abera.-4207**"))
                {
                    if (!conn.IsOpened) conn.Open("185.117.122.10", 18728, "mduzgun", "Abera.-4207**");
                    conn.Close();
                    return Json("Okey");
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

        public async Task<IActionResult> DailyTotalCost()
        {
            var data = new List<ViewKasa>();
            try
            {
                //TODO :DUĞHAN
                SqlConnection con = new SqlConnection(config.GetConnectionString("OtelAppDatabase"));
                SqlCommand cmd = new SqlCommand("Select Doviz,Satis,Iade,Bakiye ,Tarih from QV_KASA  where FirmaId='" + FirmaId + "' Order by Doviz Desc", con);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var kasa = new ViewKasa();
                    kasa.Doviz = dr.GetFieldValue<string>(dr.GetOrdinal("Doviz"));
                    kasa.Satis = dr.GetFieldValue<double>(dr.GetOrdinal("Satis"));
                    kasa.Iade = dr.GetFieldValue<double>(dr.GetOrdinal("Iade"));
                    kasa.Bakiye = dr.GetFieldValue<double>(dr.GetOrdinal("Bakiye"));
                    kasa.Tarih = dr.GetFieldValue<DateTime>(dr.GetOrdinal("Tarih"));
                    data.Add(kasa);
                }
                con.Close();

                data = data.GroupBy(x => x.Tarih.Date).Select(x => new ViewKasa
                {
                    Tarih = x.Key,
                    Doviz = x.FirstOrDefault().Doviz,
                    Iade = x.FirstOrDefault().Iade,
                    Satis = x.FirstOrDefault().Satis,
                    Bakiye = x.FirstOrDefault().Bakiye,
                }).ToList();

            }
            catch (Exception ex)
            {
                data = null;

            }

            return Ok(new Response { Success = false, Message = "Iade Yapılamadı. Hata!!", Result = data });

        }
    }
}
