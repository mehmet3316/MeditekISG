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
    public partial class IsyeriBilgMaster : MasterDetailPage
    {
        public IsyeriBilgMaster()
        {
            InitializeComponent();
            this.Master = new Master(); //Sol menü tanımlama
            this.Detail = new NavigationPage(new IsyeriBilgTabbed()); //Detay ekranı tablardan oluşmakda IsyeriBilgTablar Klasörü         
        
        }
    }
}