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
    public partial class Form5 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=moyp; user ID=postgres; password=hasanyigit61;");
        public Form5()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form5_FormClosing);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Kategori İşlemleri";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
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

        public void Listele()
        {
            string sorgu = "select * from kategoriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void Kekle()
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategoriler (kategoriad) values (@p1)", baglanti);
            komut1.Parameters.AddWithValue("@p1", textBox1.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KATEGORİ BAŞARILI BİR ŞEKİLDE EKLENDİ.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Ksil()
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("Delete from kategoriler where kategoriid=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", int.Parse(textBox3.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KATEGORİ BAŞARILI BİR ŞEKİLDE SİLİNDİ.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void Kgüncelleme()
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("update kategoriler set kategoriad=@p1 where kategoriid=@p2", baglanti);
            komut4.Parameters.AddWithValue("@p1", textBox1.Text);
            komut4.Parameters.AddWithValue("@p2", int.Parse(textBox3.Text));
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KATEGORİ GÜNCELLEME İŞLEMİ BAŞARILI.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Kara()
        {
            baglanti.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("SELECT * FROM kategoriler WHERE kategoriad ILIKE '%' || @p1 || '%'", baglanti);
            komut5.Parameters.AddWithValue("@p1", textBox1.Text);
            NpgsqlDataAdapter da2 = new NpgsqlDataAdapter(komut5);
            DataTable dt = new DataTable();
            da2.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            Kekle();
            Listele();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            Ksil();
            Listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            Kgüncelleme();
            Listele();
        }

        private void btnara_Click(object sender, EventArgs e)
        {
            Kara();
        }
    }
}
