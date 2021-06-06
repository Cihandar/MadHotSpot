using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Ayarlar : BaseModel
    {
        public bool SinirsizAktif { get; set; }
        
        public double GunlukFiyatTL{ get; set; }
        public double GunlukFiyatUSD { get; set; }
        public double GunlukFiyatEURO { get; set; }
        
        public string MikrotikIp { get; set; }
        public string MikrotikPort { get; set; }
        public string MikrotikUser { get; set; }
        public string MikrotikPass { get; set; }

        public string MikrotikDefaultSifre { get; set; }

        public bool AdSoyadZorunlu { get; set; }

    }
}
