using QLBTS_BLL;
using QLBTS_DTO;
using QuestPDF.Fluent;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class ChoXacNhan_NVG : Form
    {
        private readonly DonHangBLL bll;
        private readonly ChiTietDonHangBLL ctdh;
        UI_Form ui = new UI_Form();

        public ChoXacNhan_NVG()
        {
            bll = new DonHangBLL();
            ctdh = new ChiTietDonHangBLL();
            InitializeComponent();
            dgvOrders.CellContentClick -= dgvOrders_CellContentClick;
            dgvOrders.CellContentClick += dgvOrders_CellContentClick;
            dgvOrders.CellPainting += dgvOrders_CellPainting;
            dgvOrders.CellFormatting += dgvOrders_CellFormatting;
        }

        private void QLDonHangForm_Load(object sender, EventArgs e)
        {
            LoadDonHang();
        }

        private void LoadDonHang()
        {
            dgvOrders.AutoGenerateColumns = false;

            List<DonHangDTO> dsDonHang = new List<DonHangDTO>();

            dsDonHang.AddRange(bll.LayDSDonHangTheoTrangThai("Chờ giao"));
            dgvOrders.DataSource = dsDonHang.OrderByDescending(d => d.NgayDat).ToList();

            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dgvOrders.RowsDefaultCellStyle.BackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.Black;

            AddButtonColumn("btnNhan", "Nhận đơn", "Nhận giao", Color.LimeGreen);
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
        private void dgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvOrders.Columns[e.ColumnIndex].Name == "SDT" && dgvOrders.Rows[e.RowIndex].DataBoundItem is DonHangDTO dh)
            {
                e.Value = new TaiKhoanBLL().LaySDT(dh.MaKhach);
            }
            if (dgvOrders.Columns[e.ColumnIndex].Name == "DiaChi" && dgvOrders.Rows[e.RowIndex].DataBoundItem is DonHangDTO dh2)
            {
                e.Value = new TaiKhoanBLL().LayDiaChi(dh2.MaKhach);
            }
        }

        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return; // bỏ qua header
            if (!dgvOrders.Columns[e.ColumnIndex].Name.StartsWith("btn")) return; // chỉ button

            // Lấy trạng thái của dòng hiện tại
            string trangThai = dgvOrders.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";
            string col = dgvOrders.Columns[e.ColumnIndex].Name;

            // Quyết định nút có hiển thị không
            bool buttonHienThi = col switch
            {
                "btnNhan" => trangThai == "Chờ giao",
                "btnChiTiet" => true,
                _ => false
            };

            if (!buttonHienThi)
            {
                e.Handled = true; // không vẽ nút
                return;
            }

            // Phần vẽ nút (Giữ nguyên)
            e.PaintBackground(e.CellBounds, true);

            Rectangle rect = new Rectangle(
                e.CellBounds.X + 4,
                e.CellBounds.Y + 4,
                e.CellBounds.Width - 8,
                e.CellBounds.Height - 8
            );

            Color btnColor = col switch
            {
                "btnNhan" => Color.LimeGreen,
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

            // Cập nhật logic hiển thị (Giống như CellPainting)
            bool buttonHienThi = col switch
            {
                "btnNhan" => trangThai == "Chờ giao",
                "btnChiTiet" => true,
                _ => false
            };

            if (!buttonHienThi) return;

            bool ok = false;
            DialogResult result = DialogResult.None;
            string actionText = "";

            // --- BẮT ĐẦU LOGIC XỬ LÝ NÚT ---

            if (col == "btnNhan")
            {
                actionText = "Đánh dấu đơn hàng này là 'Đã nhận giao'";

                // Hộp thoại xác nhận cho 'Nhận đơn'
                result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn {actionText} không?",
                    "Xác nhận hành động",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    ok = bll.DoiTrangThai(maDH, "Đang giao");
                    bll.setmanv(maDH, 0, Khung.MaTK_temp);
                }
            }
            // BỎ CÁC ĐOẠN ELSE IF CỦA btnHuy, btnChuyen

            else if (col == "btnChiTiet")
            {
                ui.OpenChildForm(new ChiTietDonHang(maDH), NVGIAO.NVG_pn_tab);
                return;
            }

            if (ok)
            {
                MessageBox.Show("Đã nhận đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDonHang();
            }
        }
    }
}
