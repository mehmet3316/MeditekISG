using MeditekISG.Model.Veritabanları;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        public Master()
        {
            InitializeComponent();
            KullaniciBilg();
        }
        private void KullaniciBilg()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<T_PERSONEL>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (Giris.kullaniciid == liste[i].ID&&liste[i].SUNUCUID==SunucuSecim.secilensunucuid)
                    lbladsoyad.Text = liste[i].ADI+" "+ liste[i].SOYADI;
            }

        }
        private async void Anasayfa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AnasayfaMaster());
        }
        private async void IsyeriList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new IsyeriListeMaster());
        }
        private async void IsyeriBilg_Clicked(object sender, EventArgs e)
        {
            if(IsyeriListDetay.secilenisyeri!=null)
            await Navigation.PushModalAsync(new IsyeriBilgMaster());
            else
            {
                string action = await DisplayActionSheet("İşyeri Seçiniz!!", null, null, "İşyeri Listesi");
                Debug.WriteLine("Action: " + action);
                if (action == "İşyeri Listesi")
                {
                    await Navigation.PushModalAsync(new IsyeriListeMaster());
                }
            }
              
        }
        private async void CalisanList_Clicked(object sender, EventArgs e)
        {
            if (IsyeriListDetay.secilenisyeri != null)
                await Navigation.PushModalAsync(new CalisanListMaster());
            else
            {
                string action = await DisplayActionSheet("İşyeri Seçiniz!!", null, null, "İşyeri Listesi");
                Debug.WriteLine("Action: " + action);
                if (action == "İşyeri Listesi")
                {
                    await Navigation.PushModalAsync(new IsyeriListeMaster());
                }
            }
        }
        private async void Ziyaret_Clicked(object sender, EventArgs e)
        {
            if (IsyeriListDetay.secilenisyeri != null)
                await Navigation.PushModalAsync(new ZiyaretListMaster());
            else
            {
                string action = await DisplayActionSheet("İşyeri Seçiniz!!", null, null, "İşyeri Listesi");
                Debug.WriteLine("Action: " + action);
                if (action == "İşyeri Listesi")
                {
                    await Navigation.PushModalAsync(new IsyeriListeMaster());
                }
            }

        }
        private async void Cikis_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Giris());
        }
     
    }
}