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
    public partial class CalisanListMaster : MasterDetailPage
    {
        public CalisanListMaster()
        {
            InitializeComponent();
            this.Master = new Master();
            this.Detail = new NavigationPage(new CalisanListDetay());
        }
    }
}