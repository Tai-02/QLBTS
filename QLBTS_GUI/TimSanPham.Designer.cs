namespace QLBTS_GUI
{
    partial class TimSanPham
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
            pnLocTimKiem = new Panel();
            chBTopping = new CheckBox();
            chBTratraicay = new CheckBox();
            chBMonmoi = new CheckBox();
            chBBestseller = new CheckBox();
            label1 = new Label();
            flpSanPham = new FlowLayoutPanel();
            panel2 = new Panel();
            txtTimkiem = new TextBox();
            btnTimkiem = new Button();
            pnLocTimKiem.SuspendLayout();
            flpSanPham.SuspendLayout();
            SuspendLayout();
            // 
            // pnLocTimKiem
            // 
            pnLocTimKiem.BorderStyle = BorderStyle.FixedSingle;
            pnLocTimKiem.Controls.Add(chBTopping);
            pnLocTimKiem.Controls.Add(chBTratraicay);
            pnLocTimKiem.Controls.Add(chBMonmoi);
            pnLocTimKiem.Controls.Add(chBBestseller);
            pnLocTimKiem.Controls.Add(label1);
            pnLocTimKiem.Location = new Point(6, 81);
            pnLocTimKiem.Name = "pnLocTimKiem";
            pnLocTimKiem.Size = new Size(214, 283);
            pnLocTimKiem.TabIndex = 5;
            // 
            // chBTopping
            // 
            chBTopping.AutoSize = true;
            chBTopping.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chBTopping.Location = new Point(20, 233);
            chBTopping.Name = "chBTopping";
            chBTopping.Size = new Size(100, 25);
            chBTopping.TabIndex = 4;
            chBTopping.Text = "Topping";
            chBTopping.UseVisualStyleBackColor = true;
            // 
            // chBTratraicay
            // 
            chBTratraicay.AutoSize = true;
            chBTratraicay.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chBTratraicay.Location = new Point(20, 175);
            chBTratraicay.Name = "chBTratraicay";
            chBTratraicay.Size = new Size(127, 25);
            chBTratraicay.TabIndex = 3;
            chBTratraicay.Text = "Trà trái cây";
            chBTratraicay.UseVisualStyleBackColor = true;
            // 
            // chBMonmoi
            // 
            chBMonmoi.AutoSize = true;
            chBMonmoi.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chBMonmoi.Location = new Point(20, 125);
            chBMonmoi.Name = "chBMonmoi";
            chBMonmoi.Size = new Size(106, 25);
            chBMonmoi.TabIndex = 2;
            chBMonmoi.Text = "Món mới";
            chBMonmoi.UseVisualStyleBackColor = true;
            // 
            // chBBestseller
            // 
            chBBestseller.AutoSize = true;
            chBBestseller.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chBBestseller.Location = new Point(20, 70);
            chBBestseller.Name = "chBBestseller";
            chBBestseller.Size = new Size(121, 25);
            chBBestseller.TabIndex = 1;
            chBBestseller.Text = "Best seller";
            chBBestseller.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(34, 12);
            label1.Name = "label1";
            label1.Size = new Size(161, 28);
            label1.TabIndex = 0;
            label1.Text = "Bộ lọc tìm kiếm";
            // 
            // flpSanPham
            // 
            flpSanPham.AutoScroll = true;
            flpSanPham.Controls.Add(panel2);
            flpSanPham.Location = new Point(272, 58);
            flpSanPham.Name = "flpSanPham";
            flpSanPham.Size = new Size(705, 497);
            flpSanPham.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(0, 0);
            panel2.TabIndex = 1;
            // 
            // txtTimkiem
            // 
            txtTimkiem.BackColor = Color.LightGray;
            txtTimkiem.BorderStyle = BorderStyle.FixedSingle;
            txtTimkiem.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtTimkiem.Location = new Point(648, 12);
            txtTimkiem.Name = "txtTimkiem";
            txtTimkiem.Size = new Size(210, 30);
            txtTimkiem.TabIndex = 7;
            txtTimkiem.TextChanged += txtTimkiem_TextChanged;
            txtTimkiem.Enter += txtTimkiem_Enter;
            txtTimkiem.KeyDown += txtTimkiem_KeyDown;
            txtTimkiem.Leave += txtTimkiem_Leave;
            // 
            // btnTimkiem
            // 
            btnTimkiem.BackgroundImage = Properties.Resources.images;
            btnTimkiem.BackgroundImageLayout = ImageLayout.Zoom;
            btnTimkiem.FlatAppearance.BorderSize = 0;
            btnTimkiem.FlatStyle = FlatStyle.Flat;
            btnTimkiem.Location = new Point(848, 13);
            btnTimkiem.Name = "btnTimkiem";
            btnTimkiem.Size = new Size(65, 29);
            btnTimkiem.TabIndex = 6;
            btnTimkiem.UseVisualStyleBackColor = true;
            btnTimkiem.Click += btnTimkiem_Click;
            // 
            // TimSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(982, 553);
            Controls.Add(pnLocTimKiem);
            Controls.Add(flpSanPham);
            Controls.Add(txtTimkiem);
            Controls.Add(btnTimkiem);
            Name = "TimSanPham";
            Text = "TimSanPham";
            Load += TimSanPham_Load;
            pnLocTimKiem.ResumeLayout(false);
            pnLocTimKiem.PerformLayout();
            flpSanPham.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnLocTimKiem;
        private CheckBox chBTopping;
        private CheckBox chBTratraicay;
        private CheckBox chBMonmoi;
        private CheckBox chBBestseller;
        private Label label1;
        private FlowLayoutPanel flpSanPham;
        private Panel panel2;
        private TextBox txtTimkiem;
        private Button btnTimkiem;
    }
}