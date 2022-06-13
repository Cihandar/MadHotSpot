namespace MadHotSpot.Model.Entities
{
    public class HotSpotAyar : BaseModel
    {
        public bool UcretsizHotspot { get; set; }
        public bool MisafirEmail { get; set; }
        public bool MisafirEmailZorunlu { get; set; }
        public bool MisafirTelefon { get; set; }
        public bool MisafirTelefonZorunlu { get; set; }

        public bool ToplantiGirisi { get; set; }
        public bool ToplantiEmail { get; set; }
        public bool ToplantiEmailZorunlu { get; set; }
        public bool ToplantiTelefon { get; set; }
        public bool ToplantiTelefonZorunlu { get; set; }

        public bool PersonelGirisi { get; set; }

        public string LogoUrl { get; set; }
        public string ArkaPlanUrl { get; set; }

        public string KvkkMetni { get; set; }


    }
}
