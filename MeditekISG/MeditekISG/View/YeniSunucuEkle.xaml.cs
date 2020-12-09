using MeditekISG.Model;
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

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YeniSunucuEkle : ContentPage
    {
       
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        public YeniSunucuEkle()
        {
            InitializeComponent();
        }

        private async void Sunucu_Kaydet(object sender, EventArgs e)
        {
            try
            {
                if (tbVeritabani.Text != null && tbTanım.Text != null && tbKullaniciAdi.Text != null && tbParola.Text != null&&tbSunucu.Text!=null)
                {
                    var db = new SQLiteConnection(dbpath);
                    var idkontrol = db.Table<Sunucular>().OrderByDescending(a => a.ID).FirstOrDefault();
                    Sunucular sunucu = new Sunucular()
                    {
                        ID = (idkontrol == null ? 1 : idkontrol.ID + 1),
                        TANIM = tbTanım.Text,
                        VERITABANI = tbVeritabani.Text,
                        KULLANICI = tbKullaniciAdi.Text,
                        PAROLA = tbParola.Text,
                        SUNUCU=tbSunucu.Text
                    };
                    db.Insert(sunucu);
                    string action = await DisplayActionSheet("İşlem Tamamlandı", null, null, "Tamam");
                    Debug.WriteLine("Action: " + action);
                    if (action == "Tamam")
                        await Navigation.PushAsync(new SunucuSecim());
                }
                else
                    await DisplayAlert("Hata!!", "Alanları Doldurunuz", "Tamam");

            }
            catch(Exception ex)
            {
            
                await DisplayAlert("Hata!!", ex.ToString(), "Tamam") ;
            }
           

         
        }
    }
}