using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model
{
    class GuncellemeDurum
    { [PrimaryKey]
        public int ID { get; set; }
        public string SONGUNCELLEMETARIHI { get; set; }
        public int SUNUCUID { get; set; }
    }
}
