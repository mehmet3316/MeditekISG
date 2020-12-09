using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ZiyaretKonuEkle : ContentPage
    {
        public ZiyaretKonuEkle()
        {
            InitializeComponent();
            lblTehlike.Text = "Uygun Değil";
        }

        private async void TehlikeDurumBtn(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Tehlike Durumu ", "İptal", "", "Uygun", "Uygun Değil");
            Debug.WriteLine("Action: " + action);
            if (action == "Uygun Değil") lblTehlike.Text = "Uygun Değil";
           else if (action == "Uygun") lblTehlike.Text = "Uygun";
        }

        private async void OlasılıkSec(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Gerçekleşme Olasılığı", "İptal", "", "Çok Düşük", "Düşük","Orta","Yüksek","Çok Yüksek");
            Debug.WriteLine("Action: " + action);
            if (action == "Çok Düşük") lblolasilik.Text = "Çok Düşük";
            else if (action == "Düşük") lblolasilik.Text = "Düşük";
            else if (action == "Orta") lblolasilik.Text = "Orta";
            else if (action == "Yüksek") lblolasilik.Text = "Yüksek";
            else if (action == "Çok Yüksek") lblolasilik.Text = "Çok Yüksek";
        }

        private async void SiddetSec(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Gerçekleşme Şiddeti", "İptal", "", "Çok Hafif", "Hafif", "Orta", "Ciddi", "Çok Ciddi");
            Debug.WriteLine("Action: " + action);
            if (action == "Çok Hafif") lblsiddet.Text = "Çok Hafif";
            else if (action == "Hafif") lblsiddet.Text = "Hafif";
            else if (action == "Orta") lblsiddet.Text = "Orta";
            else if (action == "Ciddi") lblsiddet.Text = "Ciddi";
            else if (action == "Çok Ciddi") lblsiddet.Text = "Çok Ciddi";


        }
        private async void EtkilencekSec(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Etkilencek Kişiler", "İptal", "", "Tüm Çalışanlar", "Bölüm Çalışanları", "Ziyaretçiler", "Çevre");
            Debug.WriteLine("Action: " + action);
            if (action == "Tüm Çalışanlar") tbEtkilencekKisiler.Text = tbEtkilencekKisiler.Text +" "+"Çok Hafif";
            else if (action == "Bölüm Çalışanları") tbEtkilencekKisiler.Text = tbEtkilencekKisiler.Text+" "+"Bölüm Çalışanları";
            else if (action == "Ziyaretçiler") tbEtkilencekKisiler.Text = tbEtkilencekKisiler.Text + " " + "Ziyaretçiler";
            else if (action == "Çevre") tbEtkilencekKisiler.Text = tbEtkilencekKisiler.Text + " " + "Çevre";


        }
    }
}