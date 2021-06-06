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
    public class AyarlarController : Controller
    {
        public IActionResult Index()
        {
            var data = "";
            // return Json(data);
            return View(data);
 
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your conta2ct page.";

            return View();
        }

        #region Update
 

        [HttpPost]
        public async Task<IActionResult> Update(Ayarlar ayarlar)
        {
            var data = "";
            return Json(data);
        }
        #endregion

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
