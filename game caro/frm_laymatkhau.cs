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
    public partial class frm_laymatkhau : Form
    {
        public frm_laymatkhau()
        {
            InitializeComponent();
        }
        string str_connect = @"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox2.TextLength >=3 )
            {
                try
                {
                    con = new SqlConnection(str_connect);
                    con.Open();
                    string timkiem = "select * from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where (username=N'" + textBox1.Text + "')";
                    cmd = new SqlCommand(timkiem, con);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        label4.Text = dr[0].ToString();
                        label6.Text = dr[1].ToString();
                    }
                    else
                    {
                        MessageBox.Show("không lấy lại được mật khẩu");
                    }
                    string matkhaunhap = textBox2.Text;
                    bool kt = sosanh(label6.Text, matkhaunhap);
                    con.Close();
                    if (kt == true)
                    {
                        label4.Text =label6.Text.Substring(0,label6.Text.Length-3)+"***";
                    }
                    else
                    {
                        label4.Text = "sai thông tin , xin vui lòng kiểm tra lại";

                    }
                }
                catch { }
            }
            
        }
        private bool sosanh(string s1,string s2)
        {
            return s1.Contains(s2);
        }

        private void frm_laymatkhau_Load(object sender, EventArgs e)
        {
            label6.Hide();
            label4.Text = "";
        }
    }
}
