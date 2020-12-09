using MeditekISG.Model;
using MeditekISG.Model.Veritabanları;
using MeditekISG.View;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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

namespace MeditekISG.Popuplar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProgresBarPop : PopupPage
    {
        int guncelle = 0;
        float maxvalue = 1;
        float progressmax = 10;
        bool istimerRunning = true;
        float progress = 0;
        int counter = 1;
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public ProgresBarPop()
        {
            InitializeComponent();
          
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (progress >= 1)
                {
                    istimerRunning = false;
                    Navigation.PopPopupAsync();
                    Yenile();
                }
                else
                {
                    if (guncelle == 0)
                    {
                        IsyeriGuncelleme();
                        CalisanListGuncelleme();
                        GuncellemeAl();
                        GuncellenmeTarih();
                        guncelle = 1;
                    }
                    progress += maxvalue / progressmax;               
                    progresbar.ProgressTo(progress, 500, Easing.Linear);
                    lblprogress.Text = $"{counter}/{progressmax}";
                    counter += 1;
                }
                return istimerRunning;
            });

        }

        private async void Yenile()
        {
            await Navigation.PushModalAsync(new AnasayfaMaster());
        }
        private void GuncellenmeTarih()
        {
            DateTime now = DateTime.Now;

            var db = new SQLiteConnection(dbpath);
            var model = db.Table<GuncellemeDurum>().ToList();
            db.Table<GuncellemeDurum>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);          
            var idkontrol = db.Table<GuncellemeDurum>().OrderByDescending(a => a.ID).FirstOrDefault();
            GuncellemeDurum guncellemedurumu = new GuncellemeDurum()
            {
            ID = (idkontrol == null ? 1 : idkontrol.ID + 1),
            SONGUNCELLEMETARIHI=now.ToString(),
            SUNUCUID=SunucuSecim.secilensunucuid
            };
            db.Insert(guncellemedurumu);
        }
        private void IsyeriGuncelleme()
        {

            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
            var db = new SQLiteConnection(dbpath);
            var model = db.Table<ISGB_ISYERILISTESI>().ToList();
            db.Table<ISGB_ISYERILISTESI>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);

            try
            {
                baglan.Open();
                SqlCommand guncelleisyer = new SqlCommand("SELECT * FROM ISGB_ISYERILISTESI WHERE AKTIF=" + 1 + "", baglan);
                SqlDataReader gnclisyer = guncelleisyer.ExecuteReader();
                while (gnclisyer.Read())
                {
                    ISGB_ISYERILISTESI isyerilist = new ISGB_ISYERILISTESI()
                        {
                            ISYERIKODU = gnclisyer["ISYERIKODU"].ToString(),
                            ADRESI = gnclisyer["ADRESI"].ToString(),
                            AKTARIM = 1,
                            EPOSTA = gnclisyer["EPOSTA"].ToString(),
                            IGUZMANI = gnclisyer["IGUZMANI"].ToString(),
                            IL = gnclisyer["IL"].ToString(),
                            ILCE = gnclisyer["ILCE"].ToString(),
                            ISKOLU = gnclisyer["ISKOLU"].ToString(),
                            ISYERIHEKIMI = gnclisyer["ISYERIHEKIMI"].ToString(),
                            KISAADI = gnclisyer["KISAADI"].ToString(),
                            NACEKODU = gnclisyer["NACEKODU"].ToString(),
                            SUNUCUID = SunucuSecim.secilensunucuid,
                            TEHLIKESINIFI = int.Parse(gnclisyer["TEHLIKESINIFI"].ToString()),
                            TELEFON = gnclisyer["TELEFON"].ToString(),
                            UNVANI = gnclisyer["UNVANI"].ToString(),


                        };
                        db.Insert(isyerilist);
                   
               
                }
                baglan.Close();
            }
            catch(Exception ex)
            {

            }

        }
        private void CalisanListGuncelleme()
        {
           
            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
            var db1 = new SQLiteConnection(dbpath);
            var model1 = db1.Table<ISGB_CALISANLISTESI>().ToList();
            db1.Table<ISGB_CALISANLISTESI>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);
            try
            {
                baglan.Open();
                SqlCommand guncellecalisan = new SqlCommand("SELECT * FROM ISGB_CALISANLISTESI WHERE AKTIF=" + 1 + "", baglan);
                SqlDataReader gnclcalisan = guncellecalisan.ExecuteReader();
                while (gnclcalisan.Read())
                {
                 if(gnclcalisan["ISEGIRISTARIHI"].ToString()!=null && gnclcalisan["ISEGIRISTARIHI"].ToString()!=""&& gnclcalisan["TCKIMLIKNO"].ToString()!= null && gnclcalisan["TCKIMLIKNO"].ToString() != "")
                    {
                        ISGB_CALISANLISTESI calisanlist = new ISGB_CALISANLISTESI()
                        {
                             ID=gnclcalisan["ID"].ToString()+ gnclcalisan["TCKIMLIKNO"].ToString(),
                             TCKIMLIKNO = gnclcalisan["TCKIMLIKNO"].ToString(),
                            AKTARIM = 1,
                            CALISTIGIBOLUM = gnclcalisan["CALISTIGIBOLUM"].ToString(),
                            ISEGIRISTARIHI = gnclcalisan["ISEGIRISTARIHI"].ToString(),
                            ISYERIKODU = gnclcalisan["ISYERIKODU"].ToString(),
                            KISITURU = gnclcalisan["KISITURU"].ToString(),
                            SUNUCUID = SunucuSecim.secilensunucuid

                        };
                        db1.Insert(calisanlist);
                    }
                }
                baglan.Close();
            }
       catch(Exception ex2)
            {

            }

            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
            var db2 = new SQLiteConnection(dbpath);
            var model2 = db2.Table<KISILER>().ToList();
            db2.Table<KISILER>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);
            try
            {
                baglan.Open();
                SqlCommand guncellekisi = new SqlCommand("SELECT * FROM KISILER", baglan);
                SqlDataReader gnclkisi = guncellekisi.ExecuteReader();
                while (gnclkisi.Read())
                {
                    if(gnclkisi["TCKIMLIKNO"].ToString()!=null&& gnclkisi["TCKIMLIKNO"].ToString()!=""&& gnclkisi["CINSIYETI"].ToString() != null && gnclkisi["CINSIYETI"].ToString() != "")
                    {
                        KISILER kisilertablo = new KISILER()
                        {
                            ID = gnclkisi["ID"].ToString()+ gnclkisi["TCKIMLIKNO"].ToString(),
                            TCKIMLIKNO = gnclkisi["TCKIMLIKNO"].ToString(),
                            ADI = gnclkisi["ADI"].ToString(),
                            SOYADI = gnclkisi["SOYADI"].ToString(),
                            SUNUCUID = SunucuSecim.secilensunucuid,
                            AKTARIM = 1,
                            CINSIYETI = gnclkisi["CINSIYETI"].ToString(),
                            DOGUMTARIHI = gnclkisi["DOGUMTARIHI"].ToString(),
                            TELEFON = gnclkisi["TELEFON"].ToString(),
                        };
                        db2.Insert(kisilertablo);
                    }
                   
                }
                 baglan.Close();
            }
            catch(Exception ex3)
            {

            }



        }


        private void GuncellemeAl()
        {
            DateTime a = DateTime.Now;
            string b = a.ToString();
            string buyil = b.Substring(5, 4);

            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
            var db3 = new SQLiteConnection(dbpath);
            var model3 = db3.Table<ISGB_YILLIKPLAN>().ToList();
            db3.Table<ISGB_YILLIKPLAN>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);
            baglan.Close();
            try
            {
                baglan.Open();
                SqlCommand guncelplan = new SqlCommand("SELECT * FROM ISGB_YILLIKPLAN WHERE CALISMATURU=" + 98 + " AND CALISMAYILI=" + buyil + "", baglan);
                SqlDataReader gnclplan = guncelplan.ExecuteReader();
                while (gnclplan.Read())
                { string gerceklestiren = gnclplan["GERCEKLESMETARIHI"].ToString();
                    string tarihi= gnclplan["GERCEKLESTIREN"].ToString();
                    if (gerceklestiren.Length<5&& tarihi.Length<5)
                    {
                        ISGB_YILLIKPLAN yillikplanlist = new ISGB_YILLIKPLAN()
                        {
                            ISYERIKODU = gnclplan["ISYERIKODU"].ToString(),
                            PLANKODU = gnclplan["PLANKODU"].ToString(),
                            ILKTARIH = DateTime.Parse(gnclplan["ILKTARIH"].ToString()),
                            SUNUCUID = SunucuSecim.secilensunucuid
                        };
                        db3.Insert(yillikplanlist);
                    }
                    
                }
                baglan.Close();
            }
       catch(Exception ex4)
            {

            }



            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
            var db4 = new SQLiteConnection(dbpath);
            var model4 = db3.Table<ISGB_ZIYARETLER>().ToList();
            db4.Table<ISGB_ZIYARETLER>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);
            try
            {
                baglan.Open();
                SqlCommand guncelziyaret = new SqlCommand("SELECT * FROM ISGB_ZIYARETLER WHERE AKTIF=" + 1 + " AND CALISMAYILI=" + buyil + " ", baglan);
                SqlDataReader gnclziyaret = guncelziyaret.ExecuteReader();
                while (gnclziyaret.Read())
                {
                    ISGB_ZIYARETLER ziyaretlist = new ISGB_ZIYARETLER()
                    {
                        SUNUCUID = SunucuSecim.secilensunucuid,
                        CALISMAYILI = int.Parse(gnclziyaret["CALISMAYILI"].ToString()),
                        ISYERIKODU = gnclziyaret["ISYERIKODU"].ToString(),
                        KAYITKODU = gnclziyaret["KAYITKODU"].ToString(),
                        KULLANICI = gnclziyaret["KULLANICI"].ToString(),
                        NOTLAR = gnclziyaret["NOTLAR"].ToString(),
                        ZIYARETTARIHI = DateTime.Parse(gnclziyaret["ZIYARETTARIHI"].ToString()),
                    };
                    db4.Insert(ziyaretlist);
                }
                baglan.Close();
            }
            catch(Exception ex5)
            {

            }
    


            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
             var db5 = new SQLiteConnection(dbpath);
            var model5 = db5.Table<ISGB_ZIYARETLER_KONULAR>().ToList();
            db5.Table<ISGB_ZIYARETLER_KONULAR>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);
            try
            {
                baglan.Open();
                SqlCommand guncelziyaretkonu = new SqlCommand("SELECT * FROM ISGB_ZIYARETLER_KONULAR WHERE AKTIF=" + 1 + " ", baglan);
                SqlDataReader gnclziyaretkonu = guncelziyaretkonu.ExecuteReader();

                while (gnclziyaretkonu.Read())
                {
                    if (gnclziyaretkonu["KAYITTARIHI"].ToString().Substring(5, 4) == buyil)
                    {
                        ISGB_ZIYARETLER_KONULAR ziyaretkonulist = new ISGB_ZIYARETLER_KONULAR()
                        {
                            ID = gnclziyaretkonu["ID"].ToString()+gnclziyaretkonu["ZIYARETKODU"].ToString(),
                            AKTARIM = 1,
                            ETKILENECEKKISILER= gnclziyaretkonu["ETKILENECEKKISILER"].ToString(),
                            FAALIYET=gnclziyaretkonu["FAALIYET"].ToString(),
                            GERCEKLESMEOLASILIGI = int.Parse(gnclziyaretkonu["GERCEKLESMEOLASILIGI"].ToString()),
                            GERCEKLESMESIDDETI = int.Parse(gnclziyaretkonu["GERCEKLESMESIDDETI"].ToString()),
                            KONU = gnclziyaretkonu["KONU"].ToString(),
                            MEVCUTDURUM = gnclziyaretkonu["MEVCUTDURUM"].ToString(),
                            MEVZUAT = gnclziyaretkonu["MEVZUAT"].ToString(),
                            ONLEMLER = gnclziyaretkonu["ONLEMLER"].ToString(),
                            RISK = gnclziyaretkonu["RISK"].ToString(),
                            SKOR =int.Parse( gnclziyaretkonu["SKOR"].ToString()),
                            SUNUCUID = SunucuSecim.secilensunucuid,
                            SURECSORUMLUSU = gnclziyaretkonu["SURECSORUMLUSU"].ToString(),
                            TEHLIKE = gnclziyaretkonu["TEHLIKE"].ToString(),
                            TEHLIKEKAYNAGI = gnclziyaretkonu["TEHLIKEKAYNAGI"].ToString(),
                            UYGUNLUK = int.Parse(gnclziyaretkonu["UYGUNLUK"].ToString()),
                            ZIYARETKODU = gnclziyaretkonu["ZIYARETKODU"].ToString(),
                            KONUKODU= gnclziyaretkonu["KONUKODU"].ToString()
                        };
                        db5.Insert(ziyaretkonulist);

                    }

                }
                baglan.Close();
            }
            catch(Exception ex6)
            {

            }

            AnasayfaDetay.guncellemesor = 2;
         
        }

     /*   private void KapatBtn(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }*/

      
    }
}