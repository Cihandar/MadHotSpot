using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class InternetSatisViewModel
    {
        public InternetSatis InternetSatis { get; set; }
        public Ayarlar Ayarlar { get; set; }
        public List<Tarifeler>  Tarifeler{ get; set; }
    }
}
