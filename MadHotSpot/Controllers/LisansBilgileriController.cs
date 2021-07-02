using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MadHotSpot.Controllers
{
    public class LisansBilgileriController : BaseController
    {

        public OtelAppDbContext context;
        private IConfiguration config;

        public LisansBilgileriController(OtelAppDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            config = _configuration;
        }

        public IActionResult Index()
        {
            var data = context.H_Firmalar.FirstOrDefault(x=>x.Id == FirmaId);
            // return Json(data);
            return View(data);
 
        }
 

 
 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }
}
