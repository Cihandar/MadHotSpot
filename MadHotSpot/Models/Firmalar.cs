using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Firmalar:BaseModel
    {
        public string FirmaAdi { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string YetkiliAdSoyad { get; set; }
        public string Password { get; set; }
        public bool Aktif { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

    }
}
