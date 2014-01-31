using System;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace FZOM_Apteki.Models
{
    /// <summary>
    /// Оваа класа ги опфаќа сите елементи кои се дадени во XML документот кој е база на оваа апликација. Класата наслдедува од IComparable поради тоа што ги сортираме податоците од
    /// документот.
    /// </summary>
    [Serializable]
    public class Dogovor : IComparable<Dogovor>

    {
        public string ArhivskiBroj { get; set; }
        public string TipDogovorID { get; set; }
        public string TipNaDogovor { get; set; }
        public string DanocenBroj { get; set; }
        public string ShifraZU { get; set; }
        public string NazivNaUstanova { get; set; }
        public string Dejnosti { get; set; }
        public string Adresa { get; set; }
        public string NaselenoMesto { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Овој метод всушност го врши споредувањето кое е по името односно називот на здравствената установа
        /// </summary>
        /// <param name="other"> Објект од класата AptekiRow. Споредба на секој со секој објект добиен од XML документот и сортирање по името</param>
        /// <returns></returns>
        public int CompareTo(Dogovor other)
        {
            return this.NazivNaUstanova.CompareTo(other.NazivNaUstanova);
        }
    }
    /// <summary>
    /// Класа AptekiTable која претствува колекција од класи АптекиРоу. Потребна при серијализација и десеријализација од XML документот 
    /// </summary>
    [Serializable]
    [XmlRoot("Dogovori")]
    public class Dogovori : Collection<Dogovor> // Dogovori е листа од повеќе Dogovor
    {
    }
}