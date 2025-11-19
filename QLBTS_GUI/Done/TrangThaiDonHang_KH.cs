using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QLBTS_BBL;
using QLBTS_BLL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class TrangThaiDonHang_KH : Form
    {
        private UI_Form ui = new UI_Form();
        private DonHangBLL donHangBLL = new DonHangBLL();
        private int _maTK;

        public TrangThaiDonHang_KH()
        {
            InitializeComponent();
            _maTK = Khung.MaTK_temp;

            SetupDataGridView();
            LoadOrders();
        }

        // ======================= CẤU HÌNH DGV =======================
        private void SetupDataGridView()
        {
            dgvOrders.CellClick += DgvOrders_CellClick;
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;
            dgvOrders.CellPainting += DgvOrders_CellPainting;
            dgvOrders.CellMouseLeave += DgvOrders_CellMouseLeave;
        }

        // ======================= TẢI DỮ LIỆU =======================
        private void LoadOrders()
        {
            try
            {
                var dh = donHangBLL.LayDonHangTheoKhach(_maTK);

                if (dh == null || dh.Count == 0)
                {
                    dgvOrders.DataSource = null;
                    lblTitle.Text = "Trạng thái đơn hàng (0 đơn)";
                    return;
                }

                dgvOrders.AutoGenerateColumns = false;
                dgvOrders.DataSource = dh;
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải đơn hàng: " + ex.Message);
            }
        }

        // ======================= SỰ KIỆN CLICK =======================
        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var orders = donHangBLL.LayDonHangTheoKhach(_maTK);
            if (orders == null || e.RowIndex >= orders.Count) return;

            var order = orders[e.RowIndex];

            string col = dgvOrders.Columns[e.ColumnIndex].Name;

            // 🔹 Cột “Thông tin đơn” → gồm các nút hành động
            if (col == "colThongTin")
            {
                string tt = order.TrangThai?.Trim();

                // Hủy đơn khi trạng thái là “Chờ xác nhận”
                if (tt.Equals("Chờ xác nhận", StringComparison.OrdinalIgnoreCase))
                {
                    CancelOrder(order.MaDH);
                }

                // Xác nhận đã nhận hàng khi trạng thái là “Đã giao”
                else if (tt.Equals("Đã giao", StringComparison.OrdinalIgnoreCase))
                {
                    ConfirmOrderReceived(order.MaDH);
                }
            }
            // 🔹 Cột “Chi tiết”
            else if (col == "colChiTiet")
            {
                OpenOrderDetail(order.MaDH);
            }
        }

        // ======================= FORMAT MÀU TRẠNG THÁI =======================
        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvOrders.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string tt = e.Value.ToString().Trim();

                e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                switch (tt)
                {
                    case "Chờ xác nhận":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.BackColor = Color.MistyRose;
                        break;
                    case "Đang giao":
                        e.CellStyle.ForeColor = Color.ForestGreen;
                        e.CellStyle.BackColor = Color.Honeydew;
                        break;
                    case "Đã giao":
                        e.CellStyle.ForeColor = Color.MediumBlue;
                        e.CellStyle.BackColor = Color.LightCyan;
                        break;
                    case "Hoàn tất":
                        e.CellStyle.ForeColor = Color.DarkGreen;
                        e.CellStyle.BackColor = Color.Honeydew;
                        break;
                    case "Đã hủy":
                        e.CellStyle.ForeColor = Color.Gray;
                        e.CellStyle.BackColor = Color.Gainsboro;
                        break;
                    default:
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.BackColor = Color.White;
                        break;
                }
            }
        }

        // ======================= VẼ NÚT TRONG DGV =======================
        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var orders = donHangBLL.LayDonHangTheoKhach(_maTK);
            if (orders == null || e.RowIndex >= orders.Count) return;

            var order = orders[e.RowIndex];
            string tt = order.TrangThai?.Trim();

            // 🔹 Vẽ cột “Thông tin đơn”
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colThongTin")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                Rectangle rect = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y + 5,
                                               e.CellBounds.Width - 10, e.CellBounds.Height - 10);

                if (tt.Equals("Đã giao", StringComparison.OrdinalIgnoreCase))
                {
                    // Nút “Đã nhận” (màu xanh lá)
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(76, 175, 80)))
                        e.Graphics.FillRectangle(brush, rect);
                    using (Pen pen = new Pen(Color.FromArgb(56, 142, 60), 2))
                        e.Graphics.DrawRectangle(pen, rect);

                    TextRenderer.DrawText(e.Graphics, "Đã nhận",
                        new Font("Segoe UI", 9F, FontStyle.Bold),
                        rect, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                else if (tt.Equals("Chờ xác nhận", StringComparison.OrdinalIgnoreCase))
                {
                    // Nút “Hủy đơn” (màu đỏ)
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(244, 67, 54)))
                        e.Graphics.FillRectangle(brush, rect);
                    using (Pen pen = new Pen(Color.FromArgb(198, 40, 40), 2))
                        e.Graphics.DrawRectangle(pen, rect);

                    TextRenderer.DrawText(e.Graphics, "Hủy đơn",
                        new Font("Segoe UI", 9F, FontStyle.Bold),
                        rect, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                else
                {
                    // Trạng thái khác → để trống
                    using (SolidBrush brush = new SolidBrush(Color.White))
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                }

                e.Handled = true;
            }

            // 🔹 Vẽ nút “Xem chi tiết”
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "colChiTiet")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);
                Rectangle rect = new Rectangle(e.CellBounds.X + 5, e.CellBounds.Y + 5,
                                               e.CellBounds.Width - 10, e.CellBounds.Height - 10);

                using (SolidBrush brush = new SolidBrush(Color.Black))
                    e.Graphics.FillRectangle(brush, rect);
                using (Pen pen = new Pen(Color.Gray, 2))
                    e.Graphics.DrawRectangle(pen, rect);

                TextRenderer.DrawText(e.Graphics, "Xem chi tiết",
                    new Font("Segoe UI", 9F, FontStyle.Bold),
                    rect, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                e.Handled = true;
            }
        }

        // ======================= XỬ LÝ NÚT “ĐÃ NHẬN” =======================
        private void ConfirmOrderReceived(int maDH)
        {
            try
            {
                if (MessageBox.Show("Xác nhận bạn đã nhận đơn hàng này?",
                    "Xác nhận nhận hàng", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool ok = donHangBLL.DoiTrangThai(maDH, "Hoàn tất");
                    if (ok)
                    {
                        MessageBox.Show("✅ Đã xác nhận nhận hàng thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LichSuHoatDongBBL ls = new LichSuHoatDongBBL();
                        ls.ThemLichSu(Khung.MaTK_temp, maDH,
                            "Bạn đã gian hàng thành công",
                            Convert.ToInt32(donHangBLL.TinhTongTienDonHang(maDH)[1]), "Giaohang");

                        LoadOrders();
                    }
                    else ShowError("Không thể cập nhật trạng thái đơn hàng!");
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi xác nhận: " + ex.Message);
            }
        }

        // ======================= XỬ LÝ HỦY ĐƠN =======================
        private void CancelOrder(int maDH)
        {
            try
            {
                if (MessageBox.Show(
                    "Bạn có chắc muốn hủy đơn hàng này?\n⚠️ Chỉ hủy được đơn 'Chờ xác nhận'.",
                    "Xác nhận hủy đơn", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bool ok = donHangBLL.HuyDonHang(maDH);
                    if (ok)
                    {
                        MessageBox.Show("✅ Đã hủy đơn hàng thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrders();
                    }
                    else ShowError("Không thể hủy đơn hàng!");
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi hủy đơn: " + ex.Message);
            }
        }

        // ======================= MỞ FORM CHI TIẾT =======================
        private void OpenOrderDetail(int maDH)
        {
            try
            {
                ui.OpenChildForm(new ChiTietDonHang(maDH), Khachhang.KH_pn_tab);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi mở chi tiết: " + ex.Message);
            }
        }

        private void DgvOrders_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrders.Cursor = Cursors.Default;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
