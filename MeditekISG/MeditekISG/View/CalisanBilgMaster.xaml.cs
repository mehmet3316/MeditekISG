using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalisanBilgMaster : MasterDetailPage
    {
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
        public CalisanBilgMaster()
        {
            InitializeComponent();
            this.Master = new Master();
            this.Detail = new NavigationPage(new CalisanBilgTabbed());
         
        }
        
    }
}