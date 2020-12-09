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
    public partial class ZiyaretListDetay : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        public static string secilenziyaretkod = null,secilenziyaretplankod=null;
        private readonly List<Ziyaretler> model1 = new List<Ziyaretler>();
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public ZiyaretListDetay()
        {
            InitializeComponent();
            ZIYARETLISTELE();
        }

        private void ZIYARETLISTELE()
        {


            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_YILLIKPLAN>().ToList();
            for (int t=0; t < liste.Count; t++)
            {
                if(liste[t].ISYERIKODU==IsyeriListDetay.secilenisyeri && liste[t].SUNUCUID==SunucuSecim.secilensunucuid)
                {
                    model1.Add(new Ziyaretler
                    {
                        DURUM = "PLANLANDI",
                        SORUSAYISI = "0",
                        ALTTARIH = liste[t].ILKTARIH.ToString(),
                        PLANKODU = liste[t].PLANKODU


                    });
                }
              
                }
            string kullanici = null;
            var db1 = new SQLiteConnection(dbpath);
            var kullanicilar = db.Table<T_PERSONEL>().ToList();
            var gerceklesliste = db.Table<ISGB_ZIYARETLER>().ToList();
            for (int i = 0; i < gerceklesliste.Count; i++)
            {
                if (gerceklesliste[i].ISYERIKODU == IsyeriListDetay.secilenisyeri && gerceklesliste[i].SUNUCUID == SunucuSecim.secilensunucuid)
                {
                    for (int j = 0; j < kullanicilar.Count; j++)
                    {
                        if (gerceklesliste[i].KULLANICI == kullanicilar[j].TCKIMLIKNO)
                            kullanici = kullanicilar[j].ADI + " " + kullanicilar[j].SOYADI;

                    }
                    model1.Add(new Ziyaretler
                    {
                        DURUM = "GERÇEKLEŞTİRİLDİ",
                        ALTTARIH = gerceklesliste[i].ZIYARETTARIHI.ToString(),
                        ZIYARETKODU = gerceklesliste[i].KAYITKODU,
                        GERCEKLESTIREN = kullanici
                    });
                }
            }
           sorusayi();

            ZiyaretListesi.ItemsSource = model1;

        }

        private void sorusayi()
        {


           
            var db = new SQLiteConnection(dbpath);
            var konularlist = db.Table<ISGB_ZIYARETLER_KONULAR>().ToList();
            for (int j = 0; j < model1.Count; j++)
            {
                int sorusayi = 0;
                for (int k = 0; k < konularlist.Count; k++)
                {
                    if (model1[j].ZIYARETKODU == konularlist[k].ZIYARETKODU)
                        sorusayi++;
                }
                model1[j].SORUSAYISI = sorusayi.ToString();
            }
        }

        private async void SecilenZiyaret(object sender, ItemTappedEventArgs e)
        {
            Ziyaretler z = new Ziyaretler();
            z = (Ziyaretler)e.Item;
            secilenziyaretkod = z.ZIYARETKODU;
            secilenziyaretplankod = z.PLANKODU;
            await Navigation.PushModalAsync(new ZiyaretIcerikMaster());
        }
    }
}