namespace QLBTS_GUI
{
    partial class TrangChu
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
            panel1 = new Panel();
            pcNengioithieu = new PictureBox();
            panel2 = new Panel();
            pcAnhnen = new PictureBox();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcNengioithieu).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pcAnhnen).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pcNengioithieu);
            panel1.Location = new Point(-1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(355, 556);
            panel1.TabIndex = 0;
            // 
            // pcNengioithieu
            // 
            pcNengioithieu.Location = new Point(0, 0);
            pcNengioithieu.Name = "pcNengioithieu";
            pcNengioithieu.Size = new Size(355, 556);
            pcNengioithieu.SizeMode = PictureBoxSizeMode.StretchImage;
            pcNengioithieu.TabIndex = 14;
            pcNengioithieu.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(pcAnhnen);
            panel2.Location = new Point(356, 1);
            panel2.Name = "panel2";
            panel2.Size = new Size(627, 553);
            panel2.TabIndex = 1;
            // 
            // pcAnhnen
            // 
            pcAnhnen.Location = new Point(0, 0);
            pcAnhnen.Name = "pcAnhnen";
            pcAnhnen.Size = new Size(627, 561);
            pcAnhnen.TabIndex = 0;
            pcAnhnen.TabStop = false;
            // 
            // panel3
            // 
            panel3.Location = new Point(20, 108);
            panel3.Name = "panel3";
            panel3.Size = new Size(178, 204);
            panel3.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Location = new Point(20, 336);
            panel4.Name = "panel4";
            panel4.Size = new Size(178, 204);
            panel4.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Location = new Point(229, 209);
            panel5.Name = "panel5";
            panel5.Size = new Size(178, 204);
            panel5.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.Location = new Point(436, 108);
            panel6.Name = "panel6";
            panel6.Size = new Size(178, 204);
            panel6.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Location = new Point(436, 336);
            panel7.Name = "panel7";
            panel7.Size = new Size(178, 204);
            panel7.TabIndex = 5;
            // 
            // TrangChu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "TrangChu";
            Text = "TrangChu";
            Load += TrangChu_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pcNengioithieu).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pcAnhnen).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pcNengioithieu;
        private Panel panel2;
        private PictureBox pcAnhnen;
        private Panel panel7;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
    }
}