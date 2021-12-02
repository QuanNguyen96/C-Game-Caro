using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    public partial class frm_thongtintaikhoan : Form
    {
        public frm_thongtintaikhoan()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True");
        SqlCommand cmd;
        public string name;
        public string sotranthang, sotranthua;
        private byte[] convertImageToBye(string images_filename)
        {
            FileStream fs;
            fs = new FileStream(images_filename, FileMode.Open, FileAccess.Read);
            byte[] PicToBye = new byte[fs.Length];
            fs.Read(PicToBye, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            return PicToBye;
        }
        private Image byArrayToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            Image image = Image.FromStream(ms);
            return image;
        }
        private string images_filename;
        private void avatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog chonfileanh = new OpenFileDialog();
            chonfileanh.Filter = chonfileanh.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|";
            chonfileanh.FilterIndex = 1;
            chonfileanh.RestoreDirectory = true;
            if (chonfileanh.ShowDialog() == DialogResult.OK)
            {
                // anhchon = Image.FromFile(chonfileanh.FileName);
                //anhSV.Image = anhchon;
                avatar.ImageLocation = chonfileanh.FileName;
                images_filename = chonfileanh.FileName;
                button1.Enabled = true;
            }

        }
        public delegate void getdata(string imgaes);
        public getdata mydata;
        private void button1_Click(object sender, EventArgs e)
        {
            try
           {
                con.Open();
                string doipass = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET hinhanh=@HINHANH WHERE username=@USERNAME";
                cmd = new SqlCommand(doipass, con);
                cmd.Parameters.AddWithValue("@USERNAME", lb_tentk.Text);
                cmd.Parameters.AddWithValue("@HINHANH", convertImageToBye(images_filename));
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Đổi avatar thành công !");
                button1.Enabled = false;
                
                mydata(images_filename);
            }
            catch { }
        }
        
        private void frm_thongtintaikhoan_Load(object sender, EventArgs e)
        {
            textBox1.Hide();
            textBox2.Hide();

            
            con.Open();
            string timkiem = "select * from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where (username=N'" + name + "')";
            cmd = new SqlCommand(timkiem, con);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lb_tentk.Text = dr[0].ToString();
                lb_sotranthang.Text = (int.Parse(dr[2].ToString()) + int.Parse(sotranthang)).ToString();
                lb_sotranthua.Text = (int.Parse(dr[3].ToString()) + int.Parse(sotranthua)).ToString();
                byte[] image = (byte[])dr[4];
                if (image == null)
                {
                    //MessageBox.Show("hinh null");
                    avatar.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hinh15.jpg");
                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    avatar.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("loi khong hien thi dk");
            }
            

            //DataTable dt = new DataTable();
            //dt.Load(dr);
            //data.DataSource = dt;
            //textBox1.DataBindings.Clear();
            //textBox1.DataBindings.Add("text", data.DataSource, "thang");
            //textBox2.DataBindings.Clear();
            //textBox2.DataBindings.Add("text", data.DataSource, "thua");


            //lb_tentk.Text = name;
            //lb_sotranthang.Text = (int.Parse(textBox1.Text) + int.Parse(sotranthang)).ToString();
            //lb_sotranthua.Text = (int.Parse(textBox2.Text) + int.Parse(sotranthua)).ToString();
            con.Close();
        }
    }
}
