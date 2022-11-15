using MadHotSpot.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models.ViewModel
{
    public class CustomerInfoViewModel
    {
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public Guid FirmaId { get; set; }
        public string PhoneNumber { get; set; }
        public string RoomNumber { get; set; }
        public string HotspotType { get; set; }
        public string IdNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Mac { get; set; }
        public string LocalIp { get; set; }
        public LoginType LoginType { get; set; }

    }
}
