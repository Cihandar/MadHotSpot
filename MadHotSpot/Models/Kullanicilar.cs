using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Kullanicilar:BaseModel
    {
        public string KullaniciKodu { get; set; }
        public string Sifre { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Yetki { get; set; }
    }
}
