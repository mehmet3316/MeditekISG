using MeditekISG.Model;
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
    public partial class IsyeriListDetay : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
      SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        private readonly List<IsyeriList> model = new List<IsyeriList>();
        IsyeriList isyerleri = new IsyeriList();
        public static string secilenisyeri = null;
        public IsyeriListDetay()
        {
            InitializeComponent();
            ListeData();
        }
        private void ListeData()
        {
            var db = new SQLiteConnection(dbpath);
            var liste= db.Table<ISGB_ISYERILISTESI>().ToList();
            for (int i = 0; i < liste.Count; i++)
            {
                if (liste[i].SUNUCUID==SunucuSecim.secilensunucuid)
                {
                    model.Add(new IsyeriList
                    {
                        ISYERIKODU = liste[i].ISYERIKODU,
                        KISAADI = liste[i].KISAADI,
                        UNVANI = liste[i].UNVANI
                    });
                }
             
            }
            IsyeriListesi.ItemsSource = model;

        }
        private void IsyeriAra (object o, TextChangedEventArgs e) //Arama yapılrıken çalışan fonksiyonumuz
        {
            
           //Search texboxa yazılan kelimeyi anlık alır textchange ile
            var kelime = IsyeriArama.Text;
            //Aranan kelime eşleştirmeyi işyeri uzun adı ile yapmaktadır
            IsyeriListesi.ItemsSource = model.Where(x => x.UNVANI.ToLower().Contains(kelime.ToLower()));
        }

        private async void IsyeriSec(object sender, SelectedItemChangedEventArgs e)
        {
            
            isyerleri = (IsyeriList)e.SelectedItem;
            secilenisyeri = isyerleri.ISYERIKODU;
            await Navigation.PushModalAsync(new AnasayfaMaster());
        }
    }
}