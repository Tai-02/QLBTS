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
            colMaDH = new DataGridViewTextBoxColumn();
            colNgayDat = new DataGridViewTextBoxColumn();
            colGia = new DataGridViewTextBoxColumn();
            colTrangThai = new DataGridViewTextBoxColumn();
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
            dgvOrders.Columns.AddRange(new DataGridViewColumn[] { colMaDH, colNgayDat, colGia, colTrangThai, colThongTin, colChiTiet });
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.GridColor = Color.FromArgb(224, 224, 224);
            dgvOrders.Location = new Point(40, 140);
            dgvOrders.MultiSelect = false;
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.RowTemplate.Height = 50;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(920, 400);
            dgvOrders.TabIndex = 2;
            // 
            // colMaDH
            // 
            colMaDH.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colMaDH.DataPropertyName = "DisplayMaDH";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            colMaDH.DefaultCellStyle = dataGridViewCellStyle2;
            colMaDH.FillWeight = 80F;
            colMaDH.HeaderText = "Mã đơn hàng";
            colMaDH.MinimumWidth = 6;
            colMaDH.Name = "colMaDH";
            colMaDH.ReadOnly = true;
            colMaDH.Width = 158;
            // 
            // colNgayDat
            // 
            colNgayDat.HeaderText = "Ngày đặt đơn";
            colNgayDat.MinimumWidth = 6;
            colNgayDat.Name = "colNgayDat";
            colNgayDat.ReadOnly = true;
            // 
            // colGia
            // 
            colGia.DataPropertyName = "DisplayTongTien";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            colGia.DefaultCellStyle = dataGridViewCellStyle3;
            colGia.FillWeight = 90F;
            colGia.HeaderText = "Giá";
            colGia.MinimumWidth = 6;
            colGia.Name = "colGia";
            colGia.ReadOnly = true;
            // 
            // colTrangThai
            // 
            colTrangThai.DataPropertyName = "TextTrangThai";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            colTrangThai.DefaultCellStyle = dataGridViewCellStyle4;
            colTrangThai.HeaderText = "Trạng thái";
            colTrangThai.MinimumWidth = 6;
            colTrangThai.Name = "colTrangThai";
            colTrangThai.ReadOnly = true;
            // 
            // colThongTin
            // 
            colThongTin.HeaderText = "Thông tin đơn";
            colThongTin.MinimumWidth = 6;
            colThongTin.Name = "colThongTin";
            colThongTin.ReadOnly = true;
            // 
            // colChiTiet
            // 
            colChiTiet.HeaderText = "Chi tiết";
            colChiTiet.MinimumWidth = 6;
            colChiTiet.Name = "colChiTiet";
            colChiTiet.ReadOnly = true;
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

        private DataGridViewTextBoxColumn colMaDH;
        private DataGridViewTextBoxColumn colNgayDat;
        private DataGridViewTextBoxColumn colGia;
        private DataGridViewTextBoxColumn colTrangThai;
        private DataGridViewTextBoxColumn colThongTin;
        private DataGridViewTextBoxColumn colChiTiet;
    }
}