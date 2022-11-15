using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class Log : BaseModel
    {
        public DateTime Date { get; set; }
        public string Module { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }
        public string Mac { get; set; }
        public string LocalIp { get; set; }
    }
}
