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
    public partial class Form2 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=moyp; user ID=postgres; password=hasanyigit61;");
        public Form2()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            Point form1Location = this.Location;
            form2.StartPosition = FormStartPosition.Manual;
            form2.Location = form1Location;
            this.Hide();
            form2.Show();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            Point form1Location = this.Location;
            form4.StartPosition = FormStartPosition.Manual;
            form4.Location = form1Location;
            this.Hide();
            form4.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            Point form1Location = this.Location;
            form6.StartPosition = FormStartPosition.Manual;
            form6.Location = form1Location;
            this.Hide();
            form6.Show();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CenterPictureBox();
        }

        private void CenterPictureBox()
        {
            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int pictureBoxWidth = pictureBox8.Width;
            int pictureBoxHeight = pictureBox8.Height;
            int newX = (formWidth - pictureBoxWidth) / 2;
            int newY = (formHeight - pictureBoxHeight) / 2;
            pictureBox8.Location = new Point(newX, newY);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            Point form1Location = this.Location;
            form3.StartPosition = FormStartPosition.Manual;
            form3.Location = form1Location;
            this.Hide();
            form3.Show();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            Point form1Location = this.Location;
            form5.StartPosition = FormStartPosition.Manual;
            form5.Location = form1Location;
            this.Hide();
            form5.Show();

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            Point form1Location = this.Location;
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = form1Location;
            this.Hide();
            form1.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
