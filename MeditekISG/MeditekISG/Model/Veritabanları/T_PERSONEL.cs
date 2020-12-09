using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class T_PERSONEL
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string ADI { get; set; }
        public string SOYADI { get; set; }
        public int DURUM { get; set; }
        public string SIFRE { get; set; }
        public int UNVANI { get; set; }
        public int GOREVI { get; set; }
        public string CEPTELELFONU { get; set; }
        public string EPOSTA { get; set; }
        public int SUNUCUID { get; set; }
    }
}
