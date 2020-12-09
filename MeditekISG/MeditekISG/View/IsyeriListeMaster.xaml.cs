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
    public partial class IsyeriListeMaster : MasterDetailPage
    {
        public IsyeriListeMaster()
        {
            InitializeComponent();
            this.Master = new Master(); //Sol menü tanımlama
            this.Detail = new NavigationPage(new IsyeriListDetay());//İşyeri Liste Tasarım İçerikleri Olan Ekranı Atama
        }
    }
}