namespace QLBTS_GUI
{
    partial class HieuSuat
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
            dgvHieuSuat = new DataGridView();
            cbbKieuHieuSuat = new ComboBox();
            dtpNgay = new DateTimePicker();
            txtMucTieu = new TextBox();
            lb = new Label();
            TTCN_btnCapnhat = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)dgvHieuSuat).BeginInit();
            SuspendLayout();
            // 
            // dgvHieuSuat
            // 
            dgvHieuSuat.AllowUserToAddRows = false;
            dgvHieuSuat.AllowUserToDeleteRows = false;
            dgvHieuSuat.BackgroundColor = Color.White;
            dgvHieuSuat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHieuSuat.Location = new Point(0, 59);
            dgvHieuSuat.Name = "dgvHieuSuat";
            dgvHieuSuat.ReadOnly = true;
            dgvHieuSuat.RowHeadersWidth = 51;
            dgvHieuSuat.Size = new Size(948, 454);
            dgvHieuSuat.TabIndex = 0;
            // 
            // cbbKieuHieuSuat
            // 
            cbbKieuHieuSuat.FormattingEnabled = true;
            cbbKieuHieuSuat.Location = new Point(785, 12);
            cbbKieuHieuSuat.Name = "cbbKieuHieuSuat";
            cbbKieuHieuSuat.Size = new Size(151, 28);
            cbbKieuHieuSuat.TabIndex = 3;
            // 
            // dtpNgay
            // 
            dtpNgay.Location = new Point(517, 10);
            dtpNgay.Name = "dtpNgay";
            dtpNgay.Size = new Size(246, 27);
            dtpNgay.TabIndex = 4;
            // 
            // txtMucTieu
            // 
            txtMucTieu.Location = new Point(183, 14);
            txtMucTieu.Name = "txtMucTieu";
            txtMucTieu.Size = new Size(127, 27);
            txtMucTieu.TabIndex = 5;
            txtMucTieu.KeyPress += txt_KeyPress;
            // 
            // lb
            // 
            lb.AutoSize = true;
            lb.Location = new Point(26, 17);
            lb.Name = "lb";
            lb.Size = new Size(142, 20);
            lb.TabIndex = 6;
            lb.Text = "Doanh thu mục tiêu:";
            // 
            // TTCN_btnCapnhat
            // 
            TTCN_btnCapnhat.BorderRadius = 10;
            TTCN_btnCapnhat.CustomizableEdges = customizableEdges1;
            TTCN_btnCapnhat.DisabledState.BorderColor = Color.DarkGray;
            TTCN_btnCapnhat.DisabledState.CustomBorderColor = Color.DarkGray;
            TTCN_btnCapnhat.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            TTCN_btnCapnhat.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            TTCN_btnCapnhat.FillColor = Color.FromArgb(128, 64, 64);
            TTCN_btnCapnhat.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TTCN_btnCapnhat.ForeColor = Color.White;
            TTCN_btnCapnhat.HoverState.FillColor = Color.LightGray;
            TTCN_btnCapnhat.HoverState.ForeColor = Color.Black;
            TTCN_btnCapnhat.Location = new Point(325, 12);
            TTCN_btnCapnhat.Name = "TTCN_btnCapnhat";
            TTCN_btnCapnhat.ShadowDecoration.CustomizableEdges = customizableEdges2;
            TTCN_btnCapnhat.Size = new Size(131, 29);
            TTCN_btnCapnhat.TabIndex = 7;
            TTCN_btnCapnhat.Text = "Tính hiệu suất";
            TTCN_btnCapnhat.Click += TTCN_btnCapnhat_Click;
            // 
            // HieuSuat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(948, 513);
            Controls.Add(TTCN_btnCapnhat);
            Controls.Add(lb);
            Controls.Add(txtMucTieu);
            Controls.Add(dtpNgay);
            Controls.Add(cbbKieuHieuSuat);
            Controls.Add(dgvHieuSuat);
            Name = "HieuSuat";
            Text = "HieuSuat";
            Load += HieuSuat_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHieuSuat).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvHieuSuat;
        private ComboBox cbbKieuHieuSuat;
        private DateTimePicker dtpNgay;
        private TextBox txtMucTieu;
        private Label lb;
        private Guna.UI2.WinForms.Guna2Button TTCN_btnCapnhat;
    }
}