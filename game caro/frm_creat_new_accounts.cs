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
    public partial class frm_creat_new_accounts : Form
    {
        string chuoiketnoi = @"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True";
        SqlConnection con;
        SqlCommand command;
        public frm_creat_new_accounts()
        {
            InitializeComponent();
        }
        private int kiemtrasutontaicuataikhoan()
        {
            try
            {
                con = new SqlConnection(chuoiketnoi);
                con.Open();
                string kiemtradangnhap = "select Count(*) from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where username=@acc";
                command = new SqlCommand(kiemtradangnhap, con);
                command.Parameters.Add("@acc", txt_tentaikhoanmoi.Text);
                int x = (int)command.ExecuteScalar();
                return x;
            }
            catch { return 0; }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (kiemtrasutontaicuataikhoan() == 1)
            {
                lb_baoloitentaikhoan.ForeColor = Color.Red;
                lb_baoloitentaikhoan.Text = "Tên tài khoản đã tồn tại";
            }
            else
            {
                if (txt_tentaikhoanmoi.Text == "")
                {
                    lb_baoloitentaikhoan.ForeColor = Color.Red;
                    lb_baoloitentaikhoan.Text = "chưa nhập tên tài khoản";
                }
                else
                {
                    lb_baoloitentaikhoan.Text = "";
                }
            }
        }
        
        
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
        public delegate void getdata(string username);
        public getdata mydata;
        private string tenmangde = "";
        private string tenmangtrungbinh = "";
        private string tenmangkho = "";
        private void taotaikhoan()
        {
            if (kiemtrasutontaicuataikhoan() == 0 )
            {
                if (images_filename != null)
                {
                    button1.Enabled = true;
                    string themdulieu = "insert into TaiKhoan_Player values (@tentaikhoan,null,'0','0',@hinhanh,'0',@ten)";
                    SqlCommand cmd = new SqlCommand(themdulieu, con);
                    cmd.Parameters.AddWithValue("@tentaikhoan", txt_tentaikhoanmoi.Text);
                    cmd.Parameters.AddWithValue("@hinhanh", convertImageToBye(images_filename));
                    cmd.Parameters.AddWithValue("@ten", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tạo tài khoản thành công !");
                    this.Close();
                }
                else
                {
                    button1.Enabled = true;
                    string themdulieu = "insert into TaiKhoan_Player values (@tentaikhoan,null,'0','0',@hinhanh,'0',@ten)";
                    SqlCommand cmd = new SqlCommand(themdulieu, con);
                    cmd.Parameters.AddWithValue("@tentaikhoan", txt_tentaikhoanmoi.Text);
                    cmd.Parameters.AddWithValue("@hinhanh", convertImageToBye("D:\\hinh nen\\hinh15.jpg"));
                    cmd.Parameters.AddWithValue("@ten", textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tạo tài khoản thành công !");
                    this.Close();
                }
                mydata(txt_tentaikhoanmoi.Text);
                tenmangde = string.Concat(txt_tentaikhoanmoi.Text, "de.txt");
                tenmangkho = string.Concat(txt_tentaikhoanmoi.Text, "kho.txt");
                tenmangtrungbinh = string.Concat(txt_tentaikhoanmoi.Text, "trungbinh.txt");
                StreamWriter sw = new StreamWriter(tenmangde);
                StreamWriter sw1 = new StreamWriter(tenmangtrungbinh);
                StreamWriter sw2 = new StreamWriter(tenmangkho);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            taotaikhoan();
        }
        
        private void frm_creat_new_accounts_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==Keys.Enter)
            {
                taotaikhoan();
            }
        }
        private string images_filename=null;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog chonfileanh = new OpenFileDialog();
            chonfileanh.Filter = chonfileanh.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|";
            chonfileanh.FilterIndex = 1;
            chonfileanh.RestoreDirectory = true;
            if (chonfileanh.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = chonfileanh.FileName;
                images_filename = chonfileanh.FileName;
            }

        }

        private void frm_creat_new_accounts_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hinh15.jpg");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\Resources\\ic13.gif");
            pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\Resources\\t.gif");
            pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Resources\\a.gif");
            pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\Resources\\o.gif");
            pictureBox6.Image = Image.FromFile(Application.StartupPath + "\\Resources\\t.gif");
            pictureBox7.Image = Image.FromFile(Application.StartupPath + "\\Resources\\a.gif");
            pictureBox8.Image = Image.FromFile(Application.StartupPath + "\\Resources\\i.gif");
            pictureBox9.Image = Image.FromFile(Application.StartupPath + "\\Resources\\k.gif");
            pictureBox10.Image = Image.FromFile(Application.StartupPath + "\\Resources\\h.gif");
            pictureBox11.Image = Image.FromFile(Application.StartupPath + "\\Resources\\o.gif");
            pictureBox12.Image = Image.FromFile(Application.StartupPath + "\\Resources\\a.gif");
            pictureBox13.Image = Image.FromFile(Application.StartupPath + "\\Resources\\n.gif");
            pictureBox14.Image = Image.FromFile(Application.StartupPath + "\\Resources\\m.gif");
            pictureBox15.Image = Image.FromFile(Application.StartupPath + "\\Resources\\o.gif");
            pictureBox16.Image = Image.FromFile(Application.StartupPath + "\\Resources\\i.gif");
        }
    }
}
