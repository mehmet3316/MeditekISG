using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class ISGB_ZIYARETLER
    {
        [PrimaryKey]
        public string KAYITKODU { get; set; }
        public string KULLANICI { get; set; }
        public string ISYERIKODU { get; set; }
        public int CALISMAYILI { get; set; }
        public DateTime ZIYARETTARIHI { get; set; }
        public string NOTLAR { get; set; }
        public int SUNUCUID { get; set; }
    }
}
