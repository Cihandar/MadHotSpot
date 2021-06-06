using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class InternetSatis : BaseModel
    {
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public string VatNo { get; set; }
        public string Telefon { get; set; }
        public string Odano { get; set; }
        public double Tutar { get; set; }
        public double AlinanTutar { get; set; }
        public string Doviz { get; set; }
        public int Gun { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Aktarildi { get; set; }

    }
}
