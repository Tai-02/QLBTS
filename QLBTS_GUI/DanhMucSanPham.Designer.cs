namespace QLBTS_GUI
{
    partial class DanhMucSanPham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DanhMucSanPham));
            pnLocTimKiem = new Panel();
            cbb_loai = new ComboBox();
            chkTopping = new CheckBox();
            chkTrasua = new CheckBox();
            chkMonmoi = new CheckBox();
            chkBestseller = new CheckBox();
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
            pnLocTimKiem.Controls.Add(cbb_loai);
            pnLocTimKiem.Controls.Add(chkTopping);
            pnLocTimKiem.Controls.Add(chkTrasua);
            pnLocTimKiem.Controls.Add(chkMonmoi);
            pnLocTimKiem.Controls.Add(chkBestseller);
            pnLocTimKiem.Controls.Add(label1);
            pnLocTimKiem.Location = new Point(6, 74);
            pnLocTimKiem.Name = "pnLocTimKiem";
            pnLocTimKiem.Size = new Size(214, 337);
            pnLocTimKiem.TabIndex = 9;
            // 
            // cbb_loai
            // 
            cbb_loai.FormattingEnabled = true;
            cbb_loai.Location = new Point(20, 281);
            cbb_loai.Name = "cbb_loai";
            cbb_loai.Size = new Size(151, 28);
            cbb_loai.TabIndex = 13;
            cbb_loai.SelectedIndexChanged += cbb_loai_SelectedIndexChanged;
            // 
            // chkTopping
            // 
            chkTopping.AutoSize = true;
            chkTopping.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkTopping.Location = new Point(20, 233);
            chkTopping.Name = "chkTopping";
            chkTopping.Size = new Size(100, 25);
            chkTopping.TabIndex = 4;
            chkTopping.Text = "Topping";
            chkTopping.UseVisualStyleBackColor = true;
            chkTopping.CheckedChanged += chkTopping_CheckedChanged;
            // 
            // chkTrasua
            // 
            chkTrasua.AutoSize = true;
            chkTrasua.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkTrasua.Location = new Point(20, 175);
            chkTrasua.Name = "chkTrasua";
            chkTrasua.Size = new Size(96, 25);
            chkTrasua.TabIndex = 3;
            chkTrasua.Text = "Trà sữa";
            chkTrasua.UseVisualStyleBackColor = true;
            chkTrasua.CheckedChanged += chkTraSua_CheckedChanged;
            // 
            // chkMonmoi
            // 
            chkMonmoi.AutoSize = true;
            chkMonmoi.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkMonmoi.Location = new Point(20, 125);
            chkMonmoi.Name = "chkMonmoi";
            chkMonmoi.Size = new Size(106, 25);
            chkMonmoi.TabIndex = 2;
            chkMonmoi.Text = "Món mới";
            chkMonmoi.UseVisualStyleBackColor = true;
            chkMonmoi.CheckedChanged += chkMonmoi_CheckedChanged;
            // 
            // chkBestseller
            // 
            chkBestseller.AutoSize = true;
            chkBestseller.Font = new Font("Cambria", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkBestseller.Location = new Point(20, 70);
            chkBestseller.Name = "chkBestseller";
            chkBestseller.Size = new Size(121, 25);
            chkBestseller.TabIndex = 1;
            chkBestseller.Text = "Best seller";
            chkBestseller.UseVisualStyleBackColor = true;
            chkBestseller.CheckedChanged += chkBestseller_CheckedChanged;
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
            flpSanPham.Location = new Point(251, 51);
            flpSanPham.Name = "flpSanPham";
            flpSanPham.Size = new Size(726, 497);
            flpSanPham.TabIndex = 12;
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
            txtTimkiem.Location = new Point(648, 5);
            txtTimkiem.Name = "txtTimkiem";
            txtTimkiem.Size = new Size(210, 30);
            txtTimkiem.TabIndex = 11;
            // 
            // btnTimkiem
            // 
            btnTimkiem.BackgroundImage = (Image)resources.GetObject("btnTimkiem.BackgroundImage");
            btnTimkiem.BackgroundImageLayout = ImageLayout.Zoom;
            btnTimkiem.FlatAppearance.BorderSize = 0;
            btnTimkiem.FlatStyle = FlatStyle.Flat;
            btnTimkiem.Location = new Point(843, 6);
            btnTimkiem.Name = "btnTimkiem";
            btnTimkiem.Size = new Size(65, 29);
            btnTimkiem.TabIndex = 10;
            btnTimkiem.UseVisualStyleBackColor = true;
            btnTimkiem.Click += btnTimkiem_Click;
            // 
            // DanhMucSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(982, 553);
            Controls.Add(pnLocTimKiem);
            Controls.Add(flpSanPham);
            Controls.Add(txtTimkiem);
            Controls.Add(btnTimkiem);
            Name = "DanhMucSanPham";
            Text = "TimKiemSanPham";
            Load += DanhMucSanPham_Load;
            pnLocTimKiem.ResumeLayout(false);
            pnLocTimKiem.PerformLayout();
            flpSanPham.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnLocTimKiem;
        private CheckBox chkTopping;
        private CheckBox chkTrasua;
        private CheckBox chkMonmoi;
        private CheckBox chkBestseller;
        private Label label1;
        private FlowLayoutPanel flpSanPham;
        private Panel panel2;
        private TextBox txtTimkiem;
        private Button btnTimkiem;
        private ComboBox cbb_loai;
    }
}