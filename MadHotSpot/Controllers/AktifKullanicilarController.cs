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
    public class AktifKullanicilarController : BaseController
    {

        public OtelAppDbContext context;

        public AktifKullanicilarController(OtelAppDbContext _context)
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
            try
            {
                var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
                {
                    var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotActive>().ToList();
                    return Json(user);
                }
            }
            catch (Exception ex)
            {
                return null;
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
