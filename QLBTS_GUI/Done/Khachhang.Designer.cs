namespace QLBTS_GUI
{
    partial class Khachhang
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            KH_pn_menu = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            KH_lb_ls = new Label();
            KH_lb_ttcn = new Label();
            KH_lb_ttd = new Label();
            KH_lb_gh = new Label();
            KH_lb_sp = new Label();
            KH_lb_Trangchu = new Label();
            pictureBox2 = new PictureBox();
            KH_pn_tab = new Panel();
            btn_dangxuat = new Guna.UI2.WinForms.Guna2Button();
            KH_pn_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // KH_pn_menu
            // 
            KH_pn_menu.BackColor = Color.Transparent;
            KH_pn_menu.BorderRadius = 30;
            KH_pn_menu.Controls.Add(pictureBox1);
            KH_pn_menu.Controls.Add(KH_lb_ls);
            KH_pn_menu.Controls.Add(KH_lb_ttcn);
            KH_pn_menu.Controls.Add(KH_lb_ttd);
            KH_pn_menu.Controls.Add(KH_lb_gh);
            KH_pn_menu.Controls.Add(KH_lb_sp);
            KH_pn_menu.Controls.Add(KH_lb_Trangchu);
            KH_pn_menu.CustomizableEdges = customizableEdges1;
            KH_pn_menu.FillColor = Color.Black;
            KH_pn_menu.Location = new Point(12, 35);
            KH_pn_menu.Name = "KH_pn_menu";
            KH_pn_menu.ShadowDecoration.CustomizableEdges = customizableEdges2;
            KH_pn_menu.Size = new Size(942, 68);
            KH_pn_menu.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.icon_momocha;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(356, -60);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // KH_lb_ls
            // 
            KH_lb_ls.AutoSize = true;
            KH_lb_ls.BackColor = Color.Black;
            KH_lb_ls.Font = new Font("Segoe UI", 10.2F);
            KH_lb_ls.ForeColor = Color.White;
            KH_lb_ls.Location = new Point(851, 22);
            KH_lb_ls.Name = "KH_lb_ls";
            KH_lb_ls.Size = new Size(62, 23);
            KH_lb_ls.TabIndex = 6;
            KH_lb_ls.Text = "Lịch sử";
            KH_lb_ls.Click += KH_lb_ls_Click;
            // 
            // KH_lb_ttcn
            // 
            KH_lb_ttcn.AutoSize = true;
            KH_lb_ttcn.BackColor = Color.Black;
            KH_lb_ttcn.Font = new Font("Segoe UI", 10.2F);
            KH_lb_ttcn.ForeColor = Color.White;
            KH_lb_ttcn.Location = new Point(685, 22);
            KH_lb_ttcn.Name = "KH_lb_ttcn";
            KH_lb_ttcn.Size = new Size(150, 23);
            KH_lb_ttcn.TabIndex = 5;
            KH_lb_ttcn.Text = "Thông tin cá nhân";
            KH_lb_ttcn.Click += KH_lb_ttcn_Click;
            // 
            // KH_lb_ttd
            // 
            KH_lb_ttd.AutoSize = true;
            KH_lb_ttd.BackColor = Color.Black;
            KH_lb_ttd.Font = new Font("Segoe UI", 10.2F);
            KH_lb_ttd.ForeColor = Color.White;
            KH_lb_ttd.Location = new Point(557, 22);
            KH_lb_ttd.Name = "KH_lb_ttd";
            KH_lb_ttd.Size = new Size(122, 23);
            KH_lb_ttd.TabIndex = 4;
            KH_lb_ttd.Text = "Trạng thái đơn";
            KH_lb_ttd.Click += KH_lb_ttd_Click;
            // 
            // KH_lb_gh
            // 
            KH_lb_gh.AutoSize = true;
            KH_lb_gh.BackColor = Color.Black;
            KH_lb_gh.Font = new Font("Segoe UI", 10.2F);
            KH_lb_gh.ForeColor = Color.White;
            KH_lb_gh.Location = new Point(244, 22);
            KH_lb_gh.Name = "KH_lb_gh";
            KH_lb_gh.Size = new Size(108, 23);
            KH_lb_gh.TabIndex = 3;
            KH_lb_gh.Text = "Giỏ hàng \U0001f6d2";
            KH_lb_gh.Click += KH_lb_gh_Click;
            // 
            // KH_lb_sp
            // 
            KH_lb_sp.AutoSize = true;
            KH_lb_sp.BackColor = Color.Black;
            KH_lb_sp.Font = new Font("Segoe UI", 10.2F);
            KH_lb_sp.ForeColor = Color.White;
            KH_lb_sp.Location = new Point(132, 22);
            KH_lb_sp.Name = "KH_lb_sp";
            KH_lb_sp.Size = new Size(87, 23);
            KH_lb_sp.TabIndex = 2;
            KH_lb_sp.Text = "Sản phẩm";
            KH_lb_sp.Click += KH_lb_sp_Click;
            // 
            // KH_lb_Trangchu
            // 
            KH_lb_Trangchu.AutoSize = true;
            KH_lb_Trangchu.BackColor = Color.Black;
            KH_lb_Trangchu.Font = new Font("Segoe UI", 10.2F);
            KH_lb_Trangchu.ForeColor = Color.White;
            KH_lb_Trangchu.Location = new Point(25, 22);
            KH_lb_Trangchu.Name = "KH_lb_Trangchu";
            KH_lb_Trangchu.Size = new Size(86, 23);
            KH_lb_Trangchu.TabIndex = 1;
            KH_lb_Trangchu.Text = "Trang chủ";
            KH_lb_Trangchu.Click += KH_lb_Trangchu_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Properties.Resources.icon_momocha;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(368, -25);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 200);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // KH_pn_tab
            // 
            KH_pn_tab.BackColor = SystemColors.Control;
            KH_pn_tab.Location = new Point(1, 143);
            KH_pn_tab.Name = "KH_pn_tab";
            KH_pn_tab.Size = new Size(980, 508);
            KH_pn_tab.TabIndex = 1;
            // 
            // btn_dangxuat
            // 
            btn_dangxuat.CustomizableEdges = customizableEdges3;
            btn_dangxuat.DisabledState.BorderColor = Color.DarkGray;
            btn_dangxuat.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_dangxuat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_dangxuat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_dangxuat.FillColor = Color.Silver;
            btn_dangxuat.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_dangxuat.ForeColor = Color.White;
            btn_dangxuat.Location = new Point(1, 109);
            btn_dangxuat.Name = "btn_dangxuat";
            btn_dangxuat.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_dangxuat.Size = new Size(50, 33);
            btn_dangxuat.TabIndex = 0;
            btn_dangxuat.Text = " 🔙";
            btn_dangxuat.Click += btn_dangxuat_Click;
            // 
            // Khachhang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(btn_dangxuat);
            Controls.Add(KH_pn_tab);
            Controls.Add(KH_pn_menu);
            Controls.Add(pictureBox2);
            Name = "Khachhang";
            Text = "Khachhang";
            Load += Khachhang_Load;
            KH_pn_menu.ResumeLayout(false);
            KH_pn_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel KH_pn_menu;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label KH_lb_ls;
        private Label KH_lb_ttcn;
        private Label KH_lb_ttd;
        private Label KH_lb_gh;
        private Label KH_lb_sp;
        private Label KH_lb_Trangchu;
        private Guna.UI2.WinForms.Guna2Button btn_dangxuat;
        public static Panel KH_pn_tab;
    }
}