using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _160222_TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection("server=localhost;database=TelefonRehberi;user=sa;password=1234");
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select count(*) from Kullanici where KullaniciAdi= @ad and Sifre=@sifre", baglanti);
            komut.Parameters.AddWithValue("@ad", txtKulAd.Text);
            komut.Parameters.AddWithValue("@sifre", txtSifre.Text);
            baglanti.Open();
            //eğer tek bir değer kullanılacaksa executescalar kullanılıyor.
           int sayi= (int)komut.ExecuteScalar();
            if (sayi > 0)
            {
                RehberForm r = new RehberForm();
                r.Show();
                MessageBox.Show("Kullanıcı geçerli");
            }
            else
                MessageBox.Show("Hatalı kullanıcı adı veya şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            baglanti.Close();
            
        }
    }
}
