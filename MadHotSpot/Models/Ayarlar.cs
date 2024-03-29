﻿using System;
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
        public string MikrotikHotspotAdi { get; set; }
        public string MikrotikProfilAdi { get; set; }
        public string MikrotikDefaultSifre { get; set; }
        public bool AdSoyadZorunlu { get; set; }
        public bool IadeAktif { get; set; }
        public bool TarifeAktif { get; set; }
        public bool DiaEntegrasyonAktif { get; set; }
        public string DiaUrl { get; set; }
        public bool ElektraEntegrasyonAktif { get; set; }
        public string ElektraTenantId { get; set; }
        public string ElektraUser { get; set; }
        public string ElektraPassword { get; set; }
        public bool ManuelGuestAdd { get; set; }
        public string ElektraFreeProfile { get; set; }
        public string ElektraPaidProfile { get; set; }
        public string ElektraHighSpeedProfile { get; set; }
    }
}
