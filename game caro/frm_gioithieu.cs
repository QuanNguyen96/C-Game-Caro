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
    public partial class frm_gioithieu : Form
    {
        public frm_gioithieu()
        {
            InitializeComponent();
        }

        private void frm_gioithieu_Load(object sender, EventArgs e)
        {
            label1.Text = "Game Caro là 1 game đơn giản nhưng nó là 1 trong những game cơ bản thể hiện được rõ nhất trí tuệ nhân tạo của con người đối với  trong việc áp dụng khoa học máy tính . \nGame Caro sẽ giúp các bạn giải trí và thư giãn đầu óc những lúc căng thẳng";
            label2.Text = "CopyRight by nhóm 82 :Nguyễn Văn Quân ,Nguyễn Xuân Khoa ,Ninh Ngọc Luyên , Khoa toán ứng dụng và tin học đh bách khoa hà nội \nĐịa chỉ email : quan96kun@gmail.com";
        }
        
    }
}
