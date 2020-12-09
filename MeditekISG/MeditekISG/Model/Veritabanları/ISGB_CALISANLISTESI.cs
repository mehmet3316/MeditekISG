using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class ISGB_CALISANLISTESI
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string ISYERIKODU { get; set; }
        public string KISITURU { get; set; }
        public string ISEGIRISTARIHI { get; set; }
        public string CALISTIGIBOLUM { get; set; }
        public int AKTARIM { get; set; }
        public int SUNUCUID { get; set; }

    }
}
