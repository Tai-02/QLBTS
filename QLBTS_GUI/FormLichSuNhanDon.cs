using QLBTS_BBL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class FormLichSuNhanDon : Form
    {
        private int maNVGiao = 1; // test tam, sau nay se lay tu form dang nhap
        private FormLichSuNhanDonBBL bbl;

        public FormLichSuNhanDon()
        {
            InitializeComponent();
            bbl = new FormLichSuNhanDonBBL();
            InitUI();
            LoadData();
        }

        private void InitUI()
        {
            // Label tieu de
            label1.Text = "LỊCH SỬ NHẬN ĐƠN";
            label1.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(40, 90, 180);
            label1.AutoSize = true;

            // Can giua label
            label1.Location = new Point(
                (this.ClientSize.Width - label1.PreferredWidth) / 2,
                20
            );
            label1.Anchor = AnchorStyles.Top;

            // Tao DataGridView
            DataGridView dgv = new DataGridView();
            dgv.Name = "dgvLichSu";
            dgv.Location = new Point(30, 80);
            dgv.Size = new Size(920, 420);
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.Fixed3D;

            // Style header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 90, 180);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Style hang du lieu
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Them cac cot vao bang
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mã LSGH",
                DataPropertyName = "MaLSGH",
                Width = 100
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mã NV Giao",
                DataPropertyName = "MaNVGiao",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Mã Đơn Hàng",
                DataPropertyName = "MaDH",
                Width = 120
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Tổng Tiền (đ)",
                DataPropertyName = "TongTien",
                Width = 150
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Trạng Thái",
                DataPropertyName = "TrangThai",
                Width = 150
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Thời Gian",
                DataPropertyName = "ThoiGian",
                Width = 200
            });

            // Them vao panel hien co
            panel1.Controls.Clear();
            panel1.Controls.Add(dgv);
        }

        private void LoadData()
        {
            // Goi den tang nghiep vu de lay du lieu
            List<LichSuGiaoHangDTO> ds = bbl.LayLichSuTheoMaNV(maNVGiao);

            // Gan vao DataGridView
            DataGridView dgv = panel1.Controls["dgvLichSu"] as DataGridView;
            dgv.DataSource = ds;
        }
    }
}
