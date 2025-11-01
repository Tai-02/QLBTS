namespace QLBTS_GUI
{
    partial class Admin
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
            ADMIN_pn_tab = new Panel();
            pictureBox2 = new PictureBox();
            ADMIN_pn_menu = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            ADMIN_lb_hieusuat = new Label();
            ADMIN_lb_doanhthu = new Label();
            ADMIN_lb_qltknv = new Label();
            btn_dangxuat = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ADMIN_pn_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // ADMIN_pn_tab
            // 
            ADMIN_pn_tab.BackColor = SystemColors.Control;
            ADMIN_pn_tab.Location = new Point(1, 143);
            ADMIN_pn_tab.Name = "ADMIN_pn_tab";
            ADMIN_pn_tab.Size = new Size(980, 508);
            ADMIN_pn_tab.TabIndex = 3;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Properties.Resources.icon_momocha;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(370, -24);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 200);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // ADMIN_pn_menu
            // 
            ADMIN_pn_menu.BackColor = Color.Transparent;
            ADMIN_pn_menu.BorderRadius = 30;
            ADMIN_pn_menu.Controls.Add(pictureBox1);
            ADMIN_pn_menu.Controls.Add(ADMIN_lb_hieusuat);
            ADMIN_pn_menu.Controls.Add(ADMIN_lb_doanhthu);
            ADMIN_pn_menu.Controls.Add(ADMIN_lb_qltknv);
            ADMIN_pn_menu.CustomizableEdges = customizableEdges1;
            ADMIN_pn_menu.FillColor = Color.Black;
            ADMIN_pn_menu.Location = new Point(12, 34);
            ADMIN_pn_menu.Name = "ADMIN_pn_menu";
            ADMIN_pn_menu.ShadowDecoration.CustomizableEdges = customizableEdges2;
            ADMIN_pn_menu.Size = new Size(942, 68);
            ADMIN_pn_menu.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.icon_momocha;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(358, -58);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // ADMIN_lb_hieusuat
            // 
            ADMIN_lb_hieusuat.AutoSize = true;
            ADMIN_lb_hieusuat.BackColor = Color.Black;
            ADMIN_lb_hieusuat.Font = new Font("Segoe UI", 9F);
            ADMIN_lb_hieusuat.ForeColor = Color.White;
            ADMIN_lb_hieusuat.Location = new Point(779, 22);
            ADMIN_lb_hieusuat.Name = "ADMIN_lb_hieusuat";
            ADMIN_lb_hieusuat.Size = new Size(71, 20);
            ADMIN_lb_hieusuat.TabIndex = 6;
            ADMIN_lb_hieusuat.Text = "Hiệu suất";
            ADMIN_lb_hieusuat.Click += ADMIN_lb_hieusuat_Click;
            // 
            // ADMIN_lb_doanhthu
            // 
            ADMIN_lb_doanhthu.AutoSize = true;
            ADMIN_lb_doanhthu.BackColor = Color.Black;
            ADMIN_lb_doanhthu.Font = new Font("Segoe UI", 9F);
            ADMIN_lb_doanhthu.ForeColor = Color.White;
            ADMIN_lb_doanhthu.Location = new Point(622, 22);
            ADMIN_lb_doanhthu.Name = "ADMIN_lb_doanhthu";
            ADMIN_lb_doanhthu.Size = new Size(78, 20);
            ADMIN_lb_doanhthu.TabIndex = 4;
            ADMIN_lb_doanhthu.Text = "Doanh thu";
            ADMIN_lb_doanhthu.Click += ADMIN_lb_doanhthu_Click;
            // 
            // ADMIN_lb_qltknv
            // 
            ADMIN_lb_qltknv.AutoSize = true;
            ADMIN_lb_qltknv.BackColor = Color.Black;
            ADMIN_lb_qltknv.Font = new Font("Segoe UI", 9F);
            ADMIN_lb_qltknv.ForeColor = Color.White;
            ADMIN_lb_qltknv.Location = new Point(99, 22);
            ADMIN_lb_qltknv.Name = "ADMIN_lb_qltknv";
            ADMIN_lb_qltknv.Size = new Size(188, 20);
            ADMIN_lb_qltknv.TabIndex = 2;
            ADMIN_lb_qltknv.Text = "Quản lí tài khoản nhân viên";
            ADMIN_lb_qltknv.Click += ADMIN_lb_qltknv_Click;
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
            btn_dangxuat.Location = new Point(1, 108);
            btn_dangxuat.Name = "btn_dangxuat";
            btn_dangxuat.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_dangxuat.Size = new Size(50, 33);
            btn_dangxuat.TabIndex = 1;
            btn_dangxuat.Text = " 🔙";
            btn_dangxuat.Click += btn_dangxuat_Click;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(btn_dangxuat);
            Controls.Add(ADMIN_pn_menu);
            Controls.Add(ADMIN_pn_tab);
            Controls.Add(pictureBox2);
            Name = "Admin";
            Text = "NVGIAO";
            Load += NVGIAO_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ADMIN_pn_menu.ResumeLayout(false);
            ADMIN_pn_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel ADMIN_pn_menu;
        private PictureBox pictureBox1;
        private Label ADMIN_lb_qltknv;
        private PictureBox pictureBox2;
        public static Panel ADMIN_pn_tab;
        private Label ADMIN_lb_hieusuat;
        private Label ADMIN_lb_doanhthu;
        private Guna.UI2.WinForms.Guna2Button btn_dangxuat;
    }
}