using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeditekISG.Model
{
     public class IsyeriList
    {
        [PrimaryKey]
        public string ISYERIKODU { get; set; }
        public string UNVANI { get; set; }
        public string KISAADI { get; set; }
    }
}
