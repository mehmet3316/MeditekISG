using MeditekISG.Model;
using MeditekISG.View;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.Popuplar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SunucuDuzenlePop : PopupPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        public SunucuDuzenlePop()
        {
        
            InitializeComponent();
            Bilgiler();
        }
        private void Bilgiler()
        {

            var db = new SQLiteConnection(dbpath);
            var model = db.Table<Sunucular>().ToList();
           for(int i=0;i<model.Count;i++)
            {
                if(model[i].ID==SunucuSecim.sunucuduzenleid)
                {
                    tbKullaniciAdi.Text = model[i].KULLANICI;
                    tbParola.Text = model[i].PAROLA;
                    tbSunucu.Text = model[i].SUNUCU;
                    tbTanım.Text = model[i].TANIM;
                    tbVeritabani.Text = model[i].VERITABANI;
                }
                   
            }
        }

        private async void Duzelt_Clicked(object sender, EventArgs e)
        {
            if (tbVeritabani.Text != null && tbTanım.Text != null && tbKullaniciAdi.Text != null && tbParola.Text != null && tbSunucu.Text != null)
            {
                var db = new SQLiteConnection(dbpath);
                Sunucular sunucu = new Sunucular()
                {
                    ID = SunucuSecim.sunucuduzenleid,
                    SUNUCU = tbSunucu.Text,
                    VERITABANI = tbVeritabani.Text,
                    TANIM = tbTanım.Text,
                    KULLANICI = tbKullaniciAdi.Text,
                    PAROLA = tbParola.Text


                };
                db.Update(sunucu);
                string action = await DisplayActionSheet("Düzeltme Başarılı", null, null, "Tamam");
                Debug.WriteLine("Action: " + action);
                if (action == "Tamam")
                    IslemBasarili();
            }
            else
            {
                await DisplayAlert("Hata!!", "Alanları Doldurunuz", "Tamam");
            }
             
        }
        private void IslemBasarili()
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        private void PopKapa(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}