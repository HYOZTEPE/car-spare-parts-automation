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
    public partial class Form6 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=moyp; user ID=postgres; password=hasanyigit61;");
        public Form6()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form6_FormClosing);
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Ürün İşlemleri";
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "kategoriad";
            comboBox1.ValueMember = "kategoriid";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

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
        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
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
            string sorgu = "select urunid,urunad,stok,alisfiyat,satisfiyat,kategoriad from urunler inner join kategoriler\r\non\r\nurunler.kategori=kategoriler.kategoriid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        public void Ekle()
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urunler (urunad,stok,alisfiyat,satisfiyat,kategori) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            
            komut.Parameters.AddWithValue("@p1", textBox1.Text);
            komut.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            komut.Parameters.AddWithValue("@p3", double.Parse(textBox2.Text));
            komut.Parameters.AddWithValue("@p4", double.Parse(textBox4.Text));
            komut.Parameters.AddWithValue("@p5", int.Parse(comboBox1.SelectedValue.ToString()));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("ÜRÜN KAYDI BAŞARILI BİR ŞEKİLDE GERÇEKLEŞTİ.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Sil()
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete from urunler where urunid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(textBox3.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("ÜRÜN KAYDI SİLME İŞLEMİ BAŞARILI.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void Güncelle()
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("update urunler set urunad=@p1, stok=@p2, alisfiyat=@p3, satisfiyat=@p4 where urunid=@p5", baglanti);
            komut4.Parameters.AddWithValue("@p1", textBox1.Text);
            komut4.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            komut4.Parameters.AddWithValue("@p3", double.Parse(textBox2.Text));
            komut4.Parameters.AddWithValue("@p4", double.Parse(textBox4.Text));
            komut4.Parameters.AddWithValue("@p5", int.Parse(textBox3.Text));
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("ÜRÜN GÜNCELLEME İŞLEMİ BAŞARILI.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Ara()
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

        private void button4_Click(object sender, EventArgs e)
        {
            Ekle();
            Listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Güncelle();
            Listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sil();
            Listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ara();
        }
    }
}
