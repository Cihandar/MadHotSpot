using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tik4net;

namespace MadHotSpot.Models.ViewModel
{
    public class ResultMikrotikConnection
    {
        public bool result { get; set; }
        public string Message { get; set; }
        public ITikConnection tikConnection { get; set; }
        public Ayarlar ayarlar { get; set; }
    }
}
