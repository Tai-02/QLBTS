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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            KH_pn_tab = new Panel();
            KH_pn_menu = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            pictureBox2 = new PictureBox();
            KH_pn_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // KH_pn_tab
            // 
            KH_pn_tab.BackColor = SystemColors.Control;
            KH_pn_tab.Location = new Point(1, 159);
            KH_pn_tab.Name = "KH_pn_tab";
            KH_pn_tab.Size = new Size(983, 500);
            KH_pn_tab.TabIndex = 2;
            // 
            // KH_pn_menu
            // 
            KH_pn_menu.BackColor = Color.Transparent;
            KH_pn_menu.BorderRadius = 30;
            KH_pn_menu.Controls.Add(pictureBox1);
            KH_pn_menu.Controls.Add(label6);
            KH_pn_menu.Controls.Add(label5);
            KH_pn_menu.Controls.Add(label4);
            KH_pn_menu.Controls.Add(label3);
            KH_pn_menu.Controls.Add(label2);
            KH_pn_menu.CustomizableEdges = customizableEdges1;
            KH_pn_menu.FillColor = Color.Black;
            KH_pn_menu.Location = new Point(12, 43);
            KH_pn_menu.Name = "KH_pn_menu";
            KH_pn_menu.ShadowDecoration.CustomizableEdges = customizableEdges2;
            KH_pn_menu.Size = new Size(942, 68);
            KH_pn_menu.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.BackgroundImage = Properties.Resources.icon_momocha;
            pictureBox1.Location = new Point(358, -58);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 200);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Black;
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.White;
            label6.Location = new Point(851, 22);
            label6.Name = "label6";
            label6.Size = new Size(54, 20);
            label6.TabIndex = 6;
            label6.Text = "Lịch sử";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Black;
            label5.Font = new Font("Segoe UI", 9F);
            label5.ForeColor = Color.White;
            label5.Location = new Point(703, 22);
            label5.Name = "label5";
            label5.Size = new Size(123, 20);
            label5.TabIndex = 5;
            label5.Text = "Quản lí đơn hàng";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Black;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(557, 22);
            label4.Name = "label4";
            label4.Size = new Size(124, 20);
            label4.TabIndex = 4;
            label4.Text = "Quản lí sản phẩm";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Black;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(223, 22);
            label3.Name = "label3";
            label3.Size = new Size(94, 20);
            label3.TabIndex = 3;
            label3.Text = "Giỏ hàng \U0001f6d2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Black;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(71, 22);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 2;
            label2.Text = "Sản phẩm";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Properties.Resources.icon_momocha;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(370, -15);
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
            Controls.Add(KH_pn_tab);
            Controls.Add(KH_pn_menu);
            Controls.Add(pictureBox2);
            Name = "NVQUAY";
            KH_pn_menu.ResumeLayout(false);
            KH_pn_menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel KH_pn_tab;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Panel KH_pn_menu;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private PictureBox pictureBox2;
    }
}