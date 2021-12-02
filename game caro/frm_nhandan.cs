using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    public partial class frm_nhandan : Form
    {
        public frm_nhandan()
        {
            InitializeComponent();
        }
        private void frm_nhandan_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho1.png");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho2.png");
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho3.png");
            pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho4.png");
            pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho5.png");
            pictureBox6.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho6.png");
            pictureBox7.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho7.png");
            pictureBox8.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho8.png");
            pictureBox9.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho9.png");
            pictureBox10.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho10.png");
            pictureBox11.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho11.png");
            pictureBox12.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho12.png");
            pictureBox13.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho13.png");
            pictureBox14.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho14.png");
            pictureBox15.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho15.png");
            pictureBox16.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho16.png");
            pictureBox17.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho17.png");
            pictureBox18.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho18.png");
            pictureBox19.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho19.png");
            pictureBox20.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho20.png");
            pictureBox21.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho21.png");
            pictureBox22.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho22.png");
            pictureBox23.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho23.png");
            pictureBox24.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho24.png");
            pictureBox25.Image = Image.FromFile(Application.StartupPath + "\\Resources\\nhandan\\cho25.png");
        }
        public delegate void getdata(string image_filename);
        public getdata mydata;
        private string filename = "";
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho1.png";
            mydata(filename);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho2.png";
            mydata(filename);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho3.png";
            mydata(filename);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho4.png";
            mydata(filename);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho5.png";
            mydata(filename);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho6.png";
            mydata(filename);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho7.png";
            mydata(filename);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho8.png";
            mydata(filename);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho9.png";
            mydata(filename);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho10.png";
            mydata(filename);
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho11.png";
            mydata(filename);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho12.png";
            mydata(filename);
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho13.png";
            mydata(filename);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho14.png";
            mydata(filename);
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho15.png";
            mydata(filename);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho16.png";
            mydata(filename);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho17.png";
            mydata(filename);
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho18.png";
            mydata(filename);
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho19.png";
            mydata(filename);
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho20.png";
            mydata(filename);
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho21.png";
            mydata(filename);
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho22.png";
            mydata(filename);
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho23.png";
            mydata(filename);
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho24.png";
            mydata(filename);
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            filename = "\\Resources\\nhandan\\cho25.png";
            mydata(filename);
        }
    }
}
