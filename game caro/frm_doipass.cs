using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    public partial class frm_doipass : Form
    {
        string chuoiketnoi = @"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True";
        SqlConnection con;
        SqlCommand command;
        public frm_doipass()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtmkcu.Text.Length >= 8)
            {
                if (string.Compare(txtmkcu.Text, txtmkmoi.Text, true) == 0)
                {
                    btn_luu.Enabled = true;
                    lb_ktramk.Text = "";
                }
                else
                {
                    btn_luu.Enabled = false;
                    lb_ktramk.ForeColor = Color.MediumBlue;
                    lb_ktramk.Text = "không khớp";
                }
            }
        }
        public string tentaikhoan;
        private void btn_luu_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(chuoiketnoi);
                con.Open();
                string doipass = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET password=@PASSWORD WHERE username=@USERNAME";
                command = new SqlCommand(doipass, con);
                command.Parameters.AddWithValue("@USERNAME", lb_tentaikhoan.Text);
                command.Parameters.AddWithValue("@PASSWORD", txtmkcu.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("Đổi mật khẩu thành công !");
                this.Close();
            }
            catch { MessageBox.Show("Đổi mật khẩu thất bại !"); }
        }

        private void frm_doipass_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hnn2.png");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hnn3.png");
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hnn4.png");
            pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hnn5.png");
            pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hnn1.png");
            pictureBox6.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hnn6.png");
            lb_tentaikhoan.Text = tentaikhoan;
            btn_luu.Enabled = false;
        }

        private void frm_doipass_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==Keys.Enter)
            {
                try
                {
                    con = new SqlConnection(chuoiketnoi);
                    con.Open();
                    string doipass = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET password=@PASSWORD WHERE username=@USERNAME";
                    command = new SqlCommand(doipass, con);
                    command.Parameters.AddWithValue("@USERNAME", lb_tentaikhoan.Text);
                    command.Parameters.AddWithValue("@PASSWORD", txtmkcu.Text);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Đổi mật khẩu thành công !");
                    this.Close();
                }
                catch { MessageBox.Show("Đổi mật khẩu thất bại !"); }
            }
        }

        private void txt_makhaucu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(chuoiketnoi);
                con.Open();
                string timkiem = "select * from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where (username=N'" + tentaikhoan + "')";
                command = new SqlCommand(timkiem, con);
                command.ExecuteNonQuery();
                SqlDataReader dr = command.ExecuteReader();
                dr.Read();
                if (string.Compare(txt_makhaucu.Text,dr[1].ToString(),false)==0)
                {
                    txtmkcu.Enabled = true;
                    txtmkmoi.Enabled = true;
                    label8.Text = "";
                }
                else
                {
                    txtmkcu.Enabled = false;
                    txtmkmoi.Enabled = false;
                    label8.Text = "không khớp !";
                }
                con.Close();
            }
            catch { }
        }

        private void txtmkcu_TextChanged(object sender, EventArgs e)
        {
            if (txtmkcu.Text.Length < 8)
            {
                label9.Text = "mật khẩu quá ngắn !";
            }
            else
            {
                label9.Text = "";
                if (string.Compare(txtmkcu.Text, txtmkmoi.Text, false) == 0)
                {
                    lb_ktramk.Text = "";
                    btn_luu.Enabled = true;
                }
                else
                {
                    lb_ktramk.Text = "không khớp";
                    btn_luu.Enabled = false;
                }
            }
        }
    }
}
