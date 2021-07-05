using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MadHotSpot.Controllers
{
    public class KasaController : BaseController
    {

        public OtelAppDbContext context;
        private IConfiguration config;
        public KasaController(OtelAppDbContext _context,IConfiguration _configuration)
        {
            context = _context;
            config = _configuration;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public JsonResult GetAll(DateTime tarih)
        {
            var data = new List<ViewKasa>();
            try
            {
                SqlConnection con = new SqlConnection(config.GetConnectionString("OtelAppDatabase"));
                SqlCommand cmd = new SqlCommand("Select Doviz,Satis,Iade,Bakiye from QV_KASA where Tarih=CONVERT(date,'"+tarih.Date+"') and FirmaId='" + FirmaId + "' Order by Doviz Desc", con);
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var kasa = new ViewKasa();
                    kasa.Doviz = dr.GetFieldValue<string>(dr.GetOrdinal("Doviz"));
                    kasa.Satis = dr.GetFieldValue<double>(dr.GetOrdinal("Satis"));
                    kasa.Iade = dr.GetFieldValue<double>(dr.GetOrdinal("Iade"));
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



   
 
 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
