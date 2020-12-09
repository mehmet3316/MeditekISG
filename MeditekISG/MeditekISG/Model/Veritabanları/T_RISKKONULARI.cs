using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class T_RISKKONULARI
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int SUNUCUID { get; set; }
        public string KONUKODU { get; set; }
        public string KONU{ get; set; }
        public string MEVZUAT { get; set; }
        public string TEHLIKE { get; set; }
        public string RISK { get; set; }
        public string FAALIYET { get; set; }
        public string TEHLIKEKAYNAGI { get; set; }
        public string MEVCUTDURUM { get; set; }
        public string ONERI { get; set; }
        public string OLASILIK { get; set; }
        public string SIDDET { get; set; }
  

    }
}
