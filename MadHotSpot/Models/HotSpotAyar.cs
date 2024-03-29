﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class HotSpotAyar : BaseModel
    {
        public bool UcretsizHotspot { get; set; }
        public bool MisafirEmail { get; set; }
        public bool MisafirEmailZorunlu { get; set; }
        public bool MisafirTelefon { get; set; }
        public bool MisafirTelefonZorunlu { get; set; }

        public bool ToplantiGirisi { get; set; }
        public bool ToplantiEmail { get; set; }
        public bool ToplantiEmailZorunlu { get; set; }
        public bool ToplantiTelefon { get; set; }
        public bool ToplantiTelefonZorunlu { get; set; }

        public bool SpaGirisi { get; set; }
        public bool SpaEmail { get; set; }
        public bool SpaEmailZorunlu { get; set; }
        public bool SpaTelefon { get; set; }
        public bool SpaTelefonZorunlu { get; set; }

        public bool GunuBirlikGirisi { get; set; }
        public bool GunuBirlikEmail { get; set; }
        public bool GunuBirlikEmailZorunlu { get; set; }
        public bool GunuBirlikTelefon { get; set; }
        public bool GunuBirlikTelefonZorunlu { get; set; }

        public bool PersonelGirisi { get; set; }

        public string LogoUrl { get; set; }
        public string ArkaPlanUrl { get; set; }

        public string KvkkMetni { get; set; }
        

    }
}
