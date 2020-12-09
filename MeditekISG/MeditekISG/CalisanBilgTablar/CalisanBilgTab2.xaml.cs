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
    public partial class CalisanBilgTab2 : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public CalisanBilgTab2()
        {
            InitializeComponent();
            CalisanBilgileri();
        }
        private void CalisanBilgileri()
        {

            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_CALISANLISTESI>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (CalisanListDetay.secilencalisantc == liste[i].TCKIMLIKNO)
                {
                    if (liste[i].KISITURU == "1")
                        lblKISITURU.Text = "Sözleşmeli Çalışan";
                    else lblKISITURU.Text = liste[i].KISITURU;
                    lblBOLUM.Text = liste[i].CALISTIGIBOLUM;
                    lblGOREV.Text = "";
                    lblISEGIRISTARIH.Text = liste[i].ISEGIRISTARIHI;
                    lblISTENCIKIS.Text = "";
                }
            }
        }
        private async void CALISANTUR_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("ÇALIŞAN TÜRÜ :", "", initialValue: "ÇALIŞAN TÜR", maxLength: 30);
        }
        private async void BOLUM_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("ÇALIŞTIĞI BÖLÜM:", "", initialValue: "BÖLÜM ÇALIŞANI", maxLength: 30, keyboard: Keyboard.Numeric);
        }
        private async void GOREVI_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("BÖLÜMDEKİ GÖREVİ:", "", initialValue: "GÖREV", maxLength: 30, keyboard: Keyboard.Numeric);
        }
        private async void GIRISTARIH_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("İŞE GİRİŞ TARİHİ :", "", initialValue: "TARİH", maxLength: 30, keyboard: Keyboard.Numeric);
        }
        private async void CIKISTARIH_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("İŞTEN ÇIKIŞ TARİHİ :", "", initialValue: "TARİH", maxLength: 30, keyboard: Keyboard.Numeric);
        }
    }
}