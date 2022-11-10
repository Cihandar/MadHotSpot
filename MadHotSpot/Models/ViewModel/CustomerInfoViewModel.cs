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

    }
}
