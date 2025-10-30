namespace QLBTS_GUI
{
    partial class ChiTietDonHang
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
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold);
            panel1.Location = new Point(642, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(303, 509);
            panel1.TabIndex = 1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Times New Roman", 16.2F);
            label12.Location = new Point(139, 349);
            label12.Name = "label12";
            label12.Size = new Size(120, 33);
            label12.TabIndex = 8;
            label12.Text = "150.000đ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Times New Roman", 16.2F);
            label11.Location = new Point(24, 289);
            label11.Name = "label11";
            label11.Size = new Size(132, 33);
            label11.TabIndex = 7;
            label11.Text = "Tổng cộng";
            // 
            // label10
            // 
            label10.BackColor = SystemColors.ControlText;
            label10.Location = new Point(3, 238);
            label10.Name = "label10";
            label10.Size = new Size(300, 1);
            label10.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Times New Roman", 16.2F, FontStyle.Strikeout);
            label7.Location = new Point(159, 143);
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
            label5.Location = new Point(3, 105);
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
            label4.Size = new Size(192, 37);
            label4.TabIndex = 0;
            label4.Text = "TỔNG KẾT";
            // 
            // flowCart
            // 
            flowCart.AutoScroll = true;
            flowCart.Location = new Point(12, 77);
            flowCart.Name = "flowCart";
            flowCart.Size = new Size(611, 431);
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
            label1.Size = new Size(227, 32);
            label1.TabIndex = 3;
            label1.Text = "Giỏ hàng của bạn";
            // 
            // ChiTietDonHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(948, 513);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(flowCart);
            Controls.Add(panel1);
            Name = "ChiTietDonHang";
            Text = "GioHangCuaBan";
            Load += Chitietdonhang_Load;
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
    }
}
