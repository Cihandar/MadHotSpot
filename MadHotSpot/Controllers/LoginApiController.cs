using MadHotSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models;
using tik4net;
using tik4net.Api;
using tik4net.Objects;
using tik4net.Objects.Ip;
using tik4net.Objects.Ip.Firewall;
using tik4net.Objects.Queue;
using tik4net.Objects.System;
using Microsoft.AspNetCore.Cors;
using MadHotSpot.Models.ViewModel;

namespace MadHotSpot.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {

        public OtelAppDbContext context;
        private IConfiguration config;

        public LoginApiController(OtelAppDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            config = _configuration;
        }


        [EnableCors("AllowOrigin")]
        [HttpPost("LoginCheck")]
        public async Task<bool> LoginCheck(CustomerInfoViewModel customer)
        {
            var ayar = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == customer.FirmaId);


            if (ayar == null) return false;

            try
            {

      
            using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass))
            {
                 

               var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotUser>().Where(x=>x.Name==customer.RoomNumber && x.Password==customer.BirthDate).FirstOrDefault();

                if (user != null)
                {

                    context.H_CustomerInfo.Add(new CustomerInfo { 

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
