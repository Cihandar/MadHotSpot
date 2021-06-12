using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models.Enum;

namespace MadHotSpot.Models
{
    public class Kullanicilar:BaseModel
    {
        public string KullaniciKodu { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public PermissionEnum Yetki { get; set; }
    }
}
