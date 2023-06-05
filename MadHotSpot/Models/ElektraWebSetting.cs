using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class ElektraWebSetting : BaseModel
    {
        public int HotelId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int GelirGrubuId { get; set; }
        public int CashDepartmentId { get; set; }
        public int CreditDepartmentId { get; set; }

    }
}
