using System.Collections.Generic;
using System.Xml.Serialization;

namespace MadHotSpot.Model.Entities
{
    [XmlRoot(ElementName = "misafir")]
    public class Misafir
    {

        [XmlElement(ElementName = "odano")]
        public string Odano { get; set; }

        [XmlElement(ElementName = "adisoyadi")]
        public string Adisoyadi { get; set; }

        //[XmlElement(ElementName = "gelistarihi")]
        //public string Gelistarihi { get; set; }

        //[XmlElement(ElementName = "gelissaati")]
        //public string Gelissaati { get; set; }

        [XmlElement(ElementName = "ayrilistarihi")]
        public string Ayrilistarihi { get; set; }

        //[XmlElement(ElementName = "ayrilissaati")]
        //public string Ayrilissaati { get; set; }

        [XmlElement(ElementName = "voucherno")]
        public string Voucherno { get; set; }

        [XmlElement(ElementName = "pasaportno")]
        public string Pasaportno { get; set; }

        [XmlElement(ElementName = "tckimlikno")]
        public string Tckimlikno { get; set; }

        [XmlElement(ElementName = "dogumtarihi")]
        public string Dogumtarihi { get; set; }
    }

    [XmlRoot(ElementName = "misafirler")]
    public class Misafirler
    {

        [XmlElement(ElementName = "misafir")]
        public List<Misafir> Misafir { get; set; }
    }

}
