using DocumentFormat.OpenXml.ExtendedProperties;
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
    public partial class SunucuSecim : ContentPage
    {
        public static int sunucuduzenleid,secilensunucuid,onlineofline;
        public static string sqlbaglanti = "";
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        private readonly List<Sunucular> model = new List<Sunucular>();
        Sunucular sunucular = new Sunucular();
        public SunucuSecim()
        {
            InitializeComponent();
            var db = new SQLiteConnection(dbpath);
            model = db.Table<Sunucular>().ToList();

            SunucularListe.ItemsSource = model;
        }

        private async void Sunucu_Sec(object sender, ItemTappedEventArgs e)
        {
            sunucular = (Sunucular)e.Item;
            string action = await DisplayActionSheet("Veritabanı İşlemleri", "İptal", "", "Seç", "Düzenle", "Sil");
            Debug.WriteLine("Action: " + action);
            if (action == "Seç")
            {
                BaglantiDurum(sunucular);
              
            }
                
            else if (action == "Düzenle")
            {
                sunucuduzenleid = sunucular.ID;
                SunucuDuzenle();
            }
        }

        private void SunucuDuzenle()
        {
            Navigation.PushPopupAsync(new SunucuDuzenlePop());
        }
        private async  void Yeni_Sunucu(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new YeniSunucuEkle());
        
        }
        private async void BaglantiDurum(Sunucular p)
        {
            string data= "Data Source=" + p.SUNUCU+";";
            string intial = "Initial Catalog=" + p.VERITABANI+";";
            string kullanici = "User Id=" + p.KULLANICI + ";";
            string parola = "password=" + p.PAROLA + ";";
            string baglantı = data + intial + kullanici + parola;
            SqlConnection cnn = new SqlConnection(baglantı);

            try
            {
                cnn.Open();
                string action = await DisplayActionSheet("Bağlantı Başarılı", null, null, "Devam");
                Debug.WriteLine("Action: " + action);
                if (action == "Devam")
                {
                    cnn.Close();
                    sqlbaglanti = baglantı;
                    onlineofline = 1;
                    secilensunucuid = p.ID;
                    await Navigation.PushAsync(new Giris());
                
                }
            }
            catch 
            {
                string action = await DisplayActionSheet("Bağlantı Başarısız", null, null, "Ofline Kullanım İçin Devam");
                Debug.WriteLine("Action: " + action);
                if(action== "Ofline Kullanım İçin Devam")
                {
                    List<ISGB_ISYERILISTESI> isyerilist = new List<ISGB_ISYERILISTESI>();
                    var db = new SQLiteConnection(dbpath);
                    isyerilist = db.Table<ISGB_ISYERILISTESI>().ToList();
                if(isyerilist.Count<1)
                    {
                        await DisplayAlert("HATA!", "İnternete Bağlanmalısınız", "Tamam");
                    }
                else
                    {
                        onlineofline = 2;
                          secilensunucuid = p.ID;
                        await Navigation.PushAsync(new Giris());
                    }
                }
            }
        }


    }
}