using MeditekISG.Model;
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
    public partial class IsyeriBilgiTab1 : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public IsyeriBilgiTab1()
        {
            InitializeComponent();
            BilgilerDoldur();
        }

          private void BilgilerDoldur()
          {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ISYERILISTESI>().ToList();
            var kullaniciliste = db.Table<T_PERSONEL>().ToList();
            for (int i=0;i<liste.Count;i++)
            {
                if(IsyeriListDetay.secilenisyeri==liste[i].ISYERIKODU&& liste[i].SUNUCUID == SunucuSecim.secilensunucuid)
                {
                    lblKISAADI.Text = liste[i].KISAADI;
                    lblUNVAN.Text = liste[i].UNVANI;
                    for(int j=0;j<kullaniciliste.Count;j++)
                    {
                        if(kullaniciliste[j].TCKIMLIKNO==liste[i].IGUZMANI &&kullaniciliste[j].SUNUCUID == SunucuSecim.secilensunucuid)
                        {
                            lblUZMANBILG.Text = kullaniciliste[j].ADI + " " + kullaniciliste[j].SOYADI;

                        }
                    }
                    for (int j = 0; j < kullaniciliste.Count; j++)
                    {
                        if (kullaniciliste[j].TCKIMLIKNO == liste[i].ISYERIHEKIMI && kullaniciliste[j].SUNUCUID == SunucuSecim.secilensunucuid)
                        {
                            lblHEKIM.Text = kullaniciliste[j].ADI + " " + kullaniciliste[j].SOYADI;

                        }
                    }
                    if(liste[i].TEHLIKESINIFI==3)
                    {
                        lblTEHLIKE.Text = "Çok Tehlikeli";
                        lblTEHLIKE.TextColor = Color.Red;
                    }
                    if (liste[i].TEHLIKESINIFI == 2)
                    {
                        lblTEHLIKE.Text = "Tehlikeli";
                        lblTEHLIKE.TextColor = Color.OrangeRed;
                    }
                    if (liste[i].TEHLIKESINIFI == 1)
                    {
                        lblTEHLIKE.Text = "Az Tehlikeli";
                        lblTEHLIKE.TextColor = Color.Green;
                    }
                    lblNACEKOD.Text = liste[i].NACEKODU;
                    
                }
            }

          

        }
    }
}