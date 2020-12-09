using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class KISILER
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string ADI { get; set; }
        public string SOYADI { get; set; }
        public string DOGUMTARIHI { get; set; }
        public string CINSIYETI { get; set; }
        public string TELEFON { get; set; }
        public int AKTARIM { get; set; }
        public int SUNUCUID { get; set; }

    }
}
