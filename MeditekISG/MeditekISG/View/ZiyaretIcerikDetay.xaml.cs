using MeditekISG.Model.Veritabanları;
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

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZiyaretIcerikDetay : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public ZiyaretIcerikDetay()
        {
            InitializeComponent();
            IsyeriAdi();
            ZiyaretBilgileri();
        }
        private void IsyeriAdi()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ISYERILISTESI>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (IsyeriListDetay.secilenisyeri == liste[i].ISYERIKODU)
                    Title = liste[i].KISAADI;
            }
        }
        private void ZiyaretBilgileri()
        {

            if(ZiyaretListDetay.secilenziyaretplankod!=null && ZiyaretListDetay.secilenziyaretplankod.Length>5)
            {
                var db = new SQLiteConnection(dbpath);
                var liste = db.Table<ISGB_YILLIKPLAN>().ToList();
                for (int i = 0; i < liste.Count; i++)
                {
                    if (ZiyaretListDetay.secilenziyaretplankod == liste[i].PLANKODU)
                    {
                        lblDURUM.Text = "PLANLANDI";
                        lblKAYITSAHIBI.Text = "";
                        lblNOTLAR.Text = "";
                        lblSORUSAYI.Text = "0";
                        lblUYGUNKONU.Text = "0";
                        lblUYGUNOLMAYANKONU.Text = "0";
                        lblTARIH.Text =liste[i].ILKTARIH.ToString().Substring(0, 10);
                        lblSAAT.Text = "00.00";
                    }

                }
            }
            else if(ZiyaretListDetay.secilenziyaretkod!=null && ZiyaretListDetay.secilenziyaretkod.Length>5)
            {
                var db = new SQLiteConnection(dbpath);
                var liste = db.Table<ISGB_ZIYARETLER>().ToList();
                for (int i = 0; i < liste.Count; i++)
                {
                    if (ZiyaretListDetay.secilenziyaretkod == liste[i].KAYITKODU)
                    {
                        lblDURUM.Text = "GERÇEKLEŞTİRİLDİ";
                        var kullanici = db.Table<T_PERSONEL>().ToList();
                        for(int k=0;k<kullanici.Count;k++)
                        {
                            if (liste[i].KULLANICI == kullanici[k].TCKIMLIKNO)
                                lblKAYITSAHIBI.Text = kullanici[k].ADI + " " + kullanici[k].SOYADI;
                        }
                        lblNOTLAR.Text = liste[i].NOTLAR;
                        lblTARIH.Text = liste[i].ZIYARETTARIHI.ToString().Substring(0, 10);
                        lblSAAT.Text = liste[i].ZIYARETTARIHI.ToString().Substring(10, 5);
                    }
                }

                SORUKONUGETIR();
            }
        }
        private void SORUKONUGETIR()
        {
            int soru = 0, uygun = 0, uygunsuz =0;

            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ZIYARETLER_KONULAR>().ToList();
            for(int i=0;i<liste.Count;i++)
            {
                if(ZiyaretListDetay.secilenziyaretkod==liste[i].ZIYARETKODU)
                {
                    soru++;
                    if (liste[i].UYGUNLUK == 2)
                        uygunsuz++;
                    else uygun++;
                }
            }
            lblSORUSAYI.Text = soru.ToString();
            lblUYGUNKONU.Text = uygun.ToString();
            lblUYGUNOLMAYANKONU.Text = uygunsuz.ToString();
        }

        private async void DenetimBasla(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ZiyaretDenetim());

        }
    }
}