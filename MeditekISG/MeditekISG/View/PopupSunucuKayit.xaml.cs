using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupSunucuKayit : PopupPage
    {
        public PopupSunucuKayit()
        {
            InitializeComponent();
        }

        [Obsolete]
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}