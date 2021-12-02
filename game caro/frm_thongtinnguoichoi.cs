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
using System.IO;

namespace game_caro
{
    public partial class frm_thongtinnguoichoi : Form
    {
        public frm_thongtinnguoichoi()
        {
            InitializeComponent();
        }
        public string name;
        SqlConnection con = new SqlConnection(@"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True");
        SqlCommand cmd;
        private void frm_thongtinnguoichoi_Load(object sender, EventArgs e)
        {
                con.Open();
                string timkiem = "select * from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where (username=N'" + name + "')";
                cmd = new SqlCommand(timkiem, con);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                label4.Text = dr[2].ToString();
                label6.Text = dr[3].ToString();
                if (dr.HasRows)
                {
                    byte[] image = (byte[])dr[4];
                    if (image == null)
                    {
                        avatar.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(image);
                        avatar.Image = Image.FromStream(ms);
                    }
                }
                label2.Text = name;
        }
    }
}
