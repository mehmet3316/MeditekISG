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
    public partial class OnlineOfline : ContentPage
    {
        public static int onlineofline;
        public OnlineOfline()
        {
            InitializeComponent();
            
        }

        private async void Online_Clicked(object sender, EventArgs e)
        {
            onlineofline = 1;

            await Navigation.PushAsync(new SunucuSecim());
        }

        private async void Ofline_Clicked(object sender, EventArgs e)
        {
            onlineofline = 0;
            await Navigation.PushAsync(new Giris());
        }
    }
}