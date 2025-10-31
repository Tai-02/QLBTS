using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class QuanLiDonHang : Form
    {
        private readonly DonHangBLL bll;
        UI_Form ui = new UI_Form();

        public QuanLiDonHang()
        {
            bll = new DonHangBLL();
            InitializeComponent();
            dgvOrders.CellContentClick -= dgvOrders_CellContentClick;
            dgvOrders.CellContentClick += dgvOrders_CellContentClick;
            dgvOrders.CellPainting += dgvOrders_CellPainting;
        }

        private void QLDonHangForm_Load(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            dgvOrders.AutoGenerateColumns = false;

            List<DonHangDTO> dsDonHang = new List<DonHangDTO>();

            dsDonHang.AddRange(bll.LayDSDonHangTheoTrangThai("Chờ xác nhận"));
            dsDonHang.AddRange(bll.LayDSDonHangTheoTrangThai("Đã nhận"));
            dgvOrders.DataSource = dsDonHang.OrderByDescending(d => d.NgayDat).ToList();

            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgvOrders.RowsDefaultCellStyle.BackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.Black;

            AddButtonColumn("btnNhan", "Nhận đơn", "Nhận", Color.LimeGreen);
            AddButtonColumn("btnHuy", "Hủy đơn", "Hủy", Color.Red);
            AddButtonColumn("btnChuyen", "Chuyển hàng", "Chuyển", Color.RoyalBlue);
            AddButtonColumn("btnChiTiet", "Chi tiết", "Xem chi tiết", Color.Black);

            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);

            if (dgvOrders.Columns.Contains("Gia"))
            {
                dgvOrders.Columns["Gia"].DefaultCellStyle.Format = "N0";
                dgvOrders.Columns["Gia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            dgvOrders.Refresh();
        }

        private void AddButtonColumn(string name, string header, string text, Color color)
        {
            if (dgvOrders.Columns.Contains(name)) return;

            var btn = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = header,
                Text = text,
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat
            };

            btn.DefaultCellStyle.BackColor = color;
            btn.DefaultCellStyle.ForeColor = Color.White;
            btn.DefaultCellStyle.Font = new Font("Times New Roman", 11, FontStyle.Bold);
            dgvOrders.Columns.Add(btn);
        }

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return; // bỏ qua header
            if (!dgvOrders.Columns[e.ColumnIndex].Name.StartsWith("btn")) return; // chỉ button

            // Lấy trạng thái của dòng hiện tại
            string trangThai = dgvOrders.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";
            string col = dgvOrders.Columns[e.ColumnIndex].Name;

            // Quyết định nút có hiển thị không
            bool hienThi = col switch
            {
                "btnNhan" => trangThai == "Chờ xác nhận",
                "btnHuy" => trangThai == "Chờ xác nhận" || trangThai == "Đã nhận",
                "btnChuyen" => trangThai == "Đã nhận",
                "btnChiTiet" => true,
                _ => false
            };

            if (!hienThi)
            {
                e.Handled = true; // không vẽ nút
                return;
            }

            // Vẽ nền và nội dung ô
            e.PaintBackground(e.CellBounds, true);

            // Tạo nút nhỏ gọn ở giữa ô
            Rectangle rect = new Rectangle(
                e.CellBounds.X + 4,
                e.CellBounds.Y + 4,
                e.CellBounds.Width - 8,
                e.CellBounds.Height - 8
            );

            Color btnColor = col switch
            {
                "btnNhan" => Color.LimeGreen,
                "btnHuy" => Color.Red,
                "btnChuyen" => Color.RoyalBlue,
                "btnChiTiet" => Color.Black,
                _ => Color.Black
            };

            using (SolidBrush brush = new SolidBrush(btnColor))
                e.Graphics.FillRectangle(brush, rect);

            TextRenderer.DrawText(e.Graphics, (string)e.FormattedValue,
                new Font("Times New Roman", 11, FontStyle.Bold),
                rect, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            e.Handled = true;
        }
        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int maDH = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["MaDH"].Value);
            string col = dgvOrders.Columns[e.ColumnIndex].Name;
            string trangThai = dgvOrders.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";

            bool buttonHienThi = col switch
            {
                "btnNhan" => trangThai == "Chờ xác nhận",
                "btnHuy" => trangThai == "Chờ xác nhận" || trangThai == "Đã nhận",
                "btnChuyen" => trangThai == "Đã nhận",
                "btnChiTiet" => true,
                _ => false
            };

            if (!buttonHienThi) return;

            bool ok = false;
            if (col == "btnNhan") ok = bll.DoiTrangThai(maDH, "Đã nhận");
            else if (col == "btnHuy") ok = bll.HuyDonHang(maDH);
            else if (col == "btnChuyen") ok = bll.DoiTrangThai(maDH, "Chờ giao");
            else if (col == "btnChiTiet")
            {
                ui.OpenChildForm(new ChiTietDonHang(maDH), NVQUAY.NVQ_pn_tab);
                return;
            }

            if (ok)
            {
                ui.OpenChildForm(new QuanLiDonHang(), NVQUAY.NVQ_pn_tab);
            }
        }
    }
}
