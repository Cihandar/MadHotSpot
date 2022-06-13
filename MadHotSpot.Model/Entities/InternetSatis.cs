using System;

namespace MadHotSpot.Model.Entities
{
    public class InternetSatis : BaseModel
    {
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public string VatNo { get; set; }
        public string Telefon { get; set; }
        public string Odano { get; set; }
        public double Tutar { get; set; }
        //  public double AlinanTutar { get; set; }
        public string Doviz { get; set; }
        public int Gun { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Aktarildi { get; set; }
        public bool Iade { get; set; }
        public double IadeEdilenTutar { get; set; }
        public string IadeEdilenDoviz { get; set; }
        public bool Tarife { get; set; }
        public Guid TarifeId { get; set; }
    }
}
