using MeditekISG.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data.SqlClient;
using MeditekISG.Model.Veritabanları;

namespace MeditekISG.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Giris : ContentPage
    {
        public static string kullaniciid;
        string  sifrekontr = "";
        string dbpath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MEDITEKISGLITE.db");
        
        public Giris()
        {
            InitializeComponent();
            SecilenSunucu();
            if (SunucuSecim.onlineofline == 0)
            {
                SunucuAdıBtn.BackgroundColor = Color.Green;
                GirisBtn.IsVisible = false;
                SunucuAdıBtn.TextColor = Color.White;
                SunucuAdıBtn.Text ="Lütfen Sunucu Seçiniz";
                tbKullanici.IsEnabled = false;
                tbParola.IsEnabled = false;
            }
            
        }
        private void SecilenSunucu()
        {
           
            var db = new SQLiteConnection(dbpath);
            var model = db.Table<Sunucular>().ToList();
            for (int i = 0; i < model.Count; i++)
            {
                if (model[i].ID == SunucuSecim.secilensunucuid)
                {
                    tbKullanici.IsEnabled = true;
                    tbParola.IsEnabled = true;
                    SunucuAdıBtn.Text = model[i].TANIM;

                    if (SunucuSecim.onlineofline == 1)
                    {
                        lblonlineofline.Text = "ONLİNE";
                        lblonlineofline.TextColor = Color.Green;
                    }
                    else
                    {
                        lblonlineofline.Text = "OFLİNE";
                        lblonlineofline.TextColor = Color.Red;
                    }
                      
                }
               
            }
        }
        private async void Giris_Clicked(object sender, EventArgs e)
        {

            SifreleMd5(tbKullanici.Text + tbParola.Text);
            string sifre = sifrekontr;
            if (SunucuSecim.onlineofline == 0)
            {
                await Navigation.PushAsync(new SunucuSecim());
            }
           else if (SunucuSecim.onlineofline == 2)
            {
                int giris = 0;
                List<T_PERSONEL> model = new List<T_PERSONEL>();

                var db = new SQLiteConnection(dbpath);
                model = db.Table<T_PERSONEL>().ToList();
                for(int l=0;l<model.Count;l++)
                {
                    if(model[l].TCKIMLIKNO==tbKullanici.Text&&model[l].SIFRE== sifre&&model[l].SUNUCUID == SunucuSecim.secilensunucuid)
                    {
                        if(model[l].DURUM==1)
                        {
                            kullaniciid = model[l].ID.ToString();
                               giris = 1;
                          await Navigation.PushModalAsync(new AnasayfaMaster());
                        }
                       
                        else
                            await DisplayAlert("Hata", "Yöneticiniz İle Görüşünüz!", "Tamam");
                    }                  
                        
                }
               if(giris==0)
                {
                    await DisplayAlert("Hata", "Kullanıcı Adı Veya Parola Hatalı", "Tamam");

                }

            }
           else if (SunucuSecim.onlineofline==1)
            {
                SqlConnection baglan = new SqlConnection(SunucuSecim.sqlbaglanti);
                var db = new SQLiteConnection(dbpath);
                var model = db.Table<T_PERSONEL>().ToList();
                db.Table<T_PERSONEL>().Delete(x=> x.SUNUCUID==SunucuSecim.secilensunucuid);
                try
                {
                    baglan.Open();
                    SqlCommand guncelle = new SqlCommand("SELECT * FROM T_PERSONEL WHERE not (DURUM ="+0+")", baglan);
                    SqlDataReader gncl = guncelle.ExecuteReader();
                    while (gncl.Read())
                    {

                        T_PERSONEL personel = new T_PERSONEL()
                        {
                            ID = gncl["ID"].ToString() + gncl["TCKIMLIKNO"].ToString(),
                            TCKIMLIKNO = gncl["TCKIMLIKNO"].ToString(),
                            ADI = gncl["ADI"].ToString(),
                            CEPTELELFONU = gncl["CEPTELEFONU"].ToString(),
                            DURUM = int.Parse(gncl["DURUM"].ToString()),
                            EPOSTA = gncl["EPOSTA"].ToString(),
                            GOREVI = int.Parse(gncl["GOREVI"].ToString()),
                            SIFRE = gncl["SIFRE"].ToString(),
                            SOYADI = gncl["SOYADI"].ToString(),
                            UNVANI = int.Parse(gncl["UNVANI"].ToString()),
                            SUNUCUID = SunucuSecim.secilensunucuid
                        };
                        db.Insert(personel);
                    }

                    baglan.Close();

                }
                catch(Exception ex10)
                {

                }
               
                if (sifrekontr != null && sifrekontr != "")
                {
                    baglan.Close();
                    try
                    {
                        baglan.Open();
                        SqlCommand con = new SqlCommand("SELECT * FROM T_PERSONEL WHERE TCKIMLIKNO='" + tbKullanici.Text + "' AND SIFRE='" + sifre + "'", baglan);
                        SqlDataReader dr = con.ExecuteReader();
                        if (dr.Read())
                        {
                            int durum;
                            kullaniciid = dr["ID"].ToString()+ dr["TCKIMLIKNO"].ToString();
                            durum = int.Parse(dr["DURUM"].ToString());
                            if (durum == 1)
                                await Navigation.PushModalAsync(new AnasayfaMaster());
                            else
                                await DisplayAlert("Hata", "Yöneticiniz İle Görüşünüz!", "Tamam");

                        }
                        else
                        {
                            await DisplayAlert("Hata", "Kullanıcı Adı veya Parola Hatalı", "Tamam");
                        }
                        baglan.Close();
                    }
                    catch
                    {
                        await Navigation.PushAsync(new SunucuSecim());
                    }
                }
                
            }






        }
        private void SifreleMd5(string parola)
        {
            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(parola);
            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToUpper());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            sifrekontr = sb.ToString();
        }

        private async void SunucuAdıBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SunucuSecim());
        }

        /*  byte[] data = UTF8Encoding.UTF8.GetBytes(parola);
          using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
          {
              byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
              using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
              {
                  ICryptoTransform transform = tripDes.CreateEncryptor();
                  byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                  sifrekontr = Convert.ToBase64String(results, 0, results.Length); 
              }  
          } */

    }
}