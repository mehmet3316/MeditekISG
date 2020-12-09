using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.Popuplar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnasafyaOflinePop : PopupPage
    {
        public AnasafyaOflinePop()
        {
            InitializeComponent();
        }

        private void PopKapa(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}