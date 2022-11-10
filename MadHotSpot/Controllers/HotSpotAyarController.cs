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
using MadHotSpot.Interfaces;

namespace MadHotSpot.Controllers
{
    public class HotSpotAyarController : BaseController
    {

        public OtelAppDbContext context;
        private IConfiguration config;
        IFileUpload fileUpload;

        public HotSpotAyarController(OtelAppDbContext _context, IConfiguration _configuration,IFileUpload _fileUpload)
        {
            context = _context;
            config = _configuration;
            fileUpload = _fileUpload;
        }

        public IActionResult Index()
        {
            var data = context.H_HotSpotAyar.FirstOrDefault(x => x.FirmaId == FirmaId);
            return View(data);
        }

        public IActionResult FirmaAl()
        {
            var firma = context.H_Firmalar.FirstOrDefault(x => x.Id == FirmaId);
            return Json(firma);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HotSpotAyar _ayar)
        {
            var retVal = new Response();

            try
            {
                var ayar = context.H_HotSpotAyar.FirstOrDefault(x => x.FirmaId == FirmaId);
                if (ayar != null)
                {
                    ayar.ArkaPlanUrl = _ayar.ArkaPlanUrl;
                    ayar.KvkkMetni = _ayar.KvkkMetni;
                    ayar.LogoUrl = _ayar.LogoUrl;
                    ayar.MisafirEmail = _ayar.MisafirEmail;
                    ayar.MisafirEmailZorunlu = _ayar.MisafirEmailZorunlu;
                    ayar.MisafirTelefon = _ayar.MisafirTelefon;
                    ayar.MisafirTelefonZorunlu = _ayar.MisafirTelefonZorunlu;
                    ayar.PersonelGirisi = _ayar.PersonelGirisi;
                    ayar.ToplantiEmail = _ayar.ToplantiEmail;
                    ayar.ToplantiEmailZorunlu = _ayar.ToplantiEmailZorunlu;
                    ayar.ToplantiGirisi = _ayar.ToplantiGirisi;
                    ayar.ToplantiTelefon = _ayar.ToplantiTelefon;
                    ayar.ToplantiTelefonZorunlu = _ayar.ToplantiTelefonZorunlu;

                    ayar.SpaEmail = _ayar.SpaEmail;
                    ayar.SpaEmailZorunlu = _ayar.SpaEmailZorunlu;
                    ayar.SpaGirisi = _ayar.SpaGirisi;
                    ayar.SpaTelefon = _ayar.SpaTelefon;
                    ayar.SpaTelefonZorunlu = _ayar.SpaTelefonZorunlu;

                    ayar.GunuBirlikEmail = _ayar.GunuBirlikEmail;
                    ayar.GunuBirlikEmailZorunlu = _ayar.GunuBirlikEmailZorunlu;
                    ayar.GunuBirlikGirisi = _ayar.GunuBirlikGirisi;
                    ayar.GunuBirlikTelefon = _ayar.GunuBirlikTelefon;
                    ayar.GunuBirlikTelefonZorunlu = _ayar.GunuBirlikTelefonZorunlu;


                    ayar.UcretsizHotspot = _ayar.UcretsizHotspot;
                    //TODO: DUĞHAN UPDATE?
                }
                else
                {
                    _ayar.FirmaId = FirmaId;
                    context.H_HotSpotAyar.Add(_ayar);
                }
                context.SaveChanges();

                return Ok(new Response { Success = true, Message = "Kayıt Başarılı" });

            }
            catch (Exception ex)
            {
                return Ok(new Response { Success = false, Message = "Hata Döndü. " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(string base64file, string fname)
        {
            var result = await fileUpload.UploadFileImage("images/OtelLogo/", base64file, fname);
            return Json(result);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
