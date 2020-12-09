using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class ISGB_ZIYARETLER_KONULAR
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string ZIYARETKODU { get; set; }
        public string KONU { get; set; }
        public string KONUKODU { get; set; }
        public string MEVZUAT { get; set; }
        public int UYGUNLUK { get; set; }
        public string MEVCUTDURUM { get; set; }
        public string TEHLIKEKAYNAGI { get; set; }
        public string FAALIYET { get; set; }
        public string TEHLIKE { get; set; }
        public string RISK { get; set; }
        public float GERCEKLESMEOLASILIGI { get; set; }
        public float GERCEKLESMESIDDETI { get; set; }
        public string ONLEMLER { get; set; }
        public string SURECSORUMLUSU { get; set; }
        public string ETKILENECEKKISILER { get; set; }
        public float SKOR { get; set; }
        public int AKTARIM { get; set; }
        public int SUNUCUID { get; set; }
    }
}
