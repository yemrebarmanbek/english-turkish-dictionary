using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace İNGİLİZCE_TURKÇE_SOZLUK_UYGULAMASI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }
        //debug klasorü içindeki acces dosyasına erişmek için bu bağlantıyı yazdık.
        // OleDbConnection baglantim = new OleDbConnection("Provider=Microsoft.ACE.OleDb.12.0;DataSource = " + Application.StartupPath + "\\vt_sozluk.accdb");  bunun olmama sebebi false ifade yazmamamız
        OleDbConnection baglantim = new OleDbConnection("Provider = Microsoft.ACE.OleDb.12.0;Data Source=" + Application.StartupPath + "\\sozluk.accdb;Persist Security Info=False");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                baglantim.Open();                            //parantez içi alana veri gireceğimizi belirttik  ingilizce alanı textbox1.text türkçe alanı texbox2dir
                OleDbCommand eklekomutu = new OleDbCommand("insert into ingturkce (ingilizce,turkce) values('" + textBox1.Text + "','" + textBox2.Text + "')", baglantim);
                eklekomutu.ExecuteNonQuery();  //bu komut (eklekomutu sorgusunu) çalıştırır, ve sonucu veri tabanına işler (onemli komut)
                baglantim.Close();
                MessageBox.Show("sozluk veri tabanına eklendi", "veri tabanı işlemleri");
                textBox1.Clear();
                textBox2.Clear();

            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message, "veri tabanı işlemleri");
                baglantim.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbCommand guncelle_komutu = new OleDbCommand("update ingturkce set turkce='" + textBox2.Text + "'where ingilizce='" + textBox1.Text + "'", baglantim);
                guncelle_komutu.ExecuteNonQuery();
                baglantim.Close();
                MessageBox.Show("sözcük veri tabanı güncellendi.", "veri tabanı işlemleri");
                textBox1.Clear(); textBox2.Clear();
            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message, "veri tabanı işlemleri");
                baglantim.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglantim.Open();
                OleDbCommand sil_komutu = new OleDbCommand("delete from ingturkce where inglizce='" + textBox1.Text + "'", baglantim);
                sil_komutu.ExecuteNonQuery();
                baglantim.Close();
                MessageBox.Show("sozcuk veri tabanından silindi.", "veri tabanı işlemleri");
                textBox1.Clear(); textBox2.Clear();

            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message, "veri tabanı işlemleri");
                baglantim.Close();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                baglantim.Open();
                OleDbCommand arama_islemi = new OleDbCommand("select ingilizce,turkce from ingturkce where ingilizce like '" + textBox1.Text + "%'", baglantim);
                OleDbDataReader oku = arama_islemi.ExecuteReader();
                while (oku.Read())
                {
                    listBox1.Items.Add(oku["ingilizce"].ToString() + "=" + oku["turkce"].ToString());
                }
                baglantim.Close();
            }
            catch (Exception aciklama)
            {
                MessageBox.Show(aciklama.Message, "veri tabanı işlemleri");
                baglantim.Close();
            

            }
        }
    }
}
