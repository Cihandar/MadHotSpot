using MadHotSpot.Model.Enums;

namespace MadHotSpot.Model.Entities
{
    public class Kullanicilar : BaseModel
    {
        public string KullaniciKodu { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public PermissionEnum Yetki { get; set; }
    }
}
