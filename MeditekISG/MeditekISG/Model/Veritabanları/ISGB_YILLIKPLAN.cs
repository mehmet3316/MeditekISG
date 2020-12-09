using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model.Veritabanları
{
    class ISGB_YILLIKPLAN
    {
        [PrimaryKey]
        public string PLANKODU { get; set; }
        public DateTime ILKTARIH { get; set; }
        public string ISYERIKODU { get; set; }
        public int SUNUCUID { get; set; }
    }
}
