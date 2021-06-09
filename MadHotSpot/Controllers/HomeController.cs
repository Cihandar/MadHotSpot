using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Authorization;
using tik4net;

namespace MadHotSpot.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
