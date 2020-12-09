using MeditekISG.Model;
using MeditekISG.Model.Veritabanları;
using MeditekISG.Popuplar;
using Rg.Plugins.Popup.Extensions;
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
    public partial class ZiyaretDenetim : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        private readonly List<Ziyaretler_Konular> model = new List<Ziyaretler_Konular>();
        public ZiyaretDenetim()
        {
            InitializeComponent();
            ZiyaretKonulariListele();
        }
        private void ZiyaretKonulariListele()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ZIYARETLER_KONULAR>().ToList();
            int j = 0;
            for (int i = 0; i < liste.Count; i++)
            { if(ZiyaretListDetay.secilenziyaretkod==liste[i].ZIYARETKODU)
                {
                    j++;
                    model.Add(new Ziyaretler_Konular
                    {
                        SIRA = j,
                        KONU = liste[i].KONU,
                        KONUKODU = liste[i].KONUKODU
                    });
                }
            }
            baglan.Close();
            ZiyaretKonulariList.ItemsSource = model;
        }

        private void KONUARAMA_TextChanged(object sender, TextChangedEventArgs e)
        {
                //Search texboxa yazılan kelimeyi anlık alır textchange ile
                var kelime = KONUARAMA.Text;
                //Aranan kelime eşleştirmeyi işyeri uzun adı ile yapmaktadır
                ZiyaretKonulariList.ItemsSource = model.Where(x => x.KONU.ToLower().Contains(kelime.ToLower()));            
        }

        private async void KONUEKLE(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Konu Ekle", "İptal", "", "Kütüphaneden Seç", "Elle Ekle");
            Debug.WriteLine("Action: " + action);
            if(action== "Kütüphaneden Seç")
            {
                TehlikeKonulari();
            }
            else if(action== "Elle Ekle")
            {
                await Navigation.PushAsync(new ZiyaretKonuEkle());
            }
        }
        private void TehlikeKonulari()
        {
            Navigation.PushPopupAsync(new TehlikeKonularıListPop());
        }
    }
}