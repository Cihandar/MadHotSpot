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
    public class InternetSatisController : BaseController
    {

        public OtelAppDbContext context;

        public InternetSatisController(OtelAppDbContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            var data = context.H_InternetSatis.Where(x => x.FirmaId == FirmaId && x.BitisTarihi >= DateTime.Now).OrderBy(x => x.BaslamaTarihi).ToList();
            // return Json(data);
            return View(data);
 
        }
 

 

        [HttpPost]
        public async Task<IActionResult> Create(InternetSatis satis)
        {
            context.Add(satis);
            return Json("True");
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
