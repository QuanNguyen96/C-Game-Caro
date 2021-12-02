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

namespace game_caro
{
    public partial class frmLogin : Form
    {
        string str_connect = @"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True";
        SqlConnection con;
        SqlCommand command;
        public frmLogin()
        {
            InitializeComponent();
        }
        private void thuchienham_login()
        {
            con = new SqlConnection(str_connect);
            con.Open();
            string kiemtradangnhap = "select Count(*) from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where username=@UserName";
            command = new SqlCommand(kiemtradangnhap, con);
            command.Parameters.Add("@UserName", txt_username.Text);
            int x = (int)command.ExecuteScalar();
            if (x == 1)
            {
                btn_login.Enabled = false;
                timer1.Start();
                timer2.Start();
                
            }
            else
            {
                lb_login.ForeColor = Color.Red;
                lb_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
                lb_login.Text = "bạn đã nhập sai tên đăng nhập hoặc mật khẩu,xin vui lòng kiểm tra lại!";
            }
            con.Close();
        }
        private void getvalue(string name)
        {
            txt_username.Text = name;
        }
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                thuchienham_login();
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            pictureBox6.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hn3.png");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hn2.png");
            pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hn1.png");
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\Resources\\ic36.gif");
            pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Resources\\ic38.gif");
            progressBar1.Step = 21;
        }

        //private void cb_showpassword_CheckedChanged_1(object sender, EventArgs e)
        //{
        //    txt_password.PasswordChar = txt_password.PasswordChar == '*' ? '\0' : '*';
        //}

        private void btn_login_Click_1(object sender, EventArgs e)
        {
            thuchienham_login();
        }
        private int y;
        private void timer1_Tick(object sender, EventArgs e)
        {
            y+=3;
            progressBar1.PerformStep();
            label2.Text = string.Concat(y , "%");
        }
        private int x=0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox3.Location = new Point(pictureBox3.Location.X + 6, pictureBox3.Location.Y);
            x++;
            if (x == 110)
            {
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.username = txt_username.Text;
                frm1.ShowDialog();
                lb_login.Text = "";
                timer1.Stop();
                timer2.Stop();
            }
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_creat_new_accounts frm_newaccount = new frm_creat_new_accounts();
            frm_newaccount.mydata += new frm_creat_new_accounts.getdata(getvalue);
            frm_newaccount.ShowDialog();
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frm_laymatkhau frm = new frm_laymatkhau();
            frm.Show();
        }

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
