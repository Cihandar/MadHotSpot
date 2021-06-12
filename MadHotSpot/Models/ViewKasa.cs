using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class ViewKasa  : BaseModel
    {
        public string Doviz { get; set; }
        public DateTime Tarih { get; set; }
        public double Satis { get; set; }
        public double Iade { get; set; }
        public double Bakiye { get; set; }
    }
}
