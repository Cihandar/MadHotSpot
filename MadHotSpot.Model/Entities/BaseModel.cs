using System;

namespace MadHotSpot.Model.Entities
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public Guid FirmaId { get; set; }
        public int FirmaKodu { get; set; }
        public bool Silindi { get; set; }

    }
}
