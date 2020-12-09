using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model
{
    class CalisanList
    {
      [PrimaryKey]
        public string TCKIMLIKNO { get; set; }
        public string ADSOYAD { get; set; }
        public string ADI{ get; set; }
        public string SOYADI { get; set; }
        public string DOGUMTARIHI { get; set; }
        public string CINSIYETI { get; set; }
        public string KISITURU { get; set; }
        public string CALISTIGIBOLUM { get; set; }
        public string GOREVI { get; set; }
        public string ISEGIRISTARIHI { get; set; }
        public string ISTENCIKISTARIHI { get; set; }
    }
}
