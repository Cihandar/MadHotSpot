namespace MadHotSpot.Model.Entities
{
    public class Ayarlar : BaseModel
    {
        public bool SinirsizAktif { get; set; }

        public double GunlukFiyatTL { get; set; }
        public double GunlukFiyatUSD { get; set; }
        public double GunlukFiyatEURO { get; set; }

        public string MikrotikIp { get; set; }
        public string MikrotikPort { get; set; }
        public string MikrotikUser { get; set; }
        public string MikrotikPass { get; set; }
        public string MikrotikHotspotAdi { get; set; }
        public string MikrotikProfilAdi { get; set; }


        public string MikrotikDefaultSifre { get; set; }

        public bool AdSoyadZorunlu { get; set; }

        public bool IadeAktif { get; set; }

        public bool TarifeAktif { get; set; }
        public bool DiaEntegrasyonAktif { get; set; }
        public string DiaUrl { get; set; }
    }
}
