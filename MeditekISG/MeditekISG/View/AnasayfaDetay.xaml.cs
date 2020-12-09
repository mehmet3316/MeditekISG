using MeditekISG.Model;
using MeditekISG.Model.Veritabanları;
using MeditekISG.Popuplar;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnasayfaDetay : ContentPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");   
        public static int guncellemesor = 0;
        public AnasayfaDetay()
        {
            InitializeComponent();

            TehlikeKonuları();
            guncellemealani.IsVisible = false;
            if (SunucuSecim.onlineofline == 1&& guncellemesor==0)
                GuncellemeOnay();
            if (guncellemesor == 1)
            {
                guncellemealani.IsVisible = true;
                var db = new SQLiteConnection(dbpath);
                var liste = db.Table<GuncellemeDurum>().ToList();
                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].SUNUCUID == SunucuSecim.secilensunucuid)
                        lblguncellemedurumtar.Text= " Son Güncelleme : " + liste[i].SONGUNCELLEMETARIHI; 
                }     
                lblguncellemedurum.Text = "Kayıtlarınız Güncel Değil!";
                lblguncellemedurum.TextColor = Color.Red;
                btnguncelleme.IsVisible = true;
                btnguncelleme.Text = "Guncelle";
                btnguncelleme.BackgroundColor = Color.Red;
            }
            else
            {
                var db = new SQLiteConnection(dbpath);
                var liste = db.Table<GuncellemeDurum>().ToList();
                for (int i = 0; i < liste.Count; i++)
                {
                    if (liste[i].SUNUCUID ==SunucuSecim.secilensunucuid)
                        lblguncellemedurum.Text = "Son Güncellenme Tarihi =>"+ liste[i].SONGUNCELLEMETARIHI;
                }
                lblguncellemedurumtar.IsVisible = false;
                btnguncelleme.IsVisible = false;
                guncellemealani.IsVisible = true;               
                lblguncellemedurum.TextColor = Color.Green;
            }
            if (IsyeriListDetay.secilenisyeri==null)
            {
                lblisyerisecim.Text = "Lütfen İşyeri Seçiniz";
            }
            else
            {
                SecilenIsyeri();
            }
        }

        private void TehlikeKonuları()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<T_RISKKONULARI>().ToList();
            lblkutuphanesayı.Text = liste.Count.ToString()+" "+"Konu Mevcut" ;
            if(SunucuSecim.onlineofline== 2)
            {
                btnTehlikeGuncelle.IsVisible = false;
            }
        }
        private async  void GuncellemeOnay()
        {
            bool answer = await DisplayAlert("Güncelleme Alınsın Mı?", "Yakın Zamamnda Güncelleme Alındı İse Hayır Basınız", "Evet", "Hayır");
            Debug.WriteLine("Answer: " + answer);
            if (answer==true)
            {              
                ProgresGit();             
            }
            else
            {
                guncellemesor = 1;
                await Navigation.PushModalAsync(new AnasayfaMaster());
            }

        }
        private void ProgresGit()
        {
            Navigation.PushPopupAsync(new ProgresBarPop());
        }
        private void SecilenIsyeri()
        {
            var db = new SQLiteConnection(dbpath);
            var liste = db.Table<ISGB_ISYERILISTESI>().ToList();
            for(int i=0;i<liste.Count;i++)
            {
                if (IsyeriListDetay.secilenisyeri == liste[i].ISYERIKODU)
                    lblisyerisecim.Text = liste[i].KISAADI;
            }

        }

        private async void IsyeriList_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new IsyeriListeMaster());

        }

        private async void Güncelle(object sender, EventArgs e)
        {
            if(guncellemesor!=2)
            GuncellemeOnay();
            else await Navigation.PushModalAsync(new AnasayfaMaster());
        }

        private async void GüncelleTehlike(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Kütüphane Güncellensin mi?", null, "Evet", "Hayır");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true)
            {
                GuncelleYonlendir();
            }
           
        }
        private void GuncelleYonlendir()
        {
            Navigation.PushPopupAsync(new TehlikeKutuphaneGuncellePop());
        }
    }
}