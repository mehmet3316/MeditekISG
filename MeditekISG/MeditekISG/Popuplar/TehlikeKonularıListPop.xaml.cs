using MeditekISG.Model;
using MeditekISG.Model.Veritabanları;
using MeditekISG.View;
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
    public partial class TehlikeKonularıListPop : PopupPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        private readonly List<TehlikeKonulari> model = new List<TehlikeKonulari>();
        public TehlikeKonularıListPop()
        {
            InitializeComponent();
            TehlikeKonulariListele();
        }
        private void TehlikeKonulariListele()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<T_RISKKONULARI>().ToList();

        for(int i=0;i<liste.Count;i++)
            {
                model.Add(new TehlikeKonulari
                {
                    SIRA = i + 1,
                    KONU = liste[i].KONU,
                    KONUKODU = liste[i].KONUKODU

                }); 
            }
               

            ZiyaretKonulariList.ItemsSource = model;
        }

        private void Kapat(object sender, EventArgs e)
        {

            PopupNavigation.Instance.PopAsync(true);
        }

        private void KONUARAMA_TextChanged(object sender, TextChangedEventArgs e)
        { //Search texboxa yazılan kelimeyi anlık alır textchange ile
            var kelime = KONUARAMA.Text;
            //Aranan kelime eşleştirmeyi işyeri uzun adı ile yapmaktadır
            ZiyaretKonulariList.ItemsSource = model.Where(x => x.KONU.ToLower().Contains(kelime.ToLower()));

        }
    }
}