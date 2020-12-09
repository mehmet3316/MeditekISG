using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class ISGB_ISYERILISTESI
    {
        [PrimaryKey]
        public string ISYERIKODU { get; set; }
        public string UNVANI { get; set; }
        public string KISAADI { get; set; }
        public string IL { get; set; }
        public string ILCE { get; set; }
        public string ADRESI { get; set; }
        public string TELEFON { get; set; }
        public string EPOSTA { get; set; }
        public string NACEKODU { get; set; }
        public string ISKOLU { get; set; }
        public string IGUZMANI { get; set; }
        public string ISYERIHEKIMI { get; set; }
        public int TEHLIKESINIFI { get; set; }
        public int AKTARIM { get; set; }
        public int SUNUCUID { get; set; }

    }
}
