using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Rezervasyonlar : BaseModel
    {
        public DateTime AyrilisTarihi { get; set; }
        public string KimlikNo  { get; set; }
        public string Tcno { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Odano { get; set; }
        public string MusteriAdi { get; set; }
    }
}
