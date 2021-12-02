using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace game_caro
{
    public partial class Form1 : Form
    {
        public string username;
        private chessboardmanager cb;
        connectmenager socket;
        private int sotranthang_hientailucchoivsnguoi=0;
        private int sotranthua_hientailucchoivsnguoi=0;
        string chuoi_kn = @"Data Source=QUANNGUYEN96-PC\SQLEXPRESS;Initial Catalog=Q.Ly_AccountsGameCaro;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        public Form1()
        {

            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            cb = new chessboardmanager(pnlmain, txtplayername, ptbplayername, lbprocess,player1,player2,cb_capdo);
            cb.Endedgame += Cb_Endedgame;
            cb.Playermark += Cb_Playermark;
            cb.Playermark_pvc += Cb_Playermark_pvc;
            cb.Endedgame_nguoithang += Cb_Endedgame_nguoithang;
            cb.Endedgame_maythang += Cb_Endedgame_maythang;   
            prbtime.Step = Cons.processbal_nhay;
            prbtime.Maximum = Cons.thoigianprocessbal_ketthuc;
            prbtime.Value = 0;
            timer.Interval = Cons.thoigiantimer_nhay;

            socket = new connectmenager();
            newgame_pvp();
            pnlmain.Enabled = false;
        }
        private int tsplayer1 = 0;
        private int tsplayer2 = 0;
        private void Cb_Endedgame_maythang(object sender, EventArgs e)
        {
            tsplayer2++;
            ts2.Text = tsplayer2.ToString();
            timer.Stop();
            pnlmain.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            newGameToolStripMenuItem.Enabled = true;
            MessageBox.Show("Bạn đã thua...!");
        }

        private void Cb_Endedgame_nguoithang(object sender, EventArgs e)
        {
            tsplayer1++;
            ts1.Text = tsplayer1.ToString();
            timer.Stop();
            pnlmain.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            newGameToolStripMenuItem.Enabled = true;
            MessageBox.Show("Bạn đã thắng...!");
        }

        private void Cb_Playermark_pvc(object sender, EventArgs e)
        {
            timer.Start();
            prbtime.Value = 0;
            txtplayername.Text = player1.Text;
        }

        void endgamepvp()
        {
            if (ktra_nguoichoidangdanhco == 1)
            {
                sotranthang_hientailucchoivsnguoi++;
                updatecsdl_cotthang(player1.Text);
                updatecsdl_cotdiem(player1.Text);
            }
            else
            {
                sotranthua_hientailucchoivsnguoi++;
                updatecsdl_cotthua(player1.Text);
                updatecsdl_cotdiem(player1.Text);
            }
            ts1.Text = sotranthang_hientailucchoivsnguoi.ToString();
            ts2.Text = sotranthua_hientailucchoivsnguoi.ToString();

           
            timer.Stop();
            pnlmain.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            newGameToolStripMenuItem.Enabled = true;
        }
        void endgamepvc()
        {
            tsplayer2++;
            ts2.Text = tsplayer2.ToString();
            timer.Stop();
            MessageBox.Show("Hết giờ. Bạn đã thua...!");
            pnlmain.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            newGameToolStripMenuItem.Enabled = true;
        }
        private int ktra_nguoichoidangdanhco;
        private void Cb_Playermark(object sender, buttonClickevent e)
        {
            ktra_nguoichoidangdanhco = 1;
            timer.Start();
            pnlmain.Enabled = false;
            prbtime.Value = 0;
            socket.Send(new socketdata((int)socketCommand.SEND_POINT,"",e.Clickpoint));
            undoToolStripMenuItem.Enabled = false;
            listen();
            if (ktra_nguoichoidangdanhco == 1)
            {
                txtplayername.Text = player2.Text;
                player1.BackColor = Color.Bisque;
                player2.BackColor = Color.GreenYellow;
            }
            else
            {
                txtplayername.Text = player1.Text;
                player2.BackColor = Color.Bisque;
                player1.BackColor = Color.GreenYellow;
            }
        }
        private void Cb_Endedgame(object sender, EventArgs e)
        {
            endgamepvp();
            socket.Send(new socketdata((int)socketCommand.END_GAME, "", new Point()));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            prbtime.PerformStep();
            int count_time = (int)((prbtime.Value / 1000));
            if (prbtime.Value % 1000 == 0)
            {
                if ((Cons.thoigian_cho - count_time ) <= 0)
                {
                    lbprocess.ForeColor = Color.Red;
                    lbprocess.Text = "end";
                }
                else
                {
                    lbprocess.Text = (Cons.thoigian_cho - count_time).ToString();
                    if (Cons.thoigian_cho - count_time  <= 5)
                    {
                        lbprocess.ForeColor = Color.Red;
                    }
                    else
                    {
                        lbprocess.ForeColor = Color.Black;
                    }
                }
            }
            if (prbtime.Value >= prbtime.Maximum)
            {
                if (ktpvp == true && ktpvc == false)
                {
                    endgamepvp();
                    socket.Send(new socketdata((int)socketCommand.TIME_OUT, "", new Point()));
                }
                else
                {
                    endgamepvc();
                }
            }

        }
        void newgame_pvp()
        {
            prbtime.Value = 0;
            timer.Stop();
            undoToolStripMenuItem.Enabled = true;
            cb.drawmain_pvp();
        }
        void newgame_pvc()
        {
            prbtime.Value = 0;
            timer.Stop();
            undoToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = true;
            cb.drawmain_pvc();
        }
        void quit()
        {
            Application.Exit();
        }
        void undo()
        {
            cb.Undo();
            prbtime.Value = 0;
        }
        private bool ktpvp;
        private bool ktpvc;
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ktpvp == true)
            {
                newgame_pvp();
                txtplayername.Text = player1.Text;
                pnlmain.Enabled = true;
                socket.Send(new socketdata((int)socketCommand.NEW_GAME, "", new Point()));
            }
            else
            {
                newgame_pvc();
                pnlmain.Enabled = true;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ktpvc == true && ktpvp == false)
            {
                undo();
            }
            else
            {
                undo();
                socket.Send(new socketdata((int)socketCommand.UNDO, "", new Point()));
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (player2.Text != "" && player2.Text != "computer")
            {
                if (MessageBox.Show("Bạn sẽ bị xử thua , bạn có muốn thoát hay không ?", "thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    e.Cancel = true;   // huy event :dong form
                }
                else
                {
                    ts2.Text = (sotranthua_hientailucchoivsnguoi + 1).ToString();
                    try
                    {
                        updatecsdl_cotthua(player1.Text);
                        updatecsdl_cotdiem(player1.Text);
                        socket.Send(new socketdata((int)socketCommand.QUIT, "", new Point()));
                    }
                    catch { }
                }
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn thoát hay không ?", "thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    e.Cancel = true;   // huy event :dong form
                }
            }
        }
        
        private void Form1_Shown(object sender, EventArgs e)
        {
            txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txtIP.Text))
            {
                txtIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        void listen()
        {
            Thread listenthread = new Thread(() =>
            {
                try
                {
                    socketdata data = (socketdata)socket.Receive();
                    processData(data);
                }
                catch
                {

                }

            });
            listenthread.IsBackground = true;
            listenthread.Start();
        }
        private void nhandulieu_hinhanh()
        {
            try
            {
                con.Open();
                string hinhanh = "select hinhanh from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where username=@USERNAME";
                SqlCommand cmd = new SqlCommand(hinhanh, con);
                cmd.Parameters.Add("@USERNAME", player2.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                byte[] image = (byte[])dr[0];
                MemoryStream ms = new MemoryStream(image);
                pcb_player2.Image = Image.FromStream(ms);
                con.Close();
            }
            catch
            {
                pcb_player2.Image = Image.FromFile("D:\\hinh nen\\hinh15.jpg");
            }
            
                if (ktra_nguoichoidangdanhco == 1)
                {
                    txtplayername.Text = player2.Text;
                }
                else
                {
                    txtplayername.Text = player1.Text;
                }
            
        }
        private void processData(socketdata data)
        {
            switch (data.Command)
            {
                case (int)socketCommand.NOTIFY:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        player2.Text = data.Message;
                        nhandulieu_hinhanh();
                        
                    }));
                    break;
                case (int)socketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        newgame_pvp();
                        txtplayername.Text = player2.Text;
                        pnlmain.Enabled = false;
                    }));
                    break;
                case (int)socketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        ktra_nguoichoidangdanhco = 2;
                        timer.Start();
                        prbtime.Value = 0;
                        pnlmain.Enabled = true;
                        cb.Otherplayermark(data.Point);
                        undoToolStripMenuItem.Enabled = true;
                        if (ktra_nguoichoidangdanhco == 1)
                        {
                            txtplayername.Text = player2.Text;
                            player1.BackColor = Color.Bisque;
                            player2.BackColor = Color.GreenYellow;
                        }
                        else
                        {
                            txtplayername.Text = player1.Text;
                            player2.BackColor = Color.Bisque;
                            player1.BackColor = Color.GreenYellow;
                        }
                    }));
                    break;
                case (int)socketCommand.END_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (ktra_nguoichoidangdanhco == 1)
                        {
                            MessageBox.Show("" + player1.Text + "  đã giành chiến thắng !");
                        }
                        else
                        {
                            MessageBox.Show("" + player1.Text + "  đã thua !");
                        }
                    }));
                    break;
                case (int)socketCommand.TIME_OUT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (ktra_nguoichoidangdanhco == 1)
                        {
                            MessageBox.Show("" + player1.Text + "  đã giành chiến thắng !");
                        }
                        else
                        {
                            MessageBox.Show("" + player1.Text + "  đã thua !");
                        }
                    }));
                    break;
                case (int)socketCommand.UNDO:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        undo();
                        prbtime.Value = 0;
                    }));
                    break;
                case (int)socketCommand.QUIT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        timer.Stop();
                        ts1.Text = (sotranthang_hientailucchoivsnguoi + 1).ToString();
                        updatecsdl_cotthang(player1.Text);
                        updatecsdl_cotdiem(player1.Text);
                        MessageBox.Show("nguười chơi đã thoát ....");
                        pnlmain.Enabled = false;
                        player2.Text = "";
                        pcb_player2.Image = null;
                        player1.BackColor = Color.Bisque;
                        player2.BackColor = Color.Bisque;
                    }));
                    break;
                case (int)socketCommand.TEST:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        player2.Text = data.Message;
                        btn_test.Enabled = true;
                        nhandulieu_hinhanh();
                    }));
                    break;
                case (int)socketCommand.IMAGE_FILENAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pcb_player2.ImageLocation = data.Message;
                    }));
                    break;
                case (int)socketCommand.CHAT_ICON:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        PictureBox picturebox = new PictureBox()
                        {
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Width = 45,
                            Height = 45,
                            Image = Image.FromFile(Application.StartupPath + data.Message),
                            Location = new Point(173, 3)
                        };
                        Panel panel = new Panel()
                        {
                            Width = thongtin.Width - 10,
                            Height = 48,
                            Location = new Point(606, 698)
                        };
                        dodaidoanchat = dodaidoanchat + panel.Height;
                        if (dodaidoanchat > 620)
                        {
                            thongtin.Controls.Clear();
                            dodaidoanchat = 0;
                        }
                        panel.Controls.Add(picturebox);
                        thongtin.Controls.Add(panel);
                    }));
                    break;
                case (int)socketCommand.CHAT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        int k = data.Message.Length / 25 + 1;
                        Button txt_chat2;
                        int chieurong = 0;
                        if (k == 1)
                        {
                            chieurong = (int)(data.Message.Length * 7+10);
                            txt_chat2 = new Button()
                            {
                                AutoSize = false,
                                BackgroundImageLayout = ImageLayout.Stretch,
                                TextAlign = System.Drawing.ContentAlignment.TopLeft,
                                Image = Image.FromFile(Application.StartupPath + "\\Resources\\thanhchat2.png"),
                                Width = chieurong,
                                Height = 23,
                                Text = data.Message,
                            };
                        }
                        else
                        {
                            chieurong = 163;
                            txt_chat2 = new Button()
                            {
                                AutoSize = false,
                                BackgroundImageLayout = ImageLayout.Stretch,
                                TextAlign = System.Drawing.ContentAlignment.TopLeft,
                                Image = Image.FromFile(Application.StartupPath + "\\Resources\\thanhchat2.png"),
                                Width = 163,
                                Height = (int)(17.5 * k + 1),
                                Text = data.Message,
                            };
                        }
                        dodaidoanchat = dodaidoanchat + txt_chat2.Height;
                        if (dodaidoanchat > 620)
                        {
                            thongtin.Controls.Clear();
                            dodaidoanchat = 0;
                        }
                        Label label_empty = new Label()
                        {
                            Width = thongtin.Width - chieurong - 20,
                            Height = txt_chat2.Height,
                            BackColor = thongtin.BackColor
                        };
                        thongtin.Controls.Add(label_empty);
                        thongtin.Controls.Add(txt_chat2);
                    }));
                    break;
                default:
                    break;
            }

            listen();
        }

        private void btnpvp_Click(object sender, EventArgs e)
        {
            
            try
            {
                ktpvp = true;
                ktpvc = false;
                saveToolStripMenuItem.Enabled = false;
                loadToolStripMenuItem.Enabled = false;
                btn_test.Enabled = true;
                btnpvp.BackColor = Color.Blue;
                btnpvc.BackColor = Color.Gainsboro;
                ts1.Text = "0";
                ts2.Text = "0";

                newgame_pvp();
                pnlmain.Enabled = false;
                socket.IP = txtIP.Text;
                if (!socket.connectServer())               // kiểm tra xem đã có kết nối với sever (máy chủ ) chưa
                {
                    btn_test.Enabled = true;
                    socket.isServer = true;                 // nếu có rồi thì trả về đúng
                    //pnlmain.Enabled = true;                 // đồng thời mở bàn cờ lên để người ta đánh trước tương tự ngược lại khi không có là sever thì nó là client và ngồi nghe
                    socket.CreateServer();                   // nếu chưa có kết nối thì tạo ra sever
                    MessageBox.Show("Đang tìm người chơi !...");
                    ktra_nguoichoidangdanhco = 2;
                    listen();
                    
                }
                else
                {
                    btn_test.Enabled = false;
                    socket.isServer = false;
                    //pnlmain.Enabled = false;
                    listen();
                    MessageBox.Show("Đã có người chơi . Chờ người chơi đồng ý , ấn kết nối để chơi !...");
                    ktra_nguoichoidangdanhco = 1;
                    socket.Send(new socketdata((int)socketCommand.NOTIFY, "" + player1.Text, new Point()));
                }
            }
            catch
            {
                MessageBox.Show("lỗi kết nối , xin vui lòng kiểm tra lại...!");
                pnlmain.Enabled = false;
            }
        }

        private void btnpvc_Click(object sender, EventArgs e)
        {
            player1.BackColor = Color.GreenYellow;
            player2.Text = "computer";
            ktpvc = true;
            ktpvp = false;
            saveToolStripMenuItem.Enabled = true;
            loadToolStripMenuItem.Enabled = true;
            btnpvc.BackColor = Color.Blue;
            btnpvp.BackColor = Color.Gainsboro;
            pnlmain.Enabled = true;
            newgame_pvc();
            txtplayername.Text = player1.Text;
            cb_capdo.Enabled = true;
            //timer.Start();
        }
        private void play_music()
        {
            nhac = new SoundPlayer(Application.StartupPath + "\\Resources\\I-Do-911.wav");
            nhac.Play();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\Resources\\icon_camxuc\\ic14.gif");
            pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\Resources\\hinhtranggiay.png");
            panel5.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();
            undoToolStripMenuItem.Enabled = false;
            btn_thongtin.Text = username;
            player1.Text = username;
            thoigiankik_loamo = 0;
            thoigianbatdau_baihat = 0;
            cb_capdo.Text = "dễ";
            cb_capdo.Enabled = false;
            timerlabel.Start();


            
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = "select * from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where (username=N'"+player1.Text+"')";
            cmd = new SqlCommand(timkiem, con);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);
            //data.DataSource = dt;
            //textBox1.DataBindings.Clear();
            //textBox1.DataBindings.Add("text", data.DataSource, "thang");
            //textBox2.DataBindings.Clear();
            //textBox2.DataBindings.Add("text", data.DataSource, "thua");
            
            
            dr.Read();
            textBox1.Text = dr[2].ToString();
            textBox2.Text = dr[3].ToString();
            txtTen.Text = dr[6].ToString();
            if (dr.HasRows)
            {
                byte[] image = (byte[])dr[4];
                if (image == null)
                {
                    pcb_player1.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(image);
                    pcb_player1.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("loi khong hien thi dk");
            }
            con.Close();
        }
        private SoundPlayer nhac;
        private int loa = 1;
        
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            loa = loa == 0 ? 1 : 0;
            if(loa==1)
            {
                thoigianbatdau_baihat = thoigiankik_loamo;
                nhac.Play();
            }
            else
            {
                nhac.Stop();
            }
            nhac.Dispose();
        }
        private int h = 0, p = 0, s = 0;
        private int thoigiankik_loamo;

        private void btn_doipass_Click(object sender, EventArgs e)
        {
            frm_doipass frmdoipas = new frm_doipass();
            frmdoipas.tentaikhoan = player1.Text;
            frmdoipas.ShowDialog();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            listen();
            try
            {
                socket.Send(new socketdata((int)socketCommand.TEST, "" + player1.Text, new Point()));
                MessageBox.Show("Kết nối thành công !");
                if (btn_dongmochat.Text == "đóng chat")
                {
                    panel5.Show();
                }
                else
                {
                    panel5.Hide();
                }
                if(ktra_nguoichoidangdanhco==2)
                {
                    pnlmain.Enabled = true;
                    player2.BackColor = Color.Bisque;
                    player1.BackColor = Color.GreenYellow;

                }
                else
                {
                    pnlmain.Enabled = false;
                    player1.BackColor = Color.Bisque;
                    player2.BackColor = Color.GreenYellow;
                }

            }
            catch { MessageBox.Show("chưa có kết nối !"); }
        }
        private void updatecsdl_cotdiem(string ten12)
        {
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET diem=thang*thang/(thang+thua) WHERE username=N'" + ten12 + "'";
            cmd = new SqlCommand(timkiem, con);
            SqlDataReader dr = cmd.ExecuteReader();
            con.Close();
        }
        private void updatecsdl_cotthang(string ten12)
        {
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET thang=thang+1 WHERE username=N'" + ten12 + "'";
            cmd = new SqlCommand(timkiem, con);
            SqlDataReader dr = cmd.ExecuteReader();
            con.Close();
        }
        private void updatecsdl_cotthua(string ten12)
        {
            con = new SqlConnection(chuoi_kn);
            con.Open();
            string timkiem = "UPDATE [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] SET thua=thua+1 WHERE username=N'" + ten12 + "'";
            cmd = new SqlCommand(timkiem, con);
            SqlDataReader dr = cmd.ExecuteReader();
            con.Close();
        }
        private void player1_Click(object sender, EventArgs e)
        {
            frm_thongtinnguoichoi frm_thongtin = new frm_thongtinnguoichoi();
            frm_thongtin.name = player1.Text;
            frm_thongtin.Show();
        }

        private void player2_Click(object sender, EventArgs e)
        {
            if (string.Compare(player2.Text, "computer", false) != 0 && string.Compare(player2.Text, "", false) != 0)
            {
                frm_thongtinnguoichoi frm_thongtin = new frm_thongtinnguoichoi();
                frm_thongtin.name = player2.Text;
                frm_thongtin.Show();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            nhac.Stop();
            timerlabel.Stop();
            con.Close();
            Application.Exit();
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_gioithieu frmgioithieu = new frm_gioithieu();
            frmgioithieu.Show();
        }
        private int dodaidoanchat = 0;
        private void menuchat()
        {
            if (txt_chat.Text != "")
            {
                int dodaitext = txt_chat.Text.Length;
                int k = dodaitext / 25 + 1;      // 1 dong trong label co do cao =15
                Button txt_chat1 = new Button();
                //{
                //    AutoSize = false,
                //    Width = 163,
                //    Height = 15 * k,
                //    BackColor = thongtin.BackColor,
                //    ForeColor = Color.Black,
                //    Text=txt_chat.Text,
                //};
                if(k == 1)
                {
                    txt_chat1 = new Button()
                    {
                        AutoSize = false,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        TextAlign = System.Drawing.ContentAlignment.TopLeft,
                        Image = Image.FromFile(Application.StartupPath + "\\Resources\\thanhchat.png"),
                        Width = (int)(txt_chat.Text.Length * 7+10),
                        Height = 23,
                        Text = txt_chat.Text,
                    };
                }
                else
                {
                    txt_chat1 = new Button()
                    {
                        AutoSize = false,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        TextAlign = System.Drawing.ContentAlignment.TopLeft,
                        Image = Image.FromFile(Application.StartupPath + "\\Resources\\thanhchat.png"),
                        Width = 163,
                        Height = (int)(17.5 * k+1),
                        Text = txt_chat.Text,
                    };
                }
                dodaidoanchat = dodaidoanchat + txt_chat1.Height;
                if (dodaidoanchat >620)
                {
                    thongtin.Controls.Clear();
                    dodaidoanchat = 0;
                }
                thongtin.Controls.Add(txt_chat1);
                Label label_empty = new Label()
                {
                    Width = thongtin.Width - txt_chat1.Width - 20,
                    Height = txt_chat1.Height,
                    BackColor = thongtin.BackColor
                };
                thongtin.Controls.Add(label_empty);
                try
                {
                    socket.Send(new socketdata((int)socketCommand.CHAT, "" + txt_chat.Text, new Point()));
                }
                catch { }
            }
            txt_chat.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            menuchat();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==Keys.Enter)
            {
                menuchat();
            }
        }
        private string file_anh;
        private string trangthaidoianh = "1";
        private void getvalue(string images_filename)
        {
            file_anh = images_filename;
            pcb_player1.ImageLocation = images_filename;
            trangthaidoianh = trangthaidoianh == "1" ? "2" : "1";
            textBox3.Text = trangthaidoianh;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            frm_thongtintaikhoan frm_thongtintaikhoan = new frm_thongtintaikhoan();
            frm_thongtintaikhoan.name = player1.Text;
            frm_thongtintaikhoan.sotranthang = ts1.Text;
            frm_thongtintaikhoan.sotranthua = ts2.Text;
            frm_thongtintaikhoan.mydata += new frm_thongtintaikhoan.getdata(getvalue);
            frm_thongtintaikhoan.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            string hinhanh = "select hinhanh from [Q.Ly_AccountsGameCaro].[dbo].[TaiKhoan_Player] where username=@USERNAME";
            SqlCommand cmd = new SqlCommand(hinhanh, con);
            cmd.Parameters.Add("@USERNAME", player2.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            byte[] image = (byte[])dr[0];
            MemoryStream ms = new MemoryStream(image);
            pcb_player2.Image = Image.FromStream(ms);
            con.Close();
        }
        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            socket.Send(new socketdata((int)socketCommand.IMAGE_FILENAME, "" + file_anh, new Point()));
        }

        private void btn_dongmochat_Click(object sender, EventArgs e)
        {
            
            if(btn_dongmochat.Text== "đóng chat")
            {
                thongtin.Hide();
                panel5.Hide();
                this.Width = this.Width - thongtin.Width;
            }
            if (btn_dongmochat.Text == "mở chat")
            {
                thongtin.Show();
                if (player2.Text != "" && player2.Text != "computer")
                {
                    panel5.Show();
                }
                this.Width = this.Width + thongtin.Width;
            }
            btn_dongmochat.Text = btn_dongmochat.Text == "đóng chat" ? "mở chat" : "đóng chat";
        }
        private void laydulieu_nhandan(string image_filename)
        {
            PictureBox picturebox = new PictureBox()
            {
                SizeMode=PictureBoxSizeMode.StretchImage,
                Width = 45,
                Height = 45,
                Image = Image.FromFile(Application.StartupPath + image_filename)
            };
            Panel panel = new Panel()
            {
                Width = thongtin.Width - 10,
                Height = 48,
            };
            dodaidoanchat = dodaidoanchat + panel.Height;
            if (dodaidoanchat > thongtin.Height - 100)
            {
                thongtin.Controls.Clear();
                dodaidoanchat = 0;
            }
            panel.Controls.Add(picturebox);
            thongtin.Controls.Add(panel);
            socket.Send(new socketdata((int)socketCommand.CHAT_ICON, "" + image_filename, new Point()));
        }
        private void laydulieu_iconcamxuc(string image_filename)
        {
            PictureBox picturebox = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 45,
                Height = 45,
                Image = Image.FromFile(Application.StartupPath + image_filename)
            };
            Panel panel = new Panel()
            {
                Width = thongtin.Width - 10,
                Height = 48,
            };
            dodaidoanchat = dodaidoanchat + panel.Height;
            if (dodaidoanchat > thongtin.Height - 100)
            {
                thongtin.Controls.Clear();
                dodaidoanchat = 0;
            }
            panel.Controls.Add(picturebox);
            thongtin.Controls.Add(panel);
            socket.Send(new socketdata((int)socketCommand.CHAT_ICON, "" + image_filename, new Point()));
        }
        private void lb_nhandan_Click(object sender, EventArgs e)
        {
            frm_nhandan frm_nhandan = new frm_nhandan();
            frm_nhandan.mydata += new frm_nhandan.getdata(laydulieu_nhandan);
            frm_nhandan.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            frm_iconcamxuc frm_Camxuc = new frm_iconcamxuc();
            frm_Camxuc.mydata += new frm_iconcamxuc.getdata(laydulieu_iconcamxuc);
            frm_Camxuc.Show();
        }
        
        private int thoigianbatdau_baihat;   // do dai bai hat that i do 911 la 3phut 20s =200s
        private void timerlabel_Tick(object sender, EventArgs e)
        {
            if (loa == 1)
            {
                if ((thoigiankik_loamo - thoigianbatdau_baihat) % 200 == 0)
                {
                    play_music();
                }
            }
            thoigiankik_loamo++;
            timerlabel.Interval = 1000;
            {
                giay.Text = (s).ToString();
                phut.Text = p.ToString();
                gio.Text = h.ToString();
                s++;
                if (s>59)
                {
                    s = 0;
                    p++;
                }
                
                if(p>59)
                {
                    p = 0;
                    h++;
                }
                
            }
            {
                label1.Location = new Point(label1.Location.X, label1.Location.Y - 10);
                if (label1.Location.Y <= panel1.Location.Y - panel1.Height-pictureBox4.Height+20)
                {
                    label1.Location = new Point(label1.Location.X, label1.Location.Y + panel1.Height + label1.Height -40);
                }
            }
        }
        private void topToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TOP_Player frm = new TOP_Player();
            frm.Show();
        }
        private void newgamemoi()
        {
            cb.drawmain();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            newgamemoi();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cb.savegame();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newgame_pvc();
            cb.loadgame();
        }


    }
}
