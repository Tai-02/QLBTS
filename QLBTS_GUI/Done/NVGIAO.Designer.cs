namespace QLBTS_GUI
{
    partial class NVGIAO
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
            NVG_pn_tab = new Panel();
            pictureBox2 = new PictureBox();
            NVG_pn_menu = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            NVG_lb_dagiao = new Label();
            NVG_lb_danhan = new Label();
            NVG_lb_choxacnhan = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            NVG_pn_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // NVG_pn_tab
            // 
            NVG_pn_tab.BackColor = SystemColors.Control;
            NVG_pn_tab.Location = new Point(1, 143);
            NVG_pn_tab.Name = "NVG_pn_tab";
            NVG_pn_tab.Size = new Size(980, 508);
            NVG_pn_tab.TabIndex = 3;
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
            // NVG_pn_menu
            // 
            NVG_pn_menu.BackColor = Color.Transparent;
            NVG_pn_menu.BorderRadius = 30;
            NVG_pn_menu.Controls.Add(pictureBox1);
            NVG_pn_menu.Controls.Add(NVG_lb_dagiao);
            NVG_pn_menu.Controls.Add(NVG_lb_danhan);
            NVG_pn_menu.Controls.Add(NVG_lb_choxacnhan);
            NVG_pn_menu.CustomizableEdges = customizableEdges1;
            NVG_pn_menu.FillColor = Color.Black;
            NVG_pn_menu.Location = new Point(12, 34);
            NVG_pn_menu.Name = "NVG_pn_menu";
            NVG_pn_menu.ShadowDecoration.CustomizableEdges = customizableEdges2;
            NVG_pn_menu.Size = new Size(942, 68);
            NVG_pn_menu.TabIndex = 4;
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
            // NVG_lb_dagiao
            // 
            NVG_lb_dagiao.AutoSize = true;
            NVG_lb_dagiao.BackColor = Color.Black;
            NVG_lb_dagiao.Font = new Font("Segoe UI", 9F);
            NVG_lb_dagiao.ForeColor = Color.White;
            NVG_lb_dagiao.Location = new Point(802, 22);
            NVG_lb_dagiao.Name = "NVG_lb_dagiao";
            NVG_lb_dagiao.Size = new Size(62, 20);
            NVG_lb_dagiao.TabIndex = 6;
            NVG_lb_dagiao.Text = "Đã giao";
            NVG_lb_dagiao.Click += NVG_lb_dagiao_Click;
            // 
            // NVG_lb_danhan
            // 
            NVG_lb_danhan.AutoSize = true;
            NVG_lb_danhan.BackColor = Color.Black;
            NVG_lb_danhan.Font = new Font("Segoe UI", 9F);
            NVG_lb_danhan.ForeColor = Color.White;
            NVG_lb_danhan.Location = new Point(650, 22);
            NVG_lb_danhan.Name = "NVG_lb_danhan";
            NVG_lb_danhan.Size = new Size(64, 20);
            NVG_lb_danhan.TabIndex = 4;
            NVG_lb_danhan.Text = "Đã nhận";
            NVG_lb_danhan.Click += NVG_lb_danhan_Click;
            // 
            // NVG_lb_choxacnhan
            // 
            NVG_lb_choxacnhan.AutoSize = true;
            NVG_lb_choxacnhan.BackColor = Color.Black;
            NVG_lb_choxacnhan.Font = new Font("Segoe UI", 9F);
            NVG_lb_choxacnhan.ForeColor = Color.White;
            NVG_lb_choxacnhan.Location = new Point(157, 22);
            NVG_lb_choxacnhan.Name = "NVG_lb_choxacnhan";
            NVG_lb_choxacnhan.Size = new Size(97, 20);
            NVG_lb_choxacnhan.TabIndex = 2;
            NVG_lb_choxacnhan.Text = "Chờ xác nhận";
            NVG_lb_choxacnhan.Click += NVG_lb_choxacnhan_Click;
            // 
            // NVGIAO
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 653);
            Controls.Add(NVG_pn_menu);
            Controls.Add(NVG_pn_tab);
            Controls.Add(pictureBox2);
            Name = "NVGIAO";
            Text = "NVGIAO";
            Load += NVGIAO_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            NVG_pn_menu.ResumeLayout(false);
            NVG_pn_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel NVG_pn_menu;
        private PictureBox pictureBox1;
        private Label NVG_lb_dagiao;
        private Label NVG_lb_danhan;
        private Label NVG_lb_choxacnhan;
        private PictureBox pictureBox2;
        public static Panel NVG_pn_tab;
    }
}