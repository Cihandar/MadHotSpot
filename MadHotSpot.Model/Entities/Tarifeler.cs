namespace MadHotSpot.Model.Entities
{
    public class Tarifeler : BaseModel
    {
        public string TarifeAdi { get; set; }
        public int Gun { get; set; }
        public double TLFiyat { get; set; }
        public double EUROFiyat { get; set; }
        public double USDFiyat { get; set; }
        public bool Aktif { get; set; }
    }
}
