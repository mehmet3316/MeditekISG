using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model
{
    class Sunucular
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string TANIM { get; set; }
        public string SUNUCU { get; set; }
        public string VERITABANI { get; set; }
        public string KULLANICI { get; set; }
        public string PAROLA { get; set; }


    }
}
