using System;
using Npgsql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Misirlioglu
{
    public partial class Form4 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=moyp; user ID=postgres; password=hasanyigit61;");
        public Form4()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Müşteri Bul";
            tabPage2.Text = "Parça Bul";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ara();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Point form1Location = this.Location;
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = form1Location;
            this.Hide();
            form2.Show();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            Point form1Location = this.Location;
            form3.StartPosition = FormStartPosition.Manual;
            form3.Location = form1Location;
            this.Hide();
            form3.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            Point form1Location = this.Location;
            form4.StartPosition = FormStartPosition.Manual;
            form4.Location = form1Location;
            this.Hide();
            form4.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            Point form1Location = this.Location;
            form5.StartPosition = FormStartPosition.Manual;
            form5.Location = form1Location;
            this.Hide();
            form5.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            Point form1Location = this.Location;
            form6.StartPosition = FormStartPosition.Manual;
            form6.Location = form1Location;
            this.Hide();
            form6.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Point form1Location = this.Location;
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = form1Location;
            this.Hide();
            form1.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        public void Listele()
        {
            string sorgu = "select * from musteri";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void Ara()
        {
            baglanti.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("SELECT * FROM musteri WHERE musteriad ILIKE '%' || @p1 || '%'", baglanti);
            komut5.Parameters.AddWithValue("@p1", textBox3.Text);
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(komut5);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        public void Sil()
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete from musteri where musteriid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(textBox4.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("MÜŞTERİ BAŞARILI BİR ŞEKİLDE SİLİNDİ.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Listele();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sil();
        }

        public void para()
        {
            baglanti.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("SELECT * FROM urunler WHERE urunad ILIKE '%' || @p1 || '%'", baglanti);
            komut5.Parameters.AddWithValue("@p1", textBox1.Text);
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(komut5);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        

        public void UListele()
        {
            string sorgu = "select urunid,urunad,stok,alisfiyat,satisfiyat,kategoriad from urunler inner join kategoriler\r\non\r\nurunler.kategori=kategoriler.kategoriid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UListele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            para();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UListele();
        }
    }
}
