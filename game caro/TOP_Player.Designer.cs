namespace game_caro
{
    partial class TOP_Player
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hiểnThịTấtCảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_xephang = new System.Windows.Forms.Button();
            this.txt_xephang = new System.Windows.Forms.TextBox();
            this.txt_timkiem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thua = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ten = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.username,
            this.thang,
            this.thua,
            this.diem,
            this.ten});
            this.dataGridView1.Location = new System.Drawing.Point(3, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(641, 411);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(476, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "tìm kiếm người chơi";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hiểnThịTấtCảToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(647, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hiểnThịTấtCảToolStripMenuItem
            // 
            this.hiểnThịTấtCảToolStripMenuItem.Name = "hiểnThịTấtCảToolStripMenuItem";
            this.hiểnThịTấtCảToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.hiểnThịTấtCảToolStripMenuItem.Text = "hiển thị tất cả";
            this.hiểnThịTấtCảToolStripMenuItem.Click += new System.EventHandler(this.hiểnThịTấtCảToolStripMenuItem_Click);
            // 
            // btn_xephang
            // 
            this.btn_xephang.Location = new System.Drawing.Point(146, 27);
            this.btn_xephang.Name = "btn_xephang";
            this.btn_xephang.Size = new System.Drawing.Size(93, 23);
            this.btn_xephang.TabIndex = 4;
            this.btn_xephang.Text = "tìm kiếm top";
            this.btn_xephang.UseVisualStyleBackColor = true;
            this.btn_xephang.Click += new System.EventHandler(this.btn_xephang_Click);
            // 
            // txt_xephang
            // 
            this.txt_xephang.Location = new System.Drawing.Point(3, 27);
            this.txt_xephang.Name = "txt_xephang";
            this.txt_xephang.Size = new System.Drawing.Size(137, 20);
            this.txt_xephang.TabIndex = 5;
            this.txt_xephang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.Location = new System.Drawing.Point(305, 29);
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.Size = new System.Drawing.Size(165, 20);
            this.txt_timkiem.TabIndex = 6;
            this.txt_timkiem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_timkiem.TextChanged += new System.EventHandler(this.txt_timkiem_TextChanged);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(305, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 20);
            this.label1.TabIndex = 7;
            // 
            // username
            // 
            this.username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.username.DataPropertyName = "username";
            this.username.HeaderText = "tên người chơi";
            this.username.Name = "username";
            // 
            // thang
            // 
            this.thang.DataPropertyName = "thang";
            this.thang.HeaderText = "số trận thắng";
            this.thang.Name = "thang";
            // 
            // thua
            // 
            this.thua.DataPropertyName = "thua";
            this.thua.HeaderText = "số trận thua";
            this.thua.Name = "thua";
            // 
            // diem
            // 
            this.diem.DataPropertyName = "diem";
            this.diem.HeaderText = "điểm";
            this.diem.Name = "diem";
            // 
            // ten
            // 
            this.ten.DataPropertyName = "ten";
            this.ten.HeaderText = "tên tài khoản";
            this.ten.Name = "ten";
            // 
            // TOP_Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(647, 488);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_timkiem);
            this.Controls.Add(this.txt_xephang);
            this.Controls.Add(this.btn_xephang);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TOP_Player";
            this.Text = "TOP_Player";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TOP_Player_FormClosed);
            this.Load += new System.EventHandler(this.TOP_Player_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hiểnThịTấtCảToolStripMenuItem;
        private System.Windows.Forms.Button btn_xephang;
        private System.Windows.Forms.TextBox txt_xephang;
        private System.Windows.Forms.TextBox txt_timkiem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn thang;
        private System.Windows.Forms.DataGridViewTextBoxColumn thua;
        private System.Windows.Forms.DataGridViewTextBoxColumn diem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ten;
    }
}