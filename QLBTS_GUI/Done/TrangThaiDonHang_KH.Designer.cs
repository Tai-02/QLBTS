namespace QLBTS_GUI
{
    partial class TrangThaiDonHang_KH
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblTitle = new Label();
            dgvOrders = new DataGridView();
            MaDH = new DataGridViewTextBoxColumn();
            NgayDat = new DataGridViewTextBoxColumn();
            TongTien = new DataGridViewTextBoxColumn();
            TrangThai = new DataGridViewTextBoxColumn();
            colThongTin = new DataGridViewTextBoxColumn();
            colChiTiet = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.Location = new Point(280, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(405, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Trạng thái đơn hàng";
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AllowUserToResizeRows = false;
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvOrders.ColumnHeadersHeight = 45;
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvOrders.Columns.AddRange(new DataGridViewColumn[] { MaDH, NgayDat, TongTien, TrangThai, colThongTin, colChiTiet });
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.GridColor = Color.FromArgb(224, 224, 224);
            dgvOrders.Location = new Point(12, 96);
            dgvOrders.MultiSelect = false;
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.RowTemplate.Height = 50;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(987, 472);
            dgvOrders.TabIndex = 2;
            // 
            // MaDH
            // 
            MaDH.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            MaDH.DataPropertyName = "MaDH";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            MaDH.DefaultCellStyle = dataGridViewCellStyle2;
            MaDH.FillWeight = 80F;
            MaDH.HeaderText = "Mã đơn hàng";
            MaDH.MinimumWidth = 6;
            MaDH.Name = "MaDH";
            MaDH.ReadOnly = true;
            MaDH.Width = 158;
            // 
            // NgayDat
            // 
            NgayDat.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            NgayDat.DataPropertyName = "NgayDat";
            NgayDat.HeaderText = "Ngày đặt đơn";
            NgayDat.MinimumWidth = 6;
            NgayDat.Name = "NgayDat";
            NgayDat.ReadOnly = true;
            NgayDat.Width = 161;
            // 
            // TongTien
            // 
            TongTien.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            TongTien.DataPropertyName = "TongTien";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            TongTien.DefaultCellStyle = dataGridViewCellStyle3;
            TongTien.FillWeight = 90F;
            TongTien.HeaderText = "Giá";
            TongTien.MinimumWidth = 6;
            TongTien.Name = "TongTien";
            TongTien.ReadOnly = true;
            TongTien.Width = 68;
            // 
            // TrangThai
            // 
            TrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            TrangThai.DataPropertyName = "TrangThai";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TrangThai.DefaultCellStyle = dataGridViewCellStyle4;
            TrangThai.HeaderText = "Trạng thái";
            TrangThai.MinimumWidth = 6;
            TrangThai.Name = "TrangThai";
            TrangThai.ReadOnly = true;
            TrangThai.Width = 129;
            // 
            // colThongTin
            // 
            colThongTin.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colThongTin.HeaderText = "Thông tin đơn";
            colThongTin.MinimumWidth = 6;
            colThongTin.Name = "colThongTin";
            colThongTin.ReadOnly = true;
            colThongTin.Width = 167;
            // 
            // colChiTiet
            // 
            colChiTiet.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colChiTiet.HeaderText = "Chi tiết";
            colChiTiet.MinimumWidth = 6;
            colChiTiet.Name = "colChiTiet";
            colChiTiet.ReadOnly = true;
            colChiTiet.Width = 101;
            // 
            // TrangThaiDonHang_KH
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1000, 580);
            Controls.Add(dgvOrders);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TrangThaiDonHang_KH";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trạng thái đơn hàng - MOMOCHA";
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private DataGridView dgvOrders;

        #endregion

        private DataGridViewTextBoxColumn MaDH;
        private DataGridViewTextBoxColumn NgayDat;
        private DataGridViewTextBoxColumn TongTien;
        private DataGridViewTextBoxColumn TrangThai;
        private DataGridViewTextBoxColumn colThongTin;
        private DataGridViewTextBoxColumn colChiTiet;
    }
}