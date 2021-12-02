using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_caro
{
    class chessboardmanager
    {
        #region phuong thuc bien trong class :method
        private Panel chessboard;
        public Panel ChessBoard
        {
            get { return chessboard; }
            set { chessboard = value; }
        }
        private Label lb;
        private event EventHandler<buttonClickevent> playermark;
        public event EventHandler<buttonClickevent> Playermark
        {
            add { playermark += value; }
            remove { playermark -= value; }
        }
        public event EventHandler playermark_pvc;
        public event EventHandler Playermark_pvc
        {
            add { playermark_pvc += value; }
            remove { playermark_pvc -= value; }
        }
        private event EventHandler endedgame;
        public event EventHandler Endedgame
        {
            add { endedgame += value; }
            remove { endedgame -= value; }
        }
        private event EventHandler endedgame_nguoithang;
        public event EventHandler Endedgame_nguoithang
        {
            add { endedgame_nguoithang += value; }
            remove { endedgame_nguoithang -= value; }
        }
        private event EventHandler endedgame_maythang;
        public event EventHandler Endedgame_maythang
        {
            add { endedgame_maythang += value; }
            remove { endedgame_maythang -= value; }
        }
        private Stack<playinfo> buocdinguoichoi;
        public Stack<playinfo> Buocdinguoichoi
        {
            get { return buocdinguoichoi; }
            set { buocdinguoichoi = value; }
        }
        private Label nameplayer;
        public Label NamePlayer
        {
            get { return nameplayer; }
            set { nameplayer = value; }
        }
        private PictureBox markplayer;
        public PictureBox MarkPlayer
        {
            get { return markplayer; }
            set { markplayer = value; }
        }
        private Button player1, player2;
        public chessboardmanager(Panel chessboard, Label nameplayer, PictureBox markplayer, Label lb, Button player1, Button player2, ComboBox capdo)
        {
            this.ChessBoard = chessboard;
            this.MarkPlayer = markplayer;
            this.NamePlayer = nameplayer;
            this.Capdo = capdo;
            this.lb = lb;
            this.player1 = player1;
            this.player2 = player2;
            this.player = new List<player>
            {
                new player("người thứ nhất",Image.FromFile(Application.StartupPath+"\\Resources\\a18.jpg")),
                new player("người thứ hai",Image.FromFile(Application.StartupPath+"\\Resources\\a19.jpg"))
            };
        }
        private List<player> player;
        private int currentplayer;
        public List<player> Player
        {
            get { return player; }
            set { player = value; }
        }
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
        private ComboBox capdo;
        public ComboBox Capdo
        {
            get { return capdo; }
            set { capdo = value; }
        }
        #endregion
        #region vẽ bàn cờ cho 2 chế độ : pvp là người vs người ,pvc là người vs máy
        public void drawmain_pvp()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();

            buocdinguoichoi = new Stack<playinfo>();
            lb.Text = Cons.thoigian_cho.ToString();
            lb.ForeColor = Color.Black;
            currentplayer = 0;
            changeplayer();
            Matrix = new List<List<Button>>();
            Button btnn = new Button() { Width = 0, Height = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.so_dong; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.width,
                        Height = Cons.height,
                        Location = new Point(btnn.Location.X + btnn.Width, btnn.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btnpvp_Click;
                    chessboard.Controls.Add(btn);
                    matrix[i].Add(btn);
                    btnn = btn;

                }
                #region chu thich ve button
                // khong giong nhu o trong kieu bien 
                //trong 1 ham bat ki khi gan button 1= button 2, mac du chung ta chi su dung button 2 ma khong su dung button 1
                //nhung khi thay doi thuoc tinh cua button 1 thi mac dinh button 2 cung thay doi hai cai la 1
                // o day cung vay khi gan btnn=btn thi ban da mac dinh 2 cai ve 1 va o ham duoi khi ban thay doi toa do cua btnn
                //tuc btnn.location=new point... va btnn.width va btnn.height=0 thi mac dinh btn cung se co gia tri tuong tu
                // va tu nhien khi chay ta se thay mat mot cot button vi no da co chieu dai va chieu rong =0
                // cho nen de day du thi ban phai them 1 button khac nua ,vd o ngay duoi day
                #endregion
                btnn.Location = new Point(0, btnn.Location.Y + Cons.height);
                btnn.Width = 0;
                btnn.Height = 0;
            }
        }
        public void drawmain()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();

            buocdinguoichoi = new Stack<playinfo>();
            lb.Text = Cons.thoigian_cho.ToString();
            lb.ForeColor = Color.Black;
            currentplayer = 1;
            ktranguoithang = false;
            changeplayer();
            Matrix = new List<List<Button>>();
            Button btnn = new Button() { Width = 0, Height = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.so_dong; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.width,
                        Height = Cons.height,
                        Location = new Point(btnn.Location.X + btnn.Width, btnn.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btn1_Click;
                    chessboard.Controls.Add(btn);
                    matrix[i].Add(btn);
                    btnn = btn;

                }
                #region chu thich ve button
                // khong giong nhu o trong kieu bien 
                //trong 1 ham bat ki khi gan button 1= button 2, mac du chung ta chi su dung button 2 ma khong su dung button 1
                //nhung khi thay doi thuoc tinh cua button 1 thi mac dinh button 2 cung thay doi hai cai la 1
                // o day cung vay khi gan btnn=btn thi ban da mac dinh 2 cai ve 1 va o ham duoi khi ban thay doi toa do cua btnn
                //tuc btnn.location=new point... va btnn.width va btnn.height=0 thi mac dinh btn cung se co gia tri tuong tu
                // va tu nhien khi chay ta se thay mat mot cot button vi no da co chieu dai va chieu rong =0
                // cho nen de day du thi ban phai them 1 button khac nua ,vd o ngay duoi day
                #endregion
                btnn.Location = new Point(0, btnn.Location.Y + Cons.height);
                btnn.Width = 0;
                btnn.Height = 0;
            }
        }
        public void drawmain_pvc()
        {
            ChessBoard.Enabled = true;
            ChessBoard.Controls.Clear();

            buocdinguoichoi = new Stack<playinfo>();
            lb.Text = Cons.thoigian_cho.ToString();
            lb.ForeColor = Color.Black;
            currentplayer = 1;
            ktranguoithang = false;
            changeplayer();
            Matrix = new List<List<Button>>();
            Button btnn = new Button() { Width = 0, Height = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.so_dong; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.width,
                        Height = Cons.height,
                        Location = new Point(btnn.Location.X + btnn.Width, btnn.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = i.ToString()
                    };
                    btn.Click += Btnpvc_Click;
                    chessboard.Controls.Add(btn);
                    matrix[i].Add(btn);
                    btnn = btn;

                }
                #region chu thich ve button
                // khong giong nhu o trong kieu bien 
                //trong 1 ham bat ki khi gan button 1= button 2, mac du chung ta chi su dung button 2 ma khong su dung button 1
                //nhung khi thay doi thuoc tinh cua button 1 thi mac dinh button 2 cung thay doi hai cai la 1
                // o day cung vay khi gan btnn=btn thi ban da mac dinh 2 cai ve 1 va o ham duoi khi ban thay doi toa do cua btnn
                //tuc btnn.location=new point... va btnn.width va btnn.height=0 thi mac dinh btn cung se co gia tri tuong tu
                // va tu nhien khi chay ta se thay mat mot cot button vi no da co chieu dai va chieu rong =0
                // cho nen de day du thi ban phai them 1 button khac nua ,vd o ngay duoi day
                #endregion
                btnn.Location = new Point(0, btnn.Location.Y + Cons.height);
                btnn.Width = 0;
                btnn.Height = 0;
            }
            Matrix[Cons.so_dong / 2 - 1][Cons.so_cot / 2 - 1].BackgroundImage = player[0].Mark;
        }
        private void Btn1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return;
            mark(btn);
            buocdinguoichoi.Push(new playinfo(toadobtn(btn), currentplayer));
            if (isendgame(btn))
            {
                thaydoibackgroundimagekhiendgame(btn, trangthaiketthuc, currentplayer, diembatdau);
            }
            currentplayer = currentplayer == 1 ? 0 : 1;
            changeplayer();
            if (playermark != null)
            {
                playermark(this, new buttonClickevent(toadobtn(btn)));
            }
            if (isendgame(btn))
            {
                endgame();
            }
        }

        #endregion
            #region kiem tra ket thuc game trong che do nguoi vs nguoi
        public void endgame()
        {
            if (endedgame != null)
            {
                endedgame(this, new EventArgs());
            }
        }
        #endregion
        #region kiem tra ket thuc game trong che do nguoi vs may
        public void endgamepvp()
        {
            if (endedgame_nguoithang != null)
            {
                endedgame_nguoithang(this, new EventArgs());
            }
        }
        public void endgamepvc()
        {
            if (endedgame_maythang != null)
            {
                endedgame_maythang(this, new EventArgs());
            }
        }
        #endregion
        #region sự kiện click trong 2 th người vs người và người vs máy
        private void Btnpvp_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return;
            mark(btn);
            buocdinguoichoi.Push(new playinfo(toadobtn(btn), currentplayer));
            if (isendgame(btn))
            {
                thaydoibackgroundimagekhiendgame(btn, trangthaiketthuc, currentplayer, diembatdau);
            }
            currentplayer = currentplayer == 1 ? 0 : 1;
            changeplayer();
            if (playermark != null)
            {
                playermark(this, new buttonClickevent(toadobtn(btn)));
            }
            if (isendgame(btn))
            {
                endgame();
            }
        }
        private void Btnpvc_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
                return;
            nguoidanh(btn);
            if (ktranguoithang == false)
            {
                maydanhco();
            }
        }
        #endregion
        #region đánh cờ chế độ người vs máy
        // người thì currentplayer==1 còn máy là 0
        public void maydanhco()
        {
            Point point = new Point(0, 0);
            if (Capdo.Text == "dễ")
            {
                point = timkiemnuocdi_de();
            }
            if (Capdo.Text == "trung bình")
            {
                point = timkiemnuocdi_trungbinh();
            }
            if (Capdo.Text == "khó")
            {
                //int dong = 0, cot = 0;
                point = timkiemnuocdi_kho();
            }
            currentplayer = 0;
            matrix[point.X][point.Y].BackgroundImage = player[0].Mark;
            markplayer.Image = Player[1].Mark;
            //NamePlayer.Text = player[0].Name;
            buocdinguoichoi.Push(new playinfo(toadobtn(Matrix[point.X][point.Y]), currentplayer));  // luu button vua danh vao stack de undo :xin di lai

            if (isendgame(Matrix[point.X][point.Y]))
            {
                thaydoibackgroundimagekhiendgame(Matrix[point.X][point.Y], trangthaiketthuc, currentplayer, diembatdau);
                endgamepvc();
            }
        }
        private bool ktranguoithang = false;
        private void nguoidanh(Button btn)
        {
            currentplayer = 1;
            if (btn.BackgroundImage != null)
            {
                return;
            }
            btn.BackgroundImage = player[1].Mark;
            MarkPlayer.Image = player[0].Mark;
            NamePlayer.Text = player[1].Name;
            buocdinguoichoi.Push(new playinfo(toadobtn(btn), currentplayer));
            if (playermark_pvc != null)
            {
                playermark_pvc(this, new EventArgs());
            }
            if (isendgame(btn))
            {
                thaydoibackgroundimagekhiendgame(btn, trangthaiketthuc, currentplayer, diembatdau);
                endgamepvp();
                ktranguoithang = true;
            }
        }
        #endregion
        #region nhận dữ liệu rồi đánh vào đó th chơi online
        public void Otherplayermark(Point point)
        {
            Button btn = Matrix[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return;

            mark(btn);
            buocdinguoichoi.Push(new playinfo(toadobtn(btn), currentplayer));
            if (isendgame(btn))
            {
                thaydoibackgroundimagekhiendgame(btn, trangthaiketthuc, currentplayer, diembatdau);
            }
            currentplayer = currentplayer == 1 ? 0 : 1;
            changeplayer();

            if (isendgame(btn))
            {
                endgame();
            }
        }
        #endregion
        #region hiển thị thông tin về tên và ảnh của người chơi (đánh vào button khi có sự kiện click)
        private void mark(Button btn)
        {
            btn.BackgroundImage = player[currentplayer].Mark;
        }
        private void changeplayer()
        {
            //nameplayer.Text = player[currentplayer].Name;
            markplayer.Image = player[currentplayer].Mark;
        }
        #endregion
        #region=lấy tọa độ của button
        private Point toadobtn(Button btn)
        {
            int toadocot = Convert.ToInt32(btn.Tag);
            int toadohang = Matrix[toadocot].IndexOf(btn);
            Point point = new Point(toadohang, toadocot);
            return point;
        }
        #endregion
        #region kiem tra chien thang 5 con
        private bool isendgame(Button btn)
        {
            return isendgamehanghang(btn) || isendgamehangdoc(btn) || isendgamecheotang(btn) || isendgamecheogiam(btn);
        }
        private void thaydoibackgroundimagekhiendgame(Button btn, int trangthaiketthuc, int trangthainguoidanh, int diemxuatphat)
        {
            Point point = toadobtn(btn);
            Image x = Image.FromFile(Application.StartupPath + "\\Resources\\a21.JPG");
            Image y = Image.FromFile(Application.StartupPath + "\\Resources\\a22.JPG");
            if (trangthainguoidanh == 0)
            {
                if (trangthaiketthuc == 1)
                {
                    matrix[point.Y][point.X - diemxuatphat + 1].BackgroundImage = x;
                    matrix[point.Y][point.X - diemxuatphat + 2].BackgroundImage = x;
                    matrix[point.Y][point.X - diemxuatphat + 3].BackgroundImage = x;
                    matrix[point.Y][point.X - diemxuatphat + 4].BackgroundImage = x;
                    matrix[point.Y][point.X - diemxuatphat + 5].BackgroundImage = x;
                }
                if (trangthaiketthuc == 2)
                {
                    matrix[point.Y - diemxuatphat + 1][point.X].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 2][point.X].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 3][point.X].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 4][point.X].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 5][point.X].BackgroundImage = x;
                }
                if (trangthaiketthuc == 3)
                {
                    matrix[point.Y - diemxuatphat + 1][point.X - diemxuatphat + 1].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 2][point.X - diemxuatphat + 2].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 3][point.X - diemxuatphat + 3].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 4][point.X - diemxuatphat + 4].BackgroundImage = x;
                    matrix[point.Y - diemxuatphat + 5][point.X - diemxuatphat + 5].BackgroundImage = x;
                }
                if (trangthaiketthuc == 4)
                {
                    matrix[point.Y + diemxuatphat - 1][point.X - diemxuatphat + 1].BackgroundImage = x;
                    matrix[point.Y + diemxuatphat - 2][point.X - diemxuatphat + 2].BackgroundImage = x;
                    matrix[point.Y + diemxuatphat - 3][point.X - diemxuatphat + 3].BackgroundImage = x;
                    matrix[point.Y + diemxuatphat - 4][point.X - diemxuatphat + 4].BackgroundImage = x;
                    matrix[point.Y + diemxuatphat - 5][point.X - diemxuatphat + 5].BackgroundImage = x;
                }
            }
            else
            {
                if (trangthaiketthuc == 1)
                {
                    matrix[point.Y][point.X - diemxuatphat + 1].BackgroundImage = y;
                    matrix[point.Y][point.X - diemxuatphat + 2].BackgroundImage = y;
                    matrix[point.Y][point.X - diemxuatphat + 3].BackgroundImage = y;
                    matrix[point.Y][point.X - diemxuatphat + 4].BackgroundImage = y;
                    matrix[point.Y][point.X - diemxuatphat + 5].BackgroundImage = y;
                }
                if (trangthaiketthuc == 2)
                {
                    matrix[point.Y - diemxuatphat + 1][point.X].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 2][point.X].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 3][point.X].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 4][point.X].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 5][point.X].BackgroundImage = y;
                }
                if (trangthaiketthuc == 3)
                {
                    matrix[point.Y - diemxuatphat + 1][point.X - diemxuatphat + 1].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 2][point.X - diemxuatphat + 2].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 3][point.X - diemxuatphat + 3].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 4][point.X - diemxuatphat + 4].BackgroundImage = y;
                    matrix[point.Y - diemxuatphat + 5][point.X - diemxuatphat + 5].BackgroundImage = y;
                }
                if (trangthaiketthuc == 4)
                {
                    matrix[point.Y + diemxuatphat - 1][point.X - diemxuatphat + 1].BackgroundImage = y;
                    matrix[point.Y + diemxuatphat - 2][point.X - diemxuatphat + 2].BackgroundImage = y;
                    matrix[point.Y + diemxuatphat - 3][point.X - diemxuatphat + 3].BackgroundImage = y;
                    matrix[point.Y + diemxuatphat - 4][point.X - diemxuatphat + 4].BackgroundImage = y;
                    matrix[point.Y + diemxuatphat - 5][point.X - diemxuatphat + 5].BackgroundImage = y;
                }
            }
        }
        private int trangthaiketthuc;
        private int diembatdau;
        private bool isendgamehanghang(Button btn)
        {
            Point point = toadobtn(btn);
            int bentrai = 0;

            for (int i = point.X; i >= 0; i--)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    bentrai++;
                }
                else break;
            }
            int benphai = 0;
            for (int i = point.X + 1; i < Cons.so_cot; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    benphai++;
                }
                else break;
            }
            if (bentrai + benphai == 5)
            {
                if (point.X - bentrai < 0 || point.X + benphai + 1 >= Cons.so_cot || Matrix[point.Y][point.X - bentrai].BackgroundImage == null || Matrix[point.Y][point.X + benphai + 1].BackgroundImage == null)
                {
                    diembatdau = bentrai;
                    trangthaiketthuc = 1;
                    return true;
                }
            }
            return false;
        }
        private bool isendgamehangdoc(Button btn)
        {
            Point point = toadobtn(btn);
            int bentren = 0;

            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    bentren++;
                }
                else break;
            }
            int benduoi = 0;
            for (int i = point.Y + 1; i < Cons.so_dong; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    benduoi++;
                }
                else break;
            }
            if (bentren + benduoi == 5)
            {
                if (point.Y - bentren < 0 || point.Y + benduoi + 1 >= Cons.so_dong || Matrix[point.Y - bentren][point.X].BackgroundImage == null || Matrix[point.Y + benduoi + 1][point.X].BackgroundImage == null)
                {
                    diembatdau = bentren;
                    trangthaiketthuc = 2;
                    return true;
                }
            }
            return false;
        }
        private bool isendgamecheogiam(Button btn)
        {
            Point point = toadobtn(btn);
            int cheoduoi = 0;

            for (int i = 0; i <= point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                {
                    break;
                }
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    cheoduoi++;
                }
                else break;
            }
            int cheotren = 0;
            for (int i = 1; i <= Cons.so_cot - point.X; i++)
            {
                if (point.Y + i >= Cons.so_dong || point.X + i >= Cons.so_cot)
                {
                    break;
                }
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    cheotren++;
                }
                else break;
            }
            if (cheotren + cheoduoi == 5)
            {
                if (point.Y - cheoduoi < 0 || point.X - cheoduoi < 0 || point.Y + cheotren + 1 >= Cons.so_dong || point.X + cheotren + 1 >= Cons.so_cot || Matrix[point.Y - cheoduoi][point.X - cheoduoi].BackgroundImage == null || Matrix[point.Y + cheotren + 1][point.X + cheotren + 1].BackgroundImage == null)
                {
                    diembatdau = cheoduoi;
                    trangthaiketthuc = 3;
                    return true;
                }
            }
            return false;

        }
        private bool isendgamecheotang(Button btn)
        {
            Point point = toadobtn(btn);
            int cheotren = 0;

            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y + i >= Cons.so_dong || point.X - i < 0)
                {
                    break;
                }
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    cheotren++;
                }
                else break;

            }
            int cheoduoi = 0;

            for (int i = 1; i <= Cons.so_cot - point.X; i++)
            {
                if (point.X + i >= Cons.so_cot || point.Y - i < 0)
                {
                    break;
                }
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    cheoduoi++;
                }
                else break;
            }
            if (cheotren + cheoduoi == 5)
            {
                if (point.Y - cheoduoi - 1 < 0 || point.X + cheoduoi + 1 >= Cons.so_cot || point.Y + cheotren >= Cons.so_dong || point.X - cheotren < 0 || Matrix[point.Y - cheoduoi - 1][point.X + cheoduoi + 1].BackgroundImage == null || Matrix[point.Y + cheotren][point.X - cheotren].BackgroundImage == null)
                {
                    diembatdau = cheotren;
                    trangthaiketthuc = 4;
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region đi lại 2 nước (1 đã đánh và 1 người còn lại đánh)
        public bool Undo()
        {
            if (buocdinguoichoi.Count <= 0)
                return false;
            bool isundo1 = undoAsteep();
            bool isundo2 = undoAsteep();
            //playinfo oldpoint = buocdinguoichoi.Peek();
            return isundo1 && isundo2;
        }
        private bool undoAsteep()
        {
            if (buocdinguoichoi.Count <= 0)
                return false;

            playinfo oldpoint = buocdinguoichoi.Pop();
            Button btn = matrix[oldpoint.Point.Y][oldpoint.Point.X];
            btn.BackgroundImage = null;
            if (buocdinguoichoi.Count <= 0)
            {
                currentplayer = 0;
            }
            else
            {
                oldpoint = buocdinguoichoi.Peek();

                currentplayer = oldpoint.Currentplayer == 1 ? 0 : 1;
            }
            changeplayer();
            return true;
        }
        #endregion
        #region timnuocdichomay
        private long[] MangDiemTanCong = new long[7] { 0, 3, 24, 192, 1536, 12288, 98304 };
        private long[] MangDiemPhongNgu = new long[7] { 0, 1, 9, 81, 729, 6561, 59049 };
        private Point timkiemnuocdi_trungbinh()
        {
            Point point = new Point();
            long DiemMax = 0;
            for (int i = 0; i < Cons.so_dong; i++)
            {
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    if (matrix[i][j].BackgroundImage == null)
                    {
                        long DiemTanCong = DiemTC_DuyetDoc(i, j) + DiemTC_DuyetNgang(i, j) + DiemTC_DuyetCheoTang(i, j) + DiemTC_DuyetCheoGiam(i, j) + DiemTC_kiemtradacbiet(i, j);
                        long DiemPhongNgu = DiemPN_DuyetDoc(i, j) + DiemPN_DuyetNgang(i, j) + DiemPN_DuyetCheoTang(i, j) + DiemPN_DuyetCheoGiam(i, j) + DiemPN_kiemtradacbiet(i, j) + DiemPN_kiemtrachan2dau(i, j);
                        long diemtam = DiemTanCong - DiemPhongNgu > 0 ? DiemTanCong : DiemPhongNgu;
                        if (DiemMax < diemtam)
                        {
                            DiemMax = diemtam;
                            point = new Point(i, j);
                        }
                    }
                }
            }
            return point;
        }
        private Point timkiemnuocdi_de()
        {
            Point point = new Point();
            long DiemMax = 0;
            for (int i = 0; i < Cons.so_dong; i++)
            {
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    if (matrix[i][j].BackgroundImage == null)
                    {
                        long DiemTanCong = DiemTC_DuyetDoc(i, j) + DiemTC_DuyetNgang(i, j) + DiemTC_DuyetCheoTang(i, j) + DiemTC_DuyetCheoGiam(i, j);
                        long DiemPhongNgu = DiemPN_DuyetDoc(i, j) + DiemPN_DuyetNgang(i, j) + DiemPN_DuyetCheoTang(i, j) + DiemPN_DuyetCheoGiam(i, j);
                        long diemtam = DiemTanCong - DiemPhongNgu > 0 ? DiemTanCong : DiemPhongNgu;
                        if (DiemMax < diemtam)
                        {
                            DiemMax = diemtam;
                            point = new Point(i, j);
                        }
                    }
                }
            }
            return point;
        }
        private long DiemTC_DuyetDoc(int dong, int cot)
        {
            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            if (soquandich == 2) return 0;
            diemtong -= MangDiemPhongNgu[soquandich + 1];
            diemtong += MangDiemTanCong[soquanta];
            return diemtong;
        }
        private long DiemTC_DuyetNgang(int dong, int cot)
        {
            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot; dem++)
            {
                if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0; dem++)
            {
                if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            if (soquandich == 2) return 0;
            diemtong -= MangDiemPhongNgu[soquandich + 1];
            diemtong += MangDiemTanCong[soquanta];
            return diemtong;
        }
        private long DiemTC_DuyetCheoTang(int dong, int cot)
        {
            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            if (soquandich == 2) return 0;
            diemtong -= MangDiemPhongNgu[soquandich + 1];
            diemtong += MangDiemTanCong[soquanta];
            return diemtong;
        }
        private long DiemTC_DuyetCheoGiam(int dong, int cot)
        {

            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                        break;
                    }
                    else break;
                }
            }
            if (soquandich == 2) return 0;
            diemtong -= MangDiemPhongNgu[soquandich + 1];
            diemtong += MangDiemTanCong[soquanta];
            return diemtong;
        }
        private long DiemTC_kiemtradacbiet(int dong, int cot)
        {
            long diemtong = 0;
            // kiemtrahangdoc
            int soquanta1 = 0;
            int soquandich1 = 0;
            if (dong + 1 < Cons.so_dong && Matrix[dong + 1][cot].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && Matrix[dong + 1][cot].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && Matrix[dong - 1][cot].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && Matrix[dong - 1][cot].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;
                            break;
                        }
                        else break;
                    }
                }
            }
            // kiemtra hang ngang
            int soquanta2 = 0;
            int soquandich2 = 0;
            if (cot + 1 < Cons.so_cot && Matrix[dong][cot + 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot + dem < Cons.so_cot; dem++)
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                    }
                    else
                    {
                        if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (cot + 1 < Cons.so_cot && Matrix[dong][cot + 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot; dem++)
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                    }
                    else
                    {
                        if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (cot - 1 >= 0 && Matrix[dong][cot - 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot - dem >= 0; dem++)
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                    }
                    else
                    {
                        if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (cot - 1 >= 0 && Matrix[dong][cot - 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot - dem >= 0; dem++)
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                    }
                    else
                    {
                        if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else break;
                    }
                }
            }
            // kiem tra cheo tang
            int soquanta3 = 0;
            int soquandich3 = 0;
            if (dong - 1 >= 0 && cot + 1 < Cons.so_cot && Matrix[dong - 1][cot + 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot + dem < Cons.so_cot && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && cot + 1 < Cons.so_cot && Matrix[dong - 1][cot + 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && cot - 1 >= 0 && Matrix[dong + 1][cot - 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && cot - 1 >= 0 && Matrix[dong + 1][cot - 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;
                            break;
                        }
                        else break;
                    }
                }
            }
            // kiem tra cheo giam
            int soquanta4 = 0;
            int soquandich4 = 0;
            if (dong + 1 < Cons.so_dong && cot + 1 < Cons.so_cot && Matrix[dong + 1][cot + 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && cot + 1 < Cons.so_cot && Matrix[dong + 1][cot + 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && cot - 1 >= 0 && Matrix[dong - 1][cot - 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot - dem >= 0 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && cot - 1 >= 0 && Matrix[dong - 1][cot - 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot - dem >= 0 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;
                            break;
                        }
                        else break;
                    }
                }
            }
            if (soquandich1 >= 2 || soquandich2 >= 2 || soquandich3 >= 2 || soquandich4 >= 2) return 0;
            if ((soquanta1 >= 2 && soquanta2 >= 2) || (soquanta1 >= 2 && soquanta3 >= 2) || (soquanta1 >= 2 && soquanta4 >= 2) || (soquanta2 >= 2 && soquanta3 >= 2) || (soquanta2 >= 2 && soquanta4 >= 2) || (soquanta3 >= 2 && soquanta4 >= 2))
            {
                diemtong += MangDiemTanCong[1];
            }
            return diemtong;
        }
        private long DiemTC_kiemtrachan2dau(int dong, int cot)
        {
                long diemtong = 0;
            try
            {

                if (matrix[dong][cot - 1].BackgroundImage == player[1].Mark && matrix[dong][cot + 1].BackgroundImage == player[0].Mark && matrix[dong][cot + 2].BackgroundImage == player[0].Mark && matrix[dong][cot + 3].BackgroundImage == player[0].Mark && matrix[dong][cot + 4].BackgroundImage == player[0].Mark && matrix[dong][cot + 5].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong][cot + 1].BackgroundImage == player[1].Mark && matrix[dong][cot - 1].BackgroundImage == player[0].Mark && matrix[dong][cot - 2].BackgroundImage == player[0].Mark && matrix[dong][cot - 3].BackgroundImage == player[0].Mark && matrix[dong][cot - 4].BackgroundImage == player[0].Mark && matrix[dong][cot - 5].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong - 1][cot].BackgroundImage == player[1].Mark && matrix[dong + 1][cot].BackgroundImage == player[0].Mark && matrix[dong + 2][cot].BackgroundImage == player[0].Mark && matrix[dong + 3][cot].BackgroundImage == player[0].Mark && matrix[dong + 4][cot].BackgroundImage == player[0].Mark && matrix[dong + 5][cot].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong + 1][cot].BackgroundImage == player[1].Mark && matrix[dong - 1][cot].BackgroundImage == player[0].Mark && matrix[dong - 2][cot].BackgroundImage == player[0].Mark && matrix[dong - 3][cot].BackgroundImage == player[0].Mark && matrix[dong - 4][cot].BackgroundImage == player[0].Mark && matrix[dong - 5][cot].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong - 1][cot - 1].BackgroundImage == player[1].Mark && matrix[dong + 1][cot + 1].BackgroundImage == player[0].Mark && matrix[dong + 2][cot + 2].BackgroundImage == player[0].Mark && matrix[dong + 3][cot + 3].BackgroundImage == player[0].Mark && matrix[dong + 4][cot + 4].BackgroundImage == player[0].Mark && matrix[dong + 5][cot + 5].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong + 1][cot + 1].BackgroundImage == player[1].Mark && matrix[dong - 1][cot - 1].BackgroundImage == player[0].Mark && matrix[dong - 2][cot - 2].BackgroundImage == player[0].Mark && matrix[dong - 3][cot - 3].BackgroundImage == player[0].Mark && matrix[dong - 4][cot - 4].BackgroundImage == player[0].Mark && matrix[dong - 5][cot - 5].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong - 1][cot + 1].BackgroundImage == player[1].Mark && matrix[dong + 1][cot - 1].BackgroundImage == player[0].Mark && matrix[dong + 2][cot - 2].BackgroundImage == player[0].Mark && matrix[dong + 3][cot - 3].BackgroundImage == player[0].Mark && matrix[dong + 4][cot - 4].BackgroundImage == player[0].Mark && matrix[dong + 5][cot - 5].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
                if (matrix[dong + 1][cot - 1].BackgroundImage == player[1].Mark && matrix[dong - 1][cot + 1].BackgroundImage == player[0].Mark && matrix[dong - 2][cot + 2].BackgroundImage == player[0].Mark && matrix[dong - 3][cot + 3].BackgroundImage == player[0].Mark && matrix[dong - 4][cot + 4].BackgroundImage == player[0].Mark && matrix[dong - 5][cot + 5].BackgroundImage == player[1].Mark)
                {
                    diemtong -= MangDiemTanCong[4];
                }
            }catch { }
                return diemtong;
           
        }
        private long DiemPN_DuyetDoc(int dong, int cot)
        {
            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            if (soquanta == 2) return 0;
            diemtong += MangDiemPhongNgu[soquandich];
            return diemtong;
        }
        private long DiemPN_DuyetNgang(int dong, int cot)
        {
            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot; dem++)
            {
                if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0; dem++)
            {
                if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            if (soquanta == 2) { return 0; }
            diemtong += MangDiemPhongNgu[soquandich];
            return diemtong;
        }
        private long DiemPN_DuyetCheoTang(int dong, int cot)
        {
            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            if (soquanta == 2) return 0;
            diemtong += MangDiemPhongNgu[soquandich];
            return diemtong;
        }
        private long DiemPN_DuyetCheoGiam(int dong, int cot)
        {

            long diemtong = 0;
            int soquanta = 0;
            int soquandich = 0;
            for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            for (int dem = 1; dem < 6 && cot - dem >= 0 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;

                    }
                    else break;
                }
            }
            if (soquanta == 2) return 0;
            diemtong += MangDiemPhongNgu[soquandich];
            return diemtong;
        }
        private long DiemPN_kiemtradacbiet(int dong, int cot)
        {
            long diemtong = 0;
            // kiem tra hang doc
            int soquanta1 = 0;
            int soquandich1 = 0;
            if (dong + 1 < Cons.so_dong && Matrix[dong + 1][cot].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;

                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && Matrix[dong + 1][cot].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;

                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && Matrix[dong - 1][cot].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;

                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && Matrix[dong - 1][cot].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                    {
                        soquanta1++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich1++;

                        }
                        else break;
                    }
                }
            }
            // kiem tra hang ngang
            int soquanta2 = 0;
            int soquandich2 = 0;
            if (cot + 1 < Cons.so_cot && Matrix[dong][cot + 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot + dem < Cons.so_cot; dem++)
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;

                        }
                        else break;
                    }
                }
            }
            if (cot + 1 < Cons.so_cot && Matrix[dong][cot + 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot; dem++)
                {
                    if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;

                        }
                        else break;
                    }
                }
            }
            if (cot - 1 >= 0 && Matrix[dong][cot - 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot - dem >= 0; dem++)
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;

                        }
                        else break;
                    }
                }
            }
            if (cot - 1 >= 0 && Matrix[dong][cot - 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot - dem >= 0; dem++)
                {
                    if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta2++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;

                        }
                        else break;
                    }
                }
            }
            // kiem tra cheo tang
            int soquanta3 = 0;
            int soquandich3 = 0;
            if (dong - 1 >= 0 && cot + 1 < Cons.so_cot && Matrix[dong - 1][cot + 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot + dem < Cons.so_cot && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;

                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && cot + 1 < Cons.so_cot && Matrix[dong - 1][cot + 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;

                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && cot - 1 >= 0 && Matrix[dong + 1][cot - 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;

                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && cot - 1 >= 0 && Matrix[dong + 1][cot - 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta3++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich3++;

                        }
                        else break;
                    }
                }
            }
            // kiem tra cheo giam
            int soquanta4 = 0;
            int soquandich4 = 0;
            if (dong + 1 < Cons.so_dong && cot + 1 < Cons.so_cot && Matrix[dong + 1][cot + 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;

                        }
                        else break;
                    }
                }
            }
            if (dong + 1 < Cons.so_dong && cot + 1 < Cons.so_cot && Matrix[dong + 1][cot + 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
                {
                    if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;

                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && cot - 1 >= 0 && Matrix[dong - 1][cot - 1].BackgroundImage == null)
            {
                for (int dem = 2; dem < 6 && cot - dem >= 0 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;

                        }
                        else break;
                    }
                }
            }
            if (dong - 1 >= 0 && cot - 1 >= 0 && Matrix[dong - 1][cot - 1].BackgroundImage != null)
            {
                for (int dem = 1; dem < 6 && cot - dem >= 0 && dong - dem >= 0; dem++)
                {
                    if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                    {
                        soquanta4++;
                        break;
                    }
                    else
                    {
                        if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                        {
                            soquandich4++;

                        }
                        else break;
                    }
                }
            }
            if (soquanta1 >= 2 || soquanta2 >= 2 || soquanta3 >= 2 || soquanta4 >= 2) return 0;
            if ((soquandich1 >= 2 && soquandich2 >= 2) || (soquandich1 >= 2 && soquandich3 >= 2) || (soquandich1 >= 2 && soquandich4 >= 2) || (soquandich2 >= 2 && soquandich3 >= 2) || (soquandich2 >= 2 && soquandich4 >= 2) || (soquandich3 >= 2 && soquandich4 >= 2))
            {
                diemtong += MangDiemPhongNgu[2];
            }
            return diemtong;
        }
        private long DiemPN_kiemtrachan2dau(int dong, int cot)
        {
            long diemtong = 0;
            if (dong - 6 >= 0 && Matrix[dong - 1][cot].BackgroundImage == player[1].Mark && Matrix[dong - 2][cot].BackgroundImage == player[1].Mark && Matrix[dong - 3][cot].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong - 4][cot].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong - 5][cot].BackgroundImage == null && Matrix[dong - 6][cot].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong - 4][cot].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (dong + 6 < Cons.so_dong && Matrix[dong + 1][cot].BackgroundImage == player[1].Mark && Matrix[dong + 2][cot].BackgroundImage == player[1].Mark && Matrix[dong + 3][cot].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong + 4][cot].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong + 5][cot].BackgroundImage == null && Matrix[dong + 6][cot].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong + 4][cot].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (cot - 6 >= 0 && Matrix[dong][cot - 1].BackgroundImage == player[1].Mark && Matrix[dong][cot - 2].BackgroundImage == player[1].Mark && Matrix[dong][cot - 3].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong][cot - 4].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong][cot - 5].BackgroundImage == null && Matrix[dong][cot - 6].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong][cot - 4].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (cot + 6 < Cons.so_cot && Matrix[dong][cot + 1].BackgroundImage == player[1].Mark && Matrix[dong][cot + 2].BackgroundImage == player[1].Mark && Matrix[dong][cot + 3].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong][cot + 4].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong][cot + 5].BackgroundImage == null && Matrix[dong][cot + 6].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong][cot + 4].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (cot + 6 < Cons.so_cot && dong + 6 < Cons.so_dong && Matrix[dong + 1][cot + 1].BackgroundImage == player[1].Mark && Matrix[dong + 2][cot + 2].BackgroundImage == player[1].Mark && Matrix[dong + 3][cot + 3].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong + 4][cot + 4].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong + 5][cot + 5].BackgroundImage == null && Matrix[dong + 6][cot + 6].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong + 4][cot + 4].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (cot - 6 >= 0 && dong - 6 >= 0 && Matrix[dong - 1][cot - 1].BackgroundImage == player[1].Mark && Matrix[dong - 2][cot - 2].BackgroundImage == player[1].Mark && Matrix[dong - 3][cot - 3].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong - 4][cot - 4].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong - 5][cot - 5].BackgroundImage == null && Matrix[dong - 6][cot - 6].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong - 4][cot - 4].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (cot - 6 >= 0 && dong + 6 < Cons.so_dong && Matrix[dong + 1][cot - 1].BackgroundImage == player[1].Mark && Matrix[dong + 2][cot - 2].BackgroundImage == player[1].Mark && Matrix[dong + 3][cot - 3].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong + 4][cot - 4].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong + 5][cot - 5].BackgroundImage == null && Matrix[dong + 6][cot - 6].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong + 4][cot - 4].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            if (cot + 6 < Cons.so_dong && dong - 6 >= 0 && Matrix[dong - 1][cot + 1].BackgroundImage == player[1].Mark && Matrix[dong - 2][cot + 2].BackgroundImage == player[1].Mark && Matrix[dong - 3][cot + 3].BackgroundImage == player[1].Mark)
            {
                if (Matrix[dong - 4][cot + 4].BackgroundImage == player[1].Mark)
                {
                    if (Matrix[dong - 5][cot + 5].BackgroundImage == null && Matrix[dong - 6][cot + 6].BackgroundImage == player[0].Mark)
                    {
                        diemtong += MangDiemPhongNgu[4];
                    }
                }
                else
                {
                    if (Matrix[dong - 4][cot + 4].BackgroundImage == null)
                    {
                        diemtong += MangDiemPhongNgu[3];
                    }
                }
            }
            return diemtong;
        }
        #region timnuocdi_kho
        private Point timkiemnuocdi_kho()
        {
            Point point = new Point();
            long _Diem_Max = 0;
            for (int i = 0; i < Cons.so_dong; i++)
            {
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    if (Matrix[i][j].BackgroundImage == null)
                    {
                        long DiemTanCong = DiemTC_DuyetDoc2(i, j) + DiemTC_DuyetNgang2(i, j) + DiemTC_DuyetCheoTang2(i, j) + DiemTC_DuyetCheoGiam2(i, j) + DiemTC_kiemtradacbiet(i, j) + DiemTC_kiemtrachan2dau(i, j);
                        long DiemPhongNgu = DiemPN_DuyetDoc2(i, j) + DiemPN_DuyetNgang2(i, j) + DiemPN_DuyetCheoTang2(i, j) + DiemPN_DuyetCheoGiam2(i, j) + DiemPN_kiemtradacbiet(i, j) + DiemPN_kiemtrachan2dau(i, j);
                        long diemtam = DiemTanCong > DiemPhongNgu ? DiemTanCong : DiemPhongNgu;
                        long Diem_Tong = (DiemPhongNgu + DiemTanCong) > diemtam ? (DiemPhongNgu + DiemTanCong) : diemtam;
                        if (_Diem_Max < Diem_Tong)
                        {
                            _Diem_Max = Diem_Tong;
                            point = new Point(i, j);
                        }
                    }
                }
            }
            return point;
        }
        private long[] MangDiemTanCong2 = new long[6] { 0, 64, 4096, 262144, 16777216, 1073741824 };
        private long[] MangDiemPhongNgu2 = new long[6] { 0, 8, 512, 32768, 2097152, 134217728 };
        private long DiemTC_DuyetDoc2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            for (int dem = 1; dem < 5 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && dong + dem2 < Cons.so_dong; dem2++)
                        if (Matrix[dong + dem2][cot].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong + dem2][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && dong - dem2 >= 0; dem2++)
                        if (Matrix[dong - dem2][cot].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong - dem2][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquandich == 2)
                return 0;
            if (soquandich == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta];
            if (soquandich2 == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta2] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta2];
            if (soquanta >= soquanta2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquanta == 4)
                _Diem_Tong *= 2;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            return _Diem_Tong;
        }
        private long DiemTC_DuyetNgang2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            for (int dem = 1; dem < 5 && cot + dem < Cons.so_cot; dem++)
            {
                if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot + dem2 < Cons.so_cot; dem2++)
                        if (Matrix[dong][cot + dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong][cot + dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && cot - dem >= 0; dem++)
            {
                if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0; dem2++)
                        if (Matrix[dong][cot - dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong][cot - dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquandich == 2)
                return 0;
            if (soquandich == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta];
            if (soquandich2 == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta2] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta2];
            if (soquanta >= soquanta2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquanta == 4)
                _Diem_Tong *= 2;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            return _Diem_Tong;
        }
        private long DiemTC_DuyetCheoGiam2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            if (dong + 1 < Cons.so_dong && cot + 1 < Cons.so_cot && Matrix[dong + 1][cot + 1].BackgroundImage == player[0].Mark)
            {

            }
            if (dong > 0 && cot > 0 && Matrix[dong - 1][cot - 1].BackgroundImage == player[0].Mark)
            {

            }
            //
            for (int dem = 1; dem < 5 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot + dem2 < Cons.so_cot && dong + dem2 < Cons.so_dong; dem2++)
                        if (Matrix[dong + dem2][cot + dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong + dem2][cot + dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && cot - dem >= 0 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0 && dong - dem2 >= 0; dem2++)
                        if (Matrix[dong - dem2][cot - dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong - dem2][cot - dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquandich == 2)
                return 0;
            if (soquandich == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta];
            if (soquandich2 == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta2] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta2];
            if (soquanta >= soquanta2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;

            if (soquanta == 4)
                _Diem_Tong *= 2;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            return _Diem_Tong;
        }
        private long DiemTC_DuyetCheoTang2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            if (dong > 0 && cot + 1 < Cons.so_cot && Matrix[dong - 1][cot + 1].BackgroundImage == player[0].Mark)
            {

            }
            if (dong + 1 < Cons.so_dong && cot > 0 && Matrix[dong + 1][cot - 1].BackgroundImage == player[0].Mark)
            {

            }
            //
            for (int dem = 1; dem < 5 && cot + dem < Cons.so_cot && dong - dem > 0; dem++)
            {
                if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot + dem2 < Cons.so_cot && dong - dem2 > 0; dem2++)
                        if (Matrix[dong - dem2][cot + dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong - dem2][cot + dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                }
                else if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                    break;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 1; dem2 < 6 && cot - dem2 >= 0 && dong + dem2 < Cons.so_dong; dem2++)
                        if (Matrix[dong + dem2][cot - dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                        }
                        else if (Matrix[dong + dem2][cot - dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                            break;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquandich == 2)
                return 0;
            if (soquandich == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta];
            if (soquandich2 == 0)
                _Diem_Tong += MangDiemTanCong2[soquanta2] * 2;
            else
                _Diem_Tong += MangDiemTanCong2[soquanta2];
            if (soquanta >= soquanta2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquanta == 4)
                _Diem_Tong *= 2;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            return _Diem_Tong;
        }
        private long DiemPN_DuyetDoc2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            for (int dem = 1; dem < 5 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else
                {
                    if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                    {
                        soquandich++;
                    }
                    else // BackgroundImage = 0
                    {
                        for (int dem2 = 2; dem2 < 6 && dong + dem2 < Cons.so_dong; dem2++)
                        {
                            if (Matrix[dong + dem][cot].BackgroundImage == player[0].Mark)
                            {
                                soquanta2++;
                                break;
                            }
                            else
                                if (Matrix[dong + dem][cot].BackgroundImage == player[1].Mark)
                                {
                                    soquandich2++;
                                }
                                else
                                    break;
                        }
                        break;
                    }
                }
            }
            for (int dem = 1; dem < 5 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong - dem][cot].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && dong - dem2 >= 0; dem2++)
                        if (Matrix[dong - dem2][cot].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong - dem2][cot].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquanta == 2)
                return 0;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            /* 
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            */
            if (soquandich >= soquandich2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquandich == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        private long DiemPN_DuyetNgang2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            for (int dem = 1; dem < 5 && cot + dem < Cons.so_cot; dem++)
            {
                if (Matrix[dong][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong][cot + dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot + dem2 < Cons.so_cot; dem2++)
                        if (Matrix[dong][cot + dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong][cot + dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && cot - dem >= 0; dem++)
            {
                if (Matrix[dong][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong][cot - dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0; dem2++)
                        if (Matrix[dong][cot - dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong][cot - dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else break;
                    break;
                }
            }
            if (soquanta == 2)
                return 0;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            /* 
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            */
            if (soquandich >= soquandich2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquandich == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        private long DiemPN_DuyetCheoGiam2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            for (int dem = 1; dem < 5 && cot + dem < Cons.so_cot && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong + dem][cot + dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && dong + dem2 < Cons.so_dong && cot + dem2 < Cons.so_cot; dem2++)
                        if (Matrix[dong + dem2][cot + dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong + dem2][cot + dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && cot - dem >= 0 && dong - dem >= 0; dem++)
            {
                if (Matrix[dong - dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong - dem][cot - dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && cot - dem2 >= 0 && dong - dem2 >= 0; dem2++)
                        if (Matrix[dong - dem2][cot - dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong - dem2][cot - dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquanta == 2)
                return 0;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            /* 
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            */
            if (soquandich >= soquandich2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquandich == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        private long DiemPN_DuyetCheoTang2(int dong, int cot)
        {
            long _Diem_Tong = 0;
            int soquanta = 0;
            int soquandich = 0;
            int soquanta2 = 0;
            int soquandich2 = 0;
            for (int dem = 1; dem < 5 && cot + dem < Cons.so_cot && dong - dem > 0; dem++)
            {
                if (Matrix[dong - dem][cot + dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong - dem][cot + dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && dong - dem2 >= 0 && cot + dem2 < Cons.so_cot; dem2++)
                        if (Matrix[dong - dem2][cot + dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong - dem2][cot + dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else
                            break;
                    break;
                }
            }
            for (int dem = 1; dem < 5 && cot - dem >= 0 && dong + dem < Cons.so_dong; dem++)
            {
                if (Matrix[dong + dem][cot - dem].BackgroundImage == player[0].Mark)
                {
                    soquanta++;
                    break;
                }
                else if (Matrix[dong + dem][cot - dem].BackgroundImage == player[1].Mark)
                {
                    soquandich++;
                }
                else // BackgroundImage = 0
                {
                    for (int dem2 = 2; dem2 < 6 && dong + dem2 < Cons.so_dong && cot - dem2 >= 0; dem2++)
                        if (Matrix[dong + dem2][cot - dem2].BackgroundImage == player[0].Mark)
                        {
                            soquanta2++;
                            break;
                        }
                        else if (Matrix[dong + dem2][cot - dem2].BackgroundImage == player[1].Mark)
                        {
                            soquandich2++;
                        }
                        else
                            break;
                    break;
                }
            }
            if (soquanta == 2)
                return 0;
            if (soquanta == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich];
            /* 
            if (soquanta2 == 0)
                _Diem_Tong += MangDiemPhongNgu2[soquandich2] * 2;
            else
                _Diem_Tong += MangDiemPhongNgu2[soquandich2];
            */
            if (soquandich >= soquandich2)
                _Diem_Tong -= 1;
            else
                _Diem_Tong -= 2;
            if (soquandich == 4)
                _Diem_Tong *= 2;
            return _Diem_Tong;
        }
        #endregion
        #endregion
        private string ten = "";
        private int[,] mangluu = new int[Cons.so_cot, Cons.so_dong];
        private bool is_savegame = false;
        public void savegame()
        {

            for (int i = 0; i < Cons.so_dong; i++)
            {
                for (int j = 0; j < Cons.so_cot; j++)
                {
                    if (Matrix[i][j].BackgroundImage == null)
                    {
                        mangluu[i, j] = 2;
                    }
                    if (Matrix[i][j].BackgroundImage == player[0].Mark)
                    {
                        mangluu[i, j] = 0;
                    }
                    if (Matrix[i][j].BackgroundImage == player[1].Mark)
                    {
                        mangluu[i, j] = 1;
                    }
                }
            }
            if (Capdo.Text == "dễ")
            {
                ten = string.Concat(player1.Text, "de.txt");
            }
            if (Capdo.Text == "trung bình")
            {
                ten = string.Concat(player1.Text, "trungbinh.txt");
            }
            if (Capdo.Text == "khó")
            {
                ten = string.Concat(player1.Text, "kho.txt");
            }
            try
            {
                is_savegame = true;
                using (StreamWriter sw = new StreamWriter(ten))
                {

                    foreach (int s in mangluu)
                    {
                        sw.WriteLine(s);
                    }
                }
            }catch { is_savegame = false; }
            if(is_savegame==true)
            {
                MessageBox.Show("lưu thành công ...!");
            }
            else
            {
                MessageBox.Show("lưu thất bại ...!");
            }
        }
        public void loadgame()
        {
            if (capdo.Text == "dễ")
            {
                ten = string.Concat(player1.Text, "de.txt");
            }
            if (capdo.Text == "trung bình")
            {
                ten = string.Concat(player1.Text, "trungbinh.txt");
            }
            if (capdo.Text == "khó")
            {
                ten = string.Concat(player1.Text, "kho.txt");
            }
            try
            {
                using (StreamReader sr = new StreamReader(ten))
                {
                    int i = 0, j = 0;
                    int  line;
                    while ((line = int.Parse(sr.ReadLine())) != null)
                    {
                        if(line==0)
                        {
                            Matrix[i][j].BackgroundImage = player[0].Mark;
                        }
                        if (line== 1)
                        {
                            Matrix[i][j].BackgroundImage = player[1].Mark;
                        }
                        if (line == 2)
                        {
                            Matrix[i][j].BackgroundImage = null;
                        }
                        j++;
                        if(j>=Cons.so_dong)
                        {
                            i++;
                            j = 0;
                        }
                    }
                }
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(" chương trình bị lỗi ! không thể load game .....");
            }
        }
    }
    public class buttonClickevent : EventArgs
    {
        private Point clickpoint;

        public Point Clickpoint
        {
            get
            {
                return clickpoint;
            }

            set
            {
                clickpoint = value;
            }
        }
        public buttonClickevent(Point point)
        {
            this.Clickpoint = point;
        }
    }
}
