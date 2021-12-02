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
    public partial class TOP_Player : Form
    {
        public TOP_Player()
        {
            InitializeComponent();
        }
        string chuoi_kn = @"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;

        private void TOP_Player_Load(object sender, EventArgs e)
        {
            updatedulieucot();
            sapxep(1000);
            label1.Text = "hiển thị xếp hạng ";
            loaddulieuvaotextbox();
        }
        private void updatedulieucot()
        {
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET diem=thang*thang/(thang+thua+1)";
            cmd = new SqlCommand(timkiem, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void sapxep(int a)
        {
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = " SELECT top("+a+") username,thang,thua,diem,ten FROM [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] order by diem desc";
            cmd = new SqlCommand(timkiem, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void hiểnThịTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txt_timkiem.Text = "";
                txt_xephang.Text = "";
                label1.Text = "hiển thị xếp hạng ";
                sapxep(1000);
                loaddulieuvaotextbox();
            }
            catch { }
        }

        private void TOP_Player_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                updatedulieucot();
            }
            catch { }
        }

        private void btn_xephang_Click(object sender, EventArgs e)
        {
            int a = int.Parse(txt_xephang.Text);
            sapxep(a);
            loaddulieuvaotextbox();
        }
        private void timkiem()
        {
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = " SELECT username,thang,thua,diem,ten FROM [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where username like N'%" + txt_timkiem.Text + "%'";
            cmd = new SqlCommand(timkiem, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_timkiem.Text != "")
            {
                timkiem();
                xephang();
            }
            else
            {
                sapxep(1000);
            }
            loaddulieuvaotextbox();
        }
        
        private void xephang()
        {
            //MessageBox.Show("" + txt_timkiem.Text);
            try
            {
                if (txt_timkiem.Text != "")
                {
                    con.Open();
                    string timkiem1 = "  SELECT *FROM( Select  [username],[thang],[thua],[diem],[ten],Row_Number() Over(Order by diem desc) STT From[Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player]  ) A WHERE username LIKE N'%" + txt_timkiem.Text + "%'";
                    cmd = new SqlCommand(timkiem1, con);
                    cmd.ExecuteNonQuery();
                    SqlDataReader dr1 = cmd.ExecuteReader();
                    dr1.Read();
                    label1.Text = string.Concat("xếp hạng : ", dr1[4]);
                    con.Close();
                }
                else
                {
                    label1.Text = "hiển thị xếp hạng ";
                }
            }
            catch { }
        }
        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            xephang();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.CurrentRow.Selected = true;
            }
            catch { }
        }
        private void loaddulieuvaotextbox()
        {
            txt_timkiem.DataBindings.Clear();
            txt_timkiem.DataBindings.Add("text", dataGridView1.DataSource, "username");
        }
    }
}
