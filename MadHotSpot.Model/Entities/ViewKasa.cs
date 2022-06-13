using System;

namespace MadHotSpot.Model.Entities
{
    public class ViewKasa : BaseModel
    {
        public string Doviz { get; set; }
        public DateTime Tarih { get; set; }
        public double Satis { get; set; }
        public double Iade { get; set; }
        public double Bakiye { get; set; }
    }
}
