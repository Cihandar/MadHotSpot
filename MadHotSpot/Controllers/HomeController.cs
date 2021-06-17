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

        public JsonResult get_dailycash()
        {
            var data = new List<ViewKasa>();
            try
            {

                SqlConnection con = new SqlConnection(config.GetConnectionString("OtelAppDatabase"));
                SqlCommand cmd = new SqlCommand("Select SUM(Bakiye) Bakiye from QV_KASA where Tarih=CONVERT(date,getdate()) and FirmaId='" + FirmaId + "' Order by Doviz Desc", con);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var kasa = new ViewKasa();
                    kasa.Bakiye = dr.GetFieldValue<double>(dr.GetOrdinal("Bakiye"));

                    data.Add(kasa);
                }

                con.Close();


            }
            catch (Exception ex)
            {
                data = null;
            }
            return Json(data);
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
    }
}
