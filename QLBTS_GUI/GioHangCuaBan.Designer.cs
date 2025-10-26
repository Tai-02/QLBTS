namespace QLBTS_GUI
{
    partial class GioHangCuaBan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel1 = new Panel();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label8 = new Label();
            label9 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            flowCart = new FlowLayoutPanel();
            label2 = new Label();
            label3 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label12);
            panel1.Controls.Add(label11);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold);
            panel1.Location = new Point(642, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(337, 553);
            panel1.TabIndex = 1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Times New Roman", 16.2F);
            label12.Location = new Point(94, 373);
            label12.Name = "label12";
            label12.Size = new Size(120, 33);
            label12.TabIndex = 8;
            label12.Text = "150.000đ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 16.2F);
            label11.Location = new Point(24, 323);
            label11.Name = "label11";
            label11.Size = new Size(132, 33);
            label11.TabIndex = 7;
            label11.Text = "Tổng cộng";
            // 
            // label10
            // 
            label10.BackColor = SystemColors.ControlText;
            label10.Location = new Point(24, 295);
            label10.Name = "label10";
            label10.Size = new Size(300, 1);
            label10.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Times New Roman", 16.2F);
            label8.Location = new Point(183, 223);
            label8.Name = "label8";
            label8.Size = new Size(52, 33);
            label8.TabIndex = 5;
            label8.Text = "0%";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Times New Roman", 16.2F);
            label9.Location = new Point(24, 223);
            label9.Name = "label9";
            label9.Size = new Size(114, 33);
            label9.TabIndex = 4;
            label9.Text = "Giảm giá";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 16.2F, FontStyle.Strikeout);
            label7.Location = new Point(183, 143);
            label7.Name = "label7";
            label7.Size = new Size(120, 33);
            label7.TabIndex = 3;
            label7.Text = "150.000đ";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Times New Roman", 16.2F);
            label6.Location = new Point(24, 143);
            label6.Name = "label6";
            label6.Size = new Size(114, 33);
            label6.TabIndex = 2;
            label6.Text = "Tạm tính";
            // 
            // label5
            // 
            label5.BackColor = SystemColors.ControlText;
            label5.Location = new Point(24, 105);
            label5.Name = "label5";
            label5.Size = new Size(300, 1);
            label5.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Times New Roman", 19.8F, FontStyle.Bold);
            label4.Location = new Point(24, 24);
            label4.Name = "label4";
            label4.Size = new Size(198, 38);
            label4.TabIndex = 0;
            label4.Text = "TỔNG KẾT";
            // 
            // flowCart
            // 
            flowCart.AutoScroll = true;
            flowCart.Location = new Point(12, 77);
            flowCart.Name = "flowCart";
            flowCart.Size = new Size(611, 475);
            flowCart.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 10.8F);
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(24, 20);
            label2.TabIndex = 0;
            label2.Text = "...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 10.8F);
            label3.Location = new Point(56, 54);
            label3.Name = "label3";
            label3.Size = new Size(78, 20);
            label3.TabIndex = 1;
            label3.Text = "sản phẩm";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(237, 32);
            label1.TabIndex = 3;
            label1.Text = "Giỏ hàng của bạn";
            // 
            // GioHangCuaBan
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(982, 553);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(flowCart);
            Controls.Add(panel1);
            Name = "GioHangCuaBan";
            Text = "GioHangCuaBan";
            Load += GioHangCuaBan_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private FlowLayoutPanel flowCart;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label6;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label8;
        private Label label9;
    }
}
