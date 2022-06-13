using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Identity;
using tik4net;
using MadHotSpot.Models.ViewModel;
using tik4net.Objects;

namespace MadHotSpot.Controllers
{
    public class LoginController : Controller
    {

        public OtelAppDbContext context;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(OtelAppDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }

        public IActionResult Index(string ClientMac, string ClientIp, string Lokasyon, Guid FirmaId)
        {
            ViewBag.ClientMac = ClientMac;
            ViewBag.ClientIp = ClientIp;
            ViewBag.Lokasyon = Lokasyon;

            var data = context.H_HotSpotAyar.FirstOrDefault(x => x.FirmaId == FirmaId);

            return View(data); 
        }

        [HttpPost("LoginCheck")]
        public async Task<bool> LoginCheck(CustomerInfoViewModel customer)
        {
            var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == customer.FirmaId);


            if (ayar == null) return false;

            try
            {
                using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
                {
                    var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotUser>().Where(x => x.Name == customer.BirthDate && x.Password == customer.RoomNumber).FirstOrDefault();

                    if (user != null)
                    {

                        context.H_CustomerInfo.Add(new CustomerInfo
                        {

                            PhoneNumber = customer.PhoneNumber,
                            Email = customer.Email,
                            FirmaId = customer.FirmaId,
                            BirthDate = customer.BirthDate

                        });
                        context.SaveChanges();
                        return true;


                    }
                    else return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
