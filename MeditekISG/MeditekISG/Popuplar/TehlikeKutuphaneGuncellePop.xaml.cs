using MeditekISG.Model.Veritabanları;
using MeditekISG.View;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class TehlikeKutuphaneGuncellePop : PopupPage
    {
        int guncelle = 0;
        float maxvalue = 1;
        float progressmax = 10;
        bool istimerRunning = true;
        float progress = 0;
        int counter = 1;
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public TehlikeKutuphaneGuncellePop()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                if (progress >= 1)
                {
                    istimerRunning = false;
                    Navigation.PopPopupAsync();
                }
                else
                {
                    if (guncelle == 0)
                    {
                      
                        GuncellemeAl();
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
        private void GuncellemeAl()
        {
            // AKTARIMLAR 0 OLANLAR AKTARILIP 1 OLACAK SONRASINDA VERİTABANI GÜNCELLEME GEÇİLCEK
            var db = new SQLiteConnection(dbpath);
            var model = db.Table<T_RISKKONULARI>().ToList();
            db.Table<T_RISKKONULARI>().Delete(x => x.SUNUCUID == SunucuSecim.secilensunucuid);

            try
            {
                baglan.Open();
                SqlCommand guncelle = new SqlCommand("SELECT * FROM T_RISKKONULARI WHERE AKTIF=" + 1 + "", baglan);
                SqlDataReader gncl = guncelle.ExecuteReader();
                while (gncl.Read())
                {

                    T_RISKKONULARI kutuphane = new T_RISKKONULARI()
                    {

                        ID = int.Parse(gncl["ID"].ToString()),
                        FAALIYET = gncl["FAALIYET"].ToString(),
                        KONU = gncl["KONU"].ToString(),
                        KONUKODU = gncl["KONUKODU"].ToString(),
                        MEVCUTDURUM = gncl["MEVCUTDURUM"].ToString(),
                        MEVZUAT = gncl["MEVZUAT"].ToString(),
                        OLASILIK = gncl["OLASILIKMATRIS"].ToString(),
                        ONERI = gncl["ONERI"].ToString(),
                        RISK = gncl["RISK"].ToString(),
                        SIDDET = gncl["SIDDETMATRIS"].ToString(),
                        TEHLIKE = gncl["TEHLIKE"].ToString(),
                        TEHLIKEKAYNAGI = gncl["TEHLIKEKAYNAGI"].ToString(),
                        SUNUCUID = SunucuSecim.secilensunucuid
                        
                    };
                    db.Insert(kutuphane);


                }
                baglan.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}