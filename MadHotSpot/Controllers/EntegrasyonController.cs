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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using tik4net.Objects;

namespace MadHotSpot.Controllers
{
    public class EntegrasyonController : Controller
    {

        public OtelAppDbContext context;
        private IConfiguration config;

        public EntegrasyonController(OtelAppDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            config = _configuration;
        }

        public IActionResult Index()
        {
            return null;

        }

        public IActionResult KullaniciBas(Guid FirmaId)
        {

            var data = context.H_Ayarlar.Where(x => x.FirmaId == FirmaId).FirstOrDefault();

            if (data != null)
            {
                if (data.DiaEntegrasyonAktif)
                {
                    string m_strFilePath = data.DiaUrl;
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(m_strFilePath); //Load NOT LoadXml

                    XmlSerializer serializer = new XmlSerializer(typeof(Misafirler));
                    using (StringReader reader = new StringReader(myXmlDocument.OuterXml))
                    {
                        var diaData = (Misafirler)serializer.Deserialize(reader);

                        if (diaData != null && diaData.Misafir != null)
                        {
                            foreach (var x in diaData.Misafir)
                            {
                                if (kayitvarmi(x)) continue;
                                try
                                {
                                    using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, data.MikrotikIp, int.Parse(data.MikrotikPort), data.MikrotikUser, data.MikrotikPass))
                                    {
                                        DateTime dt;
                                        DateTime CheckOut;
                                        if (DateTime.TryParse(x.Dogumtarihi, out dt))
                                        {
                                            if (!DateTime.TryParse(x.Ayrilistarihi, out CheckOut))
                                            {
                                                CheckOut = DateTime.Now.AddDays(21);
                                            }else
                                            {
                                                CheckOut = CheckOut.AddDays(3);
                                            }
                                            
                                            var sure = CheckOut- DateTime.Now;
                                            var user = new tik4net.Objects.Ip.Hotspot.HotspotUser()
                                            {
                                                Profile = data.MikrotikProfilAdi,
                                                Server = data.MikrotikHotspotAdi,
                                                Name = dt.ToString("dd.MM.yyyy"),
                                                Password = x.Odano,
                                                LimitUptime = (sure.Days * 24).ToString() + ":00:00"
                                            };

                                            conn.Save(user);
                                            context.H_Rezervasyonlar.Add(new Rezervasyonlar
                                            {
                                                Odano = x.Odano,
                                                AyrilisTarihi = CheckOut,
                                                DogumTarihi = dt,
                                                MusteriAdi = x.Adisoyadi,
                                                KimlikNo = x.Pasaportno,
                                                Tcno = x.Tckimlikno,
                                                FirmaId = data.FirmaId,

                                            });

                                            context.SaveChanges();


                                        }

                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            kayitsil(data);
                        }


                    }
                }
            }



            return Ok(new Response { Success = true, Message = "Entegrasyon Yapıldı !" });
        }

        bool kayitvarmi(Misafir x)
        {
            DateTime dt;
 
            if (DateTime.TryParse(x.Dogumtarihi, out dt))
            {
                var data = context.H_Rezervasyonlar.Where(b => b.Odano == x.Odano && b.DogumTarihi == dt).FirstOrDefault();
                if (data == null) return false; else return true;
            }

            return false;
        }

        void kayitsil(Ayarlar data)
        {
            var d = context.H_Rezervasyonlar.Where(x => x.AyrilisTarihi < DateTime.Now && x.FirmaId==data.FirmaId).ToList();
            foreach (var x in d)
            {
                try
                {
                    using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, data.MikrotikIp, int.Parse(data.MikrotikPort), data.MikrotikUser, data.MikrotikPass))
                    {
                        var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotUser>(conn.CreateParameter("name", x.DogumTarihi.ToString("dd.MM.yyyy"))).FirstOrDefault();
                        conn.Delete(user);
                    }

                    SqlConnection con = new SqlConnection(config.GetConnectionString("OtelAppDatabase"));
                    SqlCommand cmd = new SqlCommand("DELETE FROM H_Rezervasyonlar where Id='" + x.Id.ToString() + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return null;
        }


    }
}
