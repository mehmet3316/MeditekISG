using MeditekISG.CalisanBilgTablar;
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
    class CalisanBilgTabbed: TabbedPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public CalisanBilgTabbed()
        {
            Children.Add(new CalisanBilgTab1());
            Children.Add(new CalisanBilgTab2());
            Calisan();
        }
        private void Calisan()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<KISILER>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (CalisanListDetay.secilencalisantc == liste[i].TCKIMLIKNO)
               Title = liste[i].ADI+" "+ liste[i].SOYADI+" ("+ liste[i].TCKIMLIKNO+")";
            }
        }
    }
}
