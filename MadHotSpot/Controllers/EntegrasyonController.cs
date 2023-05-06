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
using RestSharp;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

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

        public async Task<IActionResult> KullaniciBasAsync(Guid FirmaId)
        {
            var data = context.H_Ayarlar.FirstOrDefault(x => x.FirmaId == FirmaId);
            if (data != null)
            {
                Misafirler integrationData = new Misafirler();
                if (data.DiaEntegrasyonAktif)
                {
                    string m_strFilePath = data.DiaUrl;
                    XmlDocument myXmlDocument = new XmlDocument();
                    myXmlDocument.Load(m_strFilePath); //Load NOT LoadXml


                    XmlSerializer serializer = new XmlSerializer(typeof(Misafirler));
                    using (StringReader reader = new StringReader(myXmlDocument.OuterXml))
                    {
                        integrationData = (Misafirler)serializer.Deserialize(reader);
                    }
                }
                else if (data.ElektraEntegrasyonAktif)
                {
                    if (!string.IsNullOrWhiteSpace(data.ElektraUser))
                        integrationData = await GetElektraWebCustomers(data);
                }

                if (integrationData != null && integrationData.Misafir != null)
                {
                    //TODO: DUĞHAN
                    foreach (var x in integrationData.Misafir)
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
                                    }
                                    else
                                    {
                                        CheckOut = CheckOut.AddDays(3);
                                    }

                                    var sure = CheckOut - DateTime.Now;
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

            return Ok(new Response { Success = true, Message = "Entegrasyon Yapıldı !" });
        }

        bool kayitvarmi(Misafir x)
        {
            DateTime dt;

            if (DateTime.TryParse(x.Dogumtarihi, out dt))
            {
                var data = context.H_Rezervasyonlar.FirstOrDefault(b => b.Odano == x.Odano && b.DogumTarihi == dt);
                return data != null;
            }

            return false;
        }

        void kayitsil(Ayarlar data)
        {
            var d = context.H_Rezervasyonlar.Where(x => x.AyrilisTarihi < DateTime.Now && x.FirmaId == data.FirmaId).ToList();
            foreach (var x in d)
            {
                try
                {
                    using (var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, data.MikrotikIp, int.Parse(data.MikrotikPort), data.MikrotikUser, data.MikrotikPass))
                    {
                        var user = conn.LoadList<tik4net.Objects.Ip.Hotspot.HotspotUser>(conn.CreateParameter("name", x.DogumTarihi.ToString("dd.MM.yyyy"))).FirstOrDefault();
                        conn.Delete(user);
                    }
                    //TODO : DUĞHAN
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

        public async Task<string> GetElektraTokenAsync(Ayarlar ayar)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://4001.hoteladvisor.net"),
                    Method = new HttpMethod("Post"),
                    Content = new StringContent("{\n\"Action\": \"Login\",\n\"Tenant\": \"" + ayar.ElektraTenantId + "\",\n\"Usercode\": \"" + ayar.ElektraUser + "\",\n\"Password\": \"" + ayar.ElektraPassword + "\"\n}  \n ", null, "text/plain")
                };

                var response = await httpClient.SendAsync(request);

                var loginModel = JsonConvert.DeserializeObject<ElektraLoginModel>(await response.Content.ReadAsStringAsync());

                return loginModel.LoginToken;
            }
        }

        public async Task<Misafirler> GetElektraWebCustomers(Ayarlar ayar)
        {
            // var loginToken = await GetElektraTokenAsync(ayar); 

            var options = new RestClientOptions("http://4001.hoteladvisor.net")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Authorization", "Bearer " + ayar.ElektraUser);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
" + "\n" +
            @"    ""Parameters"": {
" + "\n" +
            @"        ""HOTELID"": " + ayar.ElektraTenantId + @"
" + "\n" +
            @"    },
" + "\n" +
            @"    ""Action"": ""Function"",
" + "\n" +
            @"    ""Object"": ""FN_EASYPMS_HOTSPOT_GUESTS"",
" + "\n" +
            @"    ""OrderBy"": [
" + "\n" +
            @"        {
" + "\n" +
            @"            ""Column"": ""null"",
" + "\n" +
            @"            ""Direction"": null
" + "\n" +
            @"        }
" + "\n" +
            @"    ],
" + "\n" +
            @"    ""Paging"": {
" + "\n" +
            @"        ""ItemsPerPage"": 2000,
" + "\n" +
            @"        ""Current"": 1
" + "\n" +
            @"    },
" + "\n" +
            @"    ""Where"": []
" + "\n" +
            @"}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            var elektraCustomers = new List<List<ElektraCustomer>>();
            try
            {
                elektraCustomers = JsonConvert.DeserializeObject<List<List<ElektraCustomer>>>(response.Content);
            }
            catch (Exception ex)
            {
            }

            var misafir = new Misafirler();
            misafir.Misafir = new List<Misafir>();
            if (elektraCustomers != null)
            {

                foreach (var item in elektraCustomers.FirstOrDefault().ToList())
                {
                    misafir.Misafir.Add(new Misafir
                    {
                        Adisoyadi = string.Concat(item.NAME, " ", item.LNAME),
                        Ayrilistarihi = item.CHECKOUT.ToString("dd.MM.yyyy"),
                        Dogumtarihi = item.BIRTHDATE,
                        Pasaportno = item.PASSPORTNO,
                        Tckimlikno = item.NATIONALIDNO,
                        Voucherno = item.VOUCHERNO,
                        Odano = item.ROOMNO
                    });
                }
            }
            else
            {
                return null;
            }

            return misafir;

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return null;
        }

        public class ElektraLoginModel
        {
            public bool Success { get; set; }
            public string LoginToken { get; set; }
            public string Code { get; set; }
            public string Usercode { get; set; }
            public string RoleName { get; set; }
            public int AdminLevel { get; set; }
            public string TenantColumn { get; set; }
            public string TenantTable { get; set; }
            //  public Tenancy Tenancy { get; set; }
            public string Message { get; set; }
        }

        public class ElektraCustomer
        {

            public string NAME { get; set; }
            public string LNAME { get; set; }
            public string ROOMNO { get; set; }
            public string NATIONALIDNO { get; set; }
            public string PASSPORTNO { get; set; }
            public string BIRTHDATE { get; set; }
            public DateTime CHECKIN { get; set; }
            public DateTime CHECKOUT { get; set; }
            public string PHONE { get; set; }
            public string NATIONALITY { get; set; }
            public string RESID { get; set; }
            public string EMAIL { get; set; }
            public string AGENCY { get; set; }
            public string AGENCYID { get; set; }
            public string HOTSPOTMODE { get; set; }
            public string CARDNO { get; set; }
            public string RESNAMEID { get; set; }
            public string PAX { get; set; }
            public string HOTELID { get; set; }
            public string VOUCHERNO { get; set; }

        }
    }
}
