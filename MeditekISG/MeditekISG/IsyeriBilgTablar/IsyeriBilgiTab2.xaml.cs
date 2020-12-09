using MeditekISG.Model.Veritabanları;
using MeditekISG.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.IsyeriBilgTablar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IsyeriBilgiTab2 : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public IsyeriBilgiTab2()
        {
            InitializeComponent();
            BilgilerDoldur();
        }
        private void BilgilerDoldur()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ISYERILISTESI>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (IsyeriListDetay.secilenisyeri == liste[i].ISYERIKODU)
                {
                    lblADRES.Text = liste[i].ADRESI;
                    lblIL.Text = liste[i].IL;
                    lblILCE.Text = liste[i].ILCE;
                    lblMAIL.Text = liste[i].EPOSTA;
                    lblTELNO.Text = liste[i].TELEFON;
                }
            }

        }
    }
}

