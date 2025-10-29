using QLBTS_BBL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class LichSuNhanDon : Form
    {
        private int maNVGiao;
        private FormLichSuNhanDonBBL bbl;

        public LichSuNhanDon()
        {
            InitializeComponent();
            bbl = new FormLichSuNhanDonBBL();
            maNVGiao = 2;
            InitUI();
            LoadData();
        }

        private void InitUI()
        {
            // ===== Label tiêu đề =====
            label1.Text = "LỊCH SỬ NHẬN ĐƠN";
            label1.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(40, 90, 180);
            label1.AutoSize = true;
            label1.Location = new Point(
                (this.ClientSize.Width - label1.PreferredWidth) / 2,
                20
            );
            label1.Anchor = AnchorStyles.Top;

            // ===== DataGridView =====
            DataGridView dgv = new DataGridView();
            dgv.Name = "dgvLichSu";

            // 👉 Quan trọng: fill toàn bộ panel
            dgv.Dock = DockStyle.Fill;

            // 👉 Có thể thêm chút khoảng cách giữa biên panel và bảng (nếu muốn)
            panel1.Padding = new Padding(15);

            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.Fixed3D;

            // Style header
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersVisible = true;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 90, 180);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Style hàng dữ liệu
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // ===== Thêm các cột =====
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Lịch Sử", DataPropertyName = "MaLS", Width = 100 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Đơn Hàng", DataPropertyName = "MaDH", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tổng Tiền (đ)", DataPropertyName = "TongTien", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Trạng Thái", DataPropertyName = "TrangThai", Width = 150 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Thời Gian", DataPropertyName = "ThoiGian", Width = 180 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Loại Lịch Sử", DataPropertyName = "LoaiLichSu", Width = 150 });

            // ===== Thêm vào panel =====
            panel1.Controls.Clear();
            panel1.Controls.Add(dgv);
        }



        private void LoadData()
        {
            // Gọi tầng nghiệp vụ để lấy dữ liệu
            List<LichSuHoatDongDTO> ds = bbl.LayLichSuTheoMaNV(maNVGiao);

            // Gán vào DataGridView
            DataGridView dgv = panel1.Controls["dgvLichSu"] as DataGridView;
            dgv.DataSource = ds;
        }
    }
}
