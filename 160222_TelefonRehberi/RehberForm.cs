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
using System.Configuration;

namespace _160222_TelefonRehberi
{
    public partial class RehberForm : Form
    {
        public RehberForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ConnectionString);
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("RehberEkle",baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@Isim", txt_isim.Text);
            komut.Parameters.AddWithValue("@Soyisim", txt_soyisim.Text);
            komut.Parameters.AddWithValue("@Telefon1", txt_tel1.Text);
            komut.Parameters.AddWithValue("@Telefon2", txt_tel2.Text);
            komut.Parameters.AddWithValue("@Email", txt_email.Text);
            komut.Parameters.AddWithValue("@Webadres", txt_webadres.Text);
            komut.Parameters.AddWithValue("@Adres", txt_adres.Text);
            komut.Parameters.AddWithValue("@Aciklama", txt_aciklama.Text);
            baglanti.Open();
            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();
            if (sonuc > 0)
            {
                MessageBox.Show("Kayıt başarılı bir şekilde eklendi."); 
                RehberListele();
            }
            else
                MessageBox.Show("Kayıt eklenirken hata oluştu.");




        }

        private void RehberForm_Load(object sender, EventArgs e)
        {
            RehberListele();

        }

        private void RehberListele()
        {
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "Select*from Rehber ";
            komut.Connection = baglanti;
            baglanti.Open();
            SqlDataReader rdr = komut.ExecuteReader();
            List<Rehber> rehberlistesi = new List<Rehber>();

            while (rdr.Read())
            {
                rehberlistesi.Add(new Rehber()
                {
                    ID = rdr.GetInt32(0),
                    Isim = rdr.IsDBNull(1)?string.Empty:rdr.GetString(1),
                    Soyisim = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2),
                    Telefon1 = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3),
                    Telefon2 = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4),
                    Email = rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5),
                    Webadres = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6),
                    Adres = rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7),
                    Aciklama = rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8)

                });

            }
            baglanti.Close();
            lst_Rehber.DataSource = rehberlistesi;
        }

        private void lst_Rehber_Click(object sender, EventArgs e)
        {
            Rehber r = (Rehber)lst_Rehber.SelectedItem;
            txt_secisim.Text = r.Isim;
            txt_secsoyisim.Text = r.Soyisim;
            txt_sectel1.Text = r.Telefon1;
            txt_sectel2.Text = r.Telefon2;
            txt_secemail.Text = r.Email;
            txt_secadres.Text = r.Adres;
            txt_secwebadres.Text = r.Webadres;
            txt_secaciklama.Text = r.Aciklama;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
           
            int sonuc=0;
            try
            {
                int id = ((Rehber)lst_Rehber.SelectedItem).ID;
                SqlCommand komut = new SqlCommand("RehberGuncelle", baglanti);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("@ID", id);
                komut.Parameters.AddWithValue("@Isim", txt_secisim.Text);
                komut.Parameters.AddWithValue("@Soyisim", txt_secsoyisim.Text);
                komut.Parameters.AddWithValue("@Telefon1", txt_sectel1.Text);
                komut.Parameters.AddWithValue("@Telefon2", txt_sectel2.Text);
                komut.Parameters.AddWithValue("@Email", txt_secemail.Text);
                komut.Parameters.AddWithValue("@Webadres", txt_secwebadres.Text);
                komut.Parameters.AddWithValue("@Adres", txt_secadres.Text);
                komut.Parameters.AddWithValue("@Aciklama", txt_secaciklama.Text);
                baglanti.Open();
               sonuc=komut.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Hata:" + ex);
            }
            finally
            {
                baglanti.Close();
            }
            
            
            if (sonuc > 0)
            {   MessageBox.Show("Kayıt güncellendi.");
            RehberListele(); }
             else
                MessageBox.Show("Güncelleme işlemi başarısız.");


        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = ((Rehber)lst_Rehber.SelectedItem).ID;
            SqlCommand komut = new SqlCommand("RehberSil", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@ID", id);
            baglanti.Open();
            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();
            if (sonuc > 0)
            {
                MessageBox.Show("Kayıt silindi.");
                txt_secisim.Text = " ";
                txt_secsoyisim.Text = " ";
                txt_secadres.Text = " ";
                txt_sectel1.Text = " ";
                txt_sectel2.Text = " ";
                txt_secwebadres.Text = " ";
                txt_secemail.Text = " ";
                txt_secaciklama.Text = " ";

                txt_isim.Text = " ";
                txt_soyisim.Text = " ";
                txt_adres.Text = " ";
                txt_tel1.Text = " ";
                txt_tel2.Text = " ";
                txt_webadres.Text = " ";
                txt_email.Text = " ";
                txt_aciklama.Text = " ";

                RehberListele();
            }
            else
                MessageBox.Show("Silme işlemi başarısızdır.");


        }
    }
}
