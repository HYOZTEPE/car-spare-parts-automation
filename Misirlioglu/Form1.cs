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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string kadi = textBox1.Text;
            string sifre = textBox2.Text;

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=hasanyigit61;Database=moyp";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
               
                string sorgu = "SELECT COUNT(*) FROM kullanici WHERE kadi = @p1 AND sifre = @p2";

                using (NpgsqlCommand command = new NpgsqlCommand(sorgu, connection))
                {
                    command.Parameters.AddWithValue("@p1", kadi);
                    command.Parameters.AddWithValue("@p2", sifre);

                    try
                    {
                        
                        connection.Open();

                        
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        
                        if (count > 0)
                        {
                            Form2 form2 = new Form2();
                            form2.StartPosition = FormStartPosition.Manual;
                            form2.Location = this.Location;
                            this.Hide();
                            form2.Show();
                        }
                        else
                        {
                            MessageBox.Show("KULLANICI ADI VEYA ŞİFRE HATALI!!!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("BİR HATA OLUŞTU : " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

    }

}
