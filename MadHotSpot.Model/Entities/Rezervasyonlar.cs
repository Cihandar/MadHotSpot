using System;

namespace MadHotSpot.Model.Entities
{
    public class Rezervasyonlar : BaseModel
    {
        public DateTime AyrilisTarihi { get; set; }
        public string KimlikNo { get; set; }
        public string Tcno { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Odano { get; set; }
        public string MusteriAdi { get; set; }
    }
}
