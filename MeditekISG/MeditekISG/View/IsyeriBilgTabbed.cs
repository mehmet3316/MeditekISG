using MeditekISG.IsyeriBilgTablar;
using MeditekISG.Model.Veritabanları;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MeditekISG.View
{
    class IsyeriBilgTabbed: TabbedPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public IsyeriBilgTabbed()
        {
            Isyeri();


            Children.Add(new IsyeriBilgiTab1()); //İşyeri bilgileri tabıdır ekran tasarımı IsyeriBilgTab Klasörü İçerisinde
            Children.Add(new IsyeriBilgiTab2()); //İşyeri İletişim Bilgileri tabıdır ekran tasarımı IsyeriBilgTab Klasörü İçerisinde
        }
        private void Isyeri()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ISYERILISTESI>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (IsyeriListDetay.secilenisyeri == liste[i].ISYERIKODU)
                    Title = liste[i].KISAADI;
            }
        }
    }
}
