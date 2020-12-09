using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnasayfaMaster : MasterDetailPage
    {
        public AnasayfaMaster()
        {
            InitializeComponent();
           
            this.Master = new Master(); //Sol Menü Kullanılcağını Tanımlıyoruz

            this.Detail = new NavigationPage(new AnasayfaDetay()); //Anasayfanın İçeriğinin Bulunduğu Bölümü Tanımlıyoruz
      
        }
    }
}