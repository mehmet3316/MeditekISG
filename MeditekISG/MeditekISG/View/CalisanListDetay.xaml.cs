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
    public partial class CalisanListDetay : ContentPage
    {
        public static string secilencalisantc = null;

        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        private readonly List<CalisanList> model = new List<CalisanList>();
        public CalisanListDetay()
        {
            InitializeComponent();
            CalisanList();
        }
        private void CalisanList()
        {
            var db = new SQLiteConnection(dbpath);
            var isyeriliste = db.Table<ISGB_ISYERILISTESI>().ToList();
            for (int i = 0; i < isyeriliste.Count; i++)
            {
                if (IsyeriListDetay.secilenisyeri == isyeriliste[i].ISYERIKODU)
                    Title = isyeriliste[i].KISAADI;
            }

            var db1 = new SQLiteConnection(dbpath);
            var calisanliste = db1.Table<ISGB_CALISANLISTESI>().ToList();
            List<CalisanList> model1 = new List<CalisanList>();
          for(int j=0;j<calisanliste.Count;j++)
            {
                if(calisanliste[j].ISYERIKODU== IsyeriListDetay.secilenisyeri)
                {
                    model1.Add(new CalisanList
                    {

                        TCKIMLIKNO = calisanliste[j].TCKIMLIKNO

                    }) ;
                }
              
            }
            var db2 = new SQLiteConnection(dbpath);
            var kisilerliste = db2.Table<KISILER>().ToList();
            for(int k=0;k<kisilerliste.Count;k++)
            {
                for(int l=0;l<model1.Count;l++)
                {
                    if(kisilerliste[k].TCKIMLIKNO==model1[l].TCKIMLIKNO)
                    {
                        model.Add(new CalisanList
                        {

                            ADSOYAD = kisilerliste[k].ADI + " " + kisilerliste[k].SOYADI,
                            TCKIMLIKNO = kisilerliste[k].TCKIMLIKNO


                        }) ;
                    }
                }
             
            }
       


            CalisanListesi.ItemsSource = model;
        }
        private void CalisanAra(object o, TextChangedEventArgs e) //Arama yapılrıken çalışan fonksiyonumuz
        {
            //Search texboxa yazılan kelimeyi anlık alır textchange ile
            var kelime = CalisanArama.Text;
            //Aranan kelime eşleştirmeyi işyeri uzun adı ile yapmaktadır
           if(kelime.Length>2)
            {
                if (kelime.Substring(0, 1) == "0" || kelime.Substring(0, 1) == "1" || kelime.Substring(0, 1) == "2" || kelime.Substring(0, 1) == "3" || kelime.Substring(0, 1) == "4" ||
               kelime.Substring(0, 1) == "5" || kelime.Substring(0, 1) == "6" || kelime.Substring(0, 1) == "7" || kelime.Substring(0, 1) == "8" || kelime.Substring(0, 1) == "9")
                {
                    CalisanListesi.ItemsSource = model.Where(x => x.TCKIMLIKNO.ToLower().Contains(kelime.ToLower()));
                }
                else
                {
                    CalisanListesi.ItemsSource = model.Where(x => x.ADSOYAD.ToLower().Contains(kelime.ToLower()));
                }
            }
            else
            {
                CalisanListesi.ItemsSource = model.Where(x => x.ADSOYAD.ToLower().Contains(kelime.ToLower()));
            }

        }

        private async void CalisanListesi_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            CalisanList c = new CalisanList();
            c = (CalisanList)e.SelectedItem;
            secilencalisantc = c.TCKIMLIKNO;
            await Navigation.PushModalAsync(new CalisanBilgMaster());
        }
    }
}