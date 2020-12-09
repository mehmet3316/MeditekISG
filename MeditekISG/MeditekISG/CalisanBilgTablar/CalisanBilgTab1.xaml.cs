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

namespace MeditekISG.CalisanBilgTablar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalisanBilgTab1 : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public CalisanBilgTab1()
        {
            InitializeComponent();
            CalisanBilgileri();
        }
        private void CalisanBilgileri()
        {

            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<KISILER>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (CalisanListDetay.secilencalisantc == liste[i].TCKIMLIKNO)
                {
                    lblTCKIMLIKNO.Text = liste[i].TCKIMLIKNO;
                    lblADI.Text = liste[i].ADI;
                    lblSOYADI.Text = liste[i].SOYADI;
                    lblDOGUMTARIHI.Text = liste[i].DOGUMTARIHI;
                    if (liste[i].CINSIYETI == "1")
                        lblCINSIYETI.Text = "ERKEK";
                    else lblCINSIYETI.Text = "KADIN";
                    lblTELEFON.Text = liste[i].TELEFON;
                }
            }
           
        }
        private async void TC_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("TC KİMLİK NO :", "", initialValue: "TC KİMLİK", maxLength: 11);
        }
        private async void AD_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("ADI :", "", initialValue: "ADI", maxLength:30);
        }
        private async void SOYAD_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("SOYADI:", "", initialValue: "SOYAD", maxLength: 30);
        }
        private async void DOGUMTARIHI_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("DOĞUM TARİHİ:", "", initialValue: "TARİH", maxLength: 30);
        }
        private async void CINSIYET_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("CİNSİYETİ :", "", initialValue: "CİNSİYET", maxLength: 30);
        }
        private async void TELNO_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("TEL NO :", "", initialValue: "TELEFON", maxLength: 11);
        }
    }
}