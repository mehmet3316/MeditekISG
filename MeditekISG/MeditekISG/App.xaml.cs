using MeditekISG.Model;
using MeditekISG.Model.Veritabanları;
using MeditekISG.View;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG
{
    public partial class App : Application
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        public App()
        {
            InitializeComponent();
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<Sunucular>();
            db.CreateTable<ISGB_CALISANLISTESI>();
            db.CreateTable<ISGB_ISYERILISTESI>();
            db.CreateTable<ISGB_YILLIKPLAN>();
            db.CreateTable<ISGB_ZIYARETLER>();
            db.CreateTable<ISGB_ZIYARETLER_KONULAR>();
            db.CreateTable<KISILER>();
            db.CreateTable<T_PERSONEL>();
            db.CreateTable<GuncellemeDurum>();
            db.CreateTable<T_RISKKONULARI>();
            MainPage = new NavigationPage(new Giris());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
