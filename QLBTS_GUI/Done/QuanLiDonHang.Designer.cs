using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    partial class QuanLiDonHang 
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private DataGridView dgvOrders;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dgvOrders = new DataGridView();
            MaDH = new DataGridViewTextBoxColumn();
            NgayDat = new DataGridViewTextBoxColumn();
            TongTien = new DataGridViewTextBoxColumn();
            TrangThai = new DataGridViewTextBoxColumn();
            SDT = new DataGridViewTextBoxColumn();
            DiaChi = new DataGridViewTextBoxColumn();
            lblTitle = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrders).BeginInit();
            SuspendLayout();
            // 
            // dgvOrders
            // 
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.LightGray;
            dgvOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.Black;
            dataGridViewCellStyle2.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvOrders.ColumnHeadersHeight = 40;
            dgvOrders.Columns.AddRange(new DataGridViewColumn[] { MaDH, NgayDat, TongTien, TrangThai, SDT, DiaChi });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = Color.White;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvOrders.DefaultCellStyle = dataGridViewCellStyle3;
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.GridColor = Color.Gray;
            dgvOrders.Location = new Point(0, 94);
            dgvOrders.Name = "dgvOrders";
            dgvOrders.ReadOnly = true;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowHeadersWidth = 51;
            dgvOrders.RowTemplate.Height = 45;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.Size = new Size(944, 415);
            dgvOrders.TabIndex = 1;
            dgvOrders.CellContentClick += dgvOrders_CellContentClick;
            // 
            // MaDH
            // 
            MaDH.DataPropertyName = "MaDH";
            MaDH.HeaderText = "Mã đơn";
            MaDH.MinimumWidth = 6;
            MaDH.Name = "MaDH";
            MaDH.ReadOnly = true;
            MaDH.Width = 146;
            // 
            // NgayDat
            // 
            NgayDat.DataPropertyName = "NgayDat";
            NgayDat.HeaderText = "Thời gian đặt ";
            NgayDat.MinimumWidth = 6;
            NgayDat.Name = "NgayDat";
            NgayDat.ReadOnly = true;
            NgayDat.Width = 146;
            // 
            // TongTien
            // 
            TongTien.DataPropertyName = "TongTien";
            TongTien.HeaderText = "Tổng tiền";
            TongTien.MinimumWidth = 6;
            TongTien.Name = "TongTien";
            TongTien.ReadOnly = true;
            TongTien.Width = 146;
            // 
            // TrangThai
            // 
            TrangThai.DataPropertyName = "TrangThai";
            TrangThai.HeaderText = "Trạng thái";
            TrangThai.MinimumWidth = 6;
            TrangThai.Name = "TrangThai";
            TrangThai.ReadOnly = true;
            TrangThai.Width = 146;
            // 
            // SDT
            // 
            SDT.HeaderText = "Số điện thoại";
            SDT.MinimumWidth = 6;
            SDT.Name = "SDT";
            SDT.ReadOnly = true;
            SDT.Width = 146;
            // 
            // DiaChi
            // 
            DiaChi.HeaderText = "Địa chỉ";
            DiaChi.MinimumWidth = 6;
            DiaChi.Name = "DiaChi";
            DiaChi.ReadOnly = true;
            DiaChi.Width = 146;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Cambria", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(0, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(900, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Quản lý đơn hàng";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // QuanLiDonHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(948, 513);
            Controls.Add(dgvOrders);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "QuanLiDonHang";
            StartPosition = FormStartPosition.CenterScreen;
            Load += QLDonHangForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrders).EndInit();
            ResumeLayout(false);
        }

        private DataGridViewTextBoxColumn MaDH;
        private DataGridViewTextBoxColumn NgayDat;
        private DataGridViewTextBoxColumn TongTien;
        private DataGridViewTextBoxColumn TrangThai;
        private DataGridViewTextBoxColumn SDT;
        private DataGridViewTextBoxColumn DiaChi;
    }
}
