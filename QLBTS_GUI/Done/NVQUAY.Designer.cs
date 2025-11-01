using QLBTS_GUI.Properties;

namespace QLBTS_GUI
{
    partial class NVQUAY
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            NVQ_pn_tab = new Panel();
            NVQ_pn_menu = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            NVQ_lb_ls = new Label();
            NVQ_lb_qldh = new Label();
            NVQ_lb_qlsp = new Label();
            NVQ_lb_gh = new Label();
            NVQ_lb_sp = new Label();
            pictureBox2 = new PictureBox();
            NVQ_pn_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // NVQ_pn_tab
            // 
            NVQ_pn_tab.BackColor = SystemColors.Control;
            NVQ_pn_tab.Location = new Point(1, 143);
            NVQ_pn_tab.Name = "NVQ_pn_tab";
            NVQ_pn_tab.Size = new Size(980, 508);
            NVQ_pn_tab.TabIndex = 2;
            // 
            // NVQ_pn_menu
            // 
            NVQ_pn_menu.BackColor = Color.Transparent;
            NVQ_pn_menu.BorderRadius = 30;
            NVQ_pn_menu.Controls.Add(pictureBox1);
            NVQ_pn_menu.Controls.Add(NVQ_lb_ls);
            NVQ_pn_menu.Controls.Add(NVQ_lb_qldh);
            NVQ_pn_menu.Controls.Add(NVQ_lb_qlsp);
            NVQ_pn_menu.Controls.Add(NVQ_lb_gh);
            NVQ_pn_menu.Controls.Add(NVQ_lb_sp);
            NVQ_pn_menu.CustomizableEdges = customizableEdges3;
            NVQ_pn_menu.FillColor = Color.Black;
            NVQ_pn_menu.Location = new Point(13, 31);
            NVQ_pn_menu.Name = "NVQ_pn_menu";
            NVQ_pn_menu.ShadowDecoration.CustomizableEdges = customizableEdges4;
            NVQ_pn_menu.Size = new Size(942, 68);
            NVQ_pn_menu.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Resources.icon_momocha;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(358, -58);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // NVQ_lb_ls
            // 
            NVQ_lb_ls.AutoSize = true;
            NVQ_lb_ls.BackColor = Color.Black;
            NVQ_lb_ls.Font = new Font("Segoe UI", 9F);
            NVQ_lb_ls.ForeColor = Color.White;
            NVQ_lb_ls.Location = new Point(851, 22);
            NVQ_lb_ls.Name = "NVQ_lb_ls";
            NVQ_lb_ls.Size = new Size(54, 20);
            NVQ_lb_ls.TabIndex = 6;
            NVQ_lb_ls.Text = "Lịch sử";
            NVQ_lb_ls.Click += NVQ_lb_ls_Click;
            // 
            // NVQ_lb_qldh
            // 
            NVQ_lb_qldh.AutoSize = true;
            NVQ_lb_qldh.BackColor = Color.Black;
            NVQ_lb_qldh.Font = new Font("Segoe UI", 9F);
            NVQ_lb_qldh.ForeColor = Color.White;
            NVQ_lb_qldh.Location = new Point(703, 22);
            NVQ_lb_qldh.Name = "NVQ_lb_qldh";
            NVQ_lb_qldh.Size = new Size(123, 20);
            NVQ_lb_qldh.TabIndex = 5;
            NVQ_lb_qldh.Text = "Quản lí đơn hàng";
            NVQ_lb_qldh.Click += NVQ_lb_qldh_Click;
            // 
            // NVQ_lb_qlsp
            // 
            NVQ_lb_qlsp.AutoSize = true;
            NVQ_lb_qlsp.BackColor = Color.Black;
            NVQ_lb_qlsp.Font = new Font("Segoe UI", 9F);
            NVQ_lb_qlsp.ForeColor = Color.White;
            NVQ_lb_qlsp.Location = new Point(557, 22);
            NVQ_lb_qlsp.Name = "NVQ_lb_qlsp";
            NVQ_lb_qlsp.Size = new Size(124, 20);
            NVQ_lb_qlsp.TabIndex = 4;
            NVQ_lb_qlsp.Text = "Quản lí sản phẩm";
            NVQ_lb_qlsp.Click += NVQ_lb_qlsp_Click;
            // 
            // NVQ_lb_gh
            // 
            NVQ_lb_gh.AutoSize = true;
            NVQ_lb_gh.BackColor = Color.Black;
            NVQ_lb_gh.Font = new Font("Segoe UI", 9F);
            NVQ_lb_gh.ForeColor = Color.White;
            NVQ_lb_gh.Location = new Point(223, 22);
            NVQ_lb_gh.Name = "NVQ_lb_gh";
            NVQ_lb_gh.Size = new Size(94, 20);
            NVQ_lb_gh.TabIndex = 3;
            NVQ_lb_gh.Text = "Giỏ hàng \U0001f6d2";
            NVQ_lb_gh.Click += NVQ_lb_gh_Click;
            // 
            // NVQ_lb_sp
            // 
            NVQ_lb_sp.AutoSize = true;
            NVQ_lb_sp.BackColor = Color.Black;
            NVQ_lb_sp.Font = new Font("Segoe UI", 9F);
            NVQ_lb_sp.ForeColor = Color.White;
            NVQ_lb_sp.Location = new Point(71, 22);
            NVQ_lb_sp.Name = "NVQ_lb_sp";
            NVQ_lb_sp.Size = new Size(75, 20);
            NVQ_lb_sp.TabIndex = 2;
            NVQ_lb_sp.Text = "Sản phẩm";
            NVQ_lb_sp.Click += NVQ_lb_sp_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Resources.icon_momocha;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(371, -26);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(200, 200);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // NVQUAY
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(NVQ_pn_tab);
            Controls.Add(NVQ_pn_menu);
            Controls.Add(pictureBox2);
            Name = "NVQUAY";
            Load += NVQUAY_Load;
            NVQ_pn_menu.ResumeLayout(false);
            NVQ_pn_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Panel NVQ_pn_menu;
        private Label NVQ_lb_ls;
        private Label NVQ_lb_qldh;
        private Label NVQ_lb_qlsp;
        private Label NVQ_lb_gh;
        private Label NVQ_lb_sp;
        private PictureBox pictureBox2;
        public static Panel NVQ_pn_tab;
    }
}