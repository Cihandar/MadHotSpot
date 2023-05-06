using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models.Enum;
namespace MadHotSpot.Models
{
    public class CustomerInfo : BaseModel
    {    
        public string RoomNumber { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Kvkk { get; set; }
        public bool Contact { get; set; }
        public LoginType LoginType { get; set; }
    }
}
