using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Tarifeler : BaseModel
    {
        public string TarifeAdi { get; set; }
        public int Gun { get; set; }
        public double TLFiyat { get; set; }
        public double EUROFiyat { get; set; }
        public double USDFiyat { get; set; }
        public bool Aktif { get; set; }
    }
}
