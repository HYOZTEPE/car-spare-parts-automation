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
    public partial class Form3 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=moyp; user ID=postgres; password=hasanyigit61;");
        public Form3()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form3_FormClosing);
        }


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Satış İşlemleri";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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
            string sorgu = "select urunid,urunad,stok,alisfiyat,satisfiyat,kategoriad from urunler inner join kategoriler\r\non\r\nurunler.kategori=kategoriler.kategoriid";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                NpgsqlCommand komut = new NpgsqlCommand("INSERT INTO musteri (musteriad, telefon, adet, parca, fiyat, tarih) VALUES (@p1, @p2, @p3, @p4, @p5, @p6) RETURNING musteriid", baglanti);

                komut.Parameters.AddWithValue("@p1", textBox3.Text);
                komut.Parameters.AddWithValue("@p2", textBox1.Text);
                komut.Parameters.AddWithValue("@p3", int.Parse(numericUpDown2.Value.ToString()));
                komut.Parameters.AddWithValue("@p4", int.Parse(textBox2.Text));

                NpgsqlCommand stokFiyatKontrolKomut = new NpgsqlCommand("SELECT stok, satisfiyat FROM urunler WHERE urunid = @p4", baglanti);
                stokFiyatKontrolKomut.Parameters.AddWithValue("@p4", int.Parse(textBox2.Text));

                using (NpgsqlDataReader reader = stokFiyatKontrolKomut.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int stokMiktari = reader.GetInt32(0);
                        double satisfiyat = reader.GetDouble(1);

                        if (int.Parse(numericUpDown2.Value.ToString()) > stokMiktari)
                        {
                            MessageBox.Show("YETERSİZ STOK. SATIŞ İŞLEMİ GERÇEKLEŞMEDİ.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        reader.Close();

                        double toplamFiyat = satisfiyat * int.Parse(numericUpDown2.Value.ToString());

                        komut.Parameters.AddWithValue("@p5", toplamFiyat);
                        komut.Parameters.AddWithValue("@p6", DateTime.Now);

                        int musteriid = (int)komut.ExecuteScalar();
                        NpgsqlCommand stokGuncellemeKomut = new NpgsqlCommand("UPDATE urunler SET stok = stok - @p3 WHERE urunid = @p4", baglanti);

                        stokGuncellemeKomut.Parameters.AddWithValue("@p3", int.Parse(numericUpDown2.Value.ToString()));
                        stokGuncellemeKomut.Parameters.AddWithValue("@p4", int.Parse(textBox2.Text));
                        stokGuncellemeKomut.ExecuteNonQuery();

                        NpgsqlCommand fiyatGuncellemeKomut = new NpgsqlCommand("UPDATE musteri SET fiyat = @p5 WHERE musteriid = @p6", baglanti);

                        fiyatGuncellemeKomut.Parameters.AddWithValue("@p5", toplamFiyat);
                        fiyatGuncellemeKomut.Parameters.AddWithValue("@p6", musteriid);
                        fiyatGuncellemeKomut.ExecuteNonQuery();

                        label7.Text = "Toplam Fiyat: " + toplamFiyat.ToString("C");

                        MessageBox.Show("SATIŞ İŞLEMİ BAŞARILI BİR ŞEKİLDE GERÇEKLEŞTİ.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ÜRÜN BULUNAMADI.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BİR HATA OLUŞTU : " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
