using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Staff : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string LastMac { get; set; }
        public string IdNumber { get; set; }
        public bool Active { get; set; }
        public string UserProfile { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MikrotikId { get; set; }
   
    }
}
