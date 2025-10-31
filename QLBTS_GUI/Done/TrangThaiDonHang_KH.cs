using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBTS_BLL;
using QLBTS_DTO;


namespace QLBTS_GUI
{
    public partial class TrangThaiDonHang_KH : Form
    {
        // ===== FIELDS =====
        UI_Form ui = new UI_Form();
        private DonHangBLL donHangBLL = new DonHangBLL();
        private int _maTK;

        // ===== CONSTRUCTOR =====
        public TrangThaiDonHang_KH()
        {
            InitializeComponent();            
            _maTK = Khung.MaTK_temp;

            SetupDataGridView();
            LoadOrders();
        }

        // ===== PRIVATE METHODS =====

        /// <summary>
        /// Setup DataGridView
        /// </summary>
        private void SetupDataGridView()
        {
            dgvOrders.CellClick += DgvOrders_CellClick;
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;
            dgvOrders.CellPainting += DgvOrders_CellPainting;
            //dgvOrders.CellMouseEnter += DgvOrders_CellMouseEnter;
            dgvOrders.CellMouseLeave += DgvOrders_CellMouseLeave;
        }

        /// <summary>
        /// Load đơn hàng của khách
        /// </summary>
        private void LoadOrders()
        {
            try
            {
                if (donHangBLL == null)
                    throw new Exception("donHangBLL chưa được khởi tạo.");

                if (dgvOrders == null)
                    throw new Exception("dgvOrders chưa được khởi tạo.");

                var dh = donHangBLL.LayDonHangTheoKhach(_maTK);

                if (dh == null || dh.Count == 0)
                {
                    dgvOrders.DataSource = null;
                    return;
                }

                RefreshGrid(dh);
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải đơn hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Refresh DataGridView
        /// </summary>
        private void RefreshGrid(List<DonHangDTO> dh)
        {
            if (dh == null)
            {
                dgvOrders.DataSource = null;
                lblTitle.Text = "Trạng thái đơn hàng (0 đơn)";
                return;
            }

            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.DataSource = dh;

            //// Ẩn các cột tự động sinh
            //foreach (DataGridViewColumn col in dgvOrders.Columns)
            //{
            //    if (!col.Name.StartsWith("col"))
            //    {
            //        col.Visible = false;
            //    }
            //}
        }

        /// <summary>
        /// Event: Click vào cell
        /// </summary>
        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            var order = donHangBLL.LayDonHangTheoKhach(_maTK)[e.RowIndex];

            // ✅ XỬ LÝ CỘT "THÔNG TIN ĐƠN" (chứa 2 nút: Đã nhận / Hủy đơn)
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colThongTin")
            {
                // Nếu đang giao → Click vào nút "Đã nhận"
                
                // Nếu chờ xác nhận → Click vào nút "Hủy đơn"
                if (order.TrangThai == "Chờ xác nhận")
                {
                    CancelOrder(order.MaDH);
                }
                if (order.TrangThai == "Đang giao")
                {
                    ConfirmOrderReceived(order.MaDH);
                }
            }
            // XỬ LÝ NÚT "XEM CHI TIẾT"
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "colChiTiet")
            {
                OpenOrderDetail(order.MaDH);
            }
        }

        /// <summary>
        /// Event: Format cell
        /// </summary>
        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Chỉ xử lý cột "Trạng thái"
            if (dgvOrders.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString();

                e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                switch (trangThai)
                {
                    case "Chờ xác nhận":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.BackColor = Color.MistyRose;
                        break;

                    case "Đang giao":
                        e.CellStyle.ForeColor = Color.ForestGreen;
                        e.CellStyle.BackColor = Color.Honeydew;
                        break;

                    case "Đã nhận":
                        e.CellStyle.ForeColor = Color.RoyalBlue;
                        e.CellStyle.BackColor = Color.AliceBlue;
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

        /// <summary>
        /// Event: Custom paint cho button
        /// </summary>
        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var order = donHangBLL.LayDonHangTheoKhach(_maTK)[e.RowIndex];

            // ✅ VẼ CỘT "THÔNG TIN ĐƠN" (chứa 2 loại nút)
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colThongTin")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                // TRƯỜNG HỢP 1: Đơn đang giao → Hiện nút "Đã nhận" (màu xanh lá)
                if (order.TrangThai == "Đang giao")
                {
                    int buttonWidth = e.CellBounds.Width - 20;
                    int buttonHeight = e.CellBounds.Height - 20;
                    int buttonX = e.CellBounds.X + (e.CellBounds.Width - buttonWidth) / 2;
                    int buttonY = e.CellBounds.Y + (e.CellBounds.Height - buttonHeight) / 2;

                    Rectangle buttonRect = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);

                    // Vẽ background màu xanh lá
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(76, 175, 80))) // Green
                    {
                        e.Graphics.FillRectangle(brush, buttonRect);
                    }

                    // Vẽ border
                    using (Pen pen = new Pen(Color.FromArgb(56, 142, 60), 2))
                    {
                        e.Graphics.DrawRectangle(pen, buttonRect);
                    }

                    // Vẽ text "Đã nhận"
                    TextRenderer.DrawText(
                        e.Graphics,
                        "Đã nhận",
                        new Font("Segoe UI", 9F, FontStyle.Bold),
                        buttonRect,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }
                // TRƯỜNG HỢP 2: Đơn chờ xác nhận → Hiện nút "Hủy đơn" (màu đỏ)
                else if (order.TrangThai == "Chờ xác nhận")
                {
                    int buttonWidth = e.CellBounds.Width - 20;
                    int buttonHeight = e.CellBounds.Height - 20;
                    int buttonX = e.CellBounds.X + (e.CellBounds.Width - buttonWidth) / 2;
                    int buttonY = e.CellBounds.Y + (e.CellBounds.Height - buttonHeight) / 2;

                    Rectangle buttonRect = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);

                    // Vẽ background màu đỏ
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(244, 67, 54))) // Red
                    {
                        e.Graphics.FillRectangle(brush, buttonRect);
                    }

                    // Vẽ border
                    using (Pen pen = new Pen(Color.FromArgb(198, 40, 40), 2))
                    {
                        e.Graphics.DrawRectangle(pen, buttonRect);
                    }

                    // Vẽ text "Hủy đơn"
                    TextRenderer.DrawText(
                        e.Graphics,
                        "Hủy đơn",
                        new Font("Segoe UI", 9F, FontStyle.Bold),
                        buttonRect,
                        Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                    );
                }
                // TRƯỜNG HỢP 3: Các trạng thái khác → Để trống
                else
                {
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        e.Graphics.FillRectangle(brush, e.CellBounds);
                    }
                }

                e.Handled = true;
            }
            // VẼ NÚT "XEM CHI TIẾT"
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "colChiTiet")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                int buttonWidth = e.CellBounds.Width - 20;
                int buttonHeight = e.CellBounds.Height - 20;
                int buttonX = e.CellBounds.X + (e.CellBounds.Width - buttonWidth) / 2;
                int buttonY = e.CellBounds.Y + (e.CellBounds.Height - buttonHeight) / 2;

                Rectangle buttonRect = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);

                // Vẽ background màu đen
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.FillRectangle(brush, buttonRect);
                }

                // Vẽ border
                using (Pen pen = new Pen(Color.Gray, 2))
                {
                    e.Graphics.DrawRectangle(pen, buttonRect);
                }

                // Vẽ text
                TextRenderer.DrawText(
                    e.Graphics,
                    "Xem chi tiết",
                    new Font("Segoe UI", 9F, FontStyle.Bold),
                    buttonRect,
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );

                e.Handled = true;
            }
        }

        /// <summary>
        /// Xác nhận đã nhận đơn hàng
        /// </summary>
        private void ConfirmOrderReceived(int maDH)
        {
            try
            {
                var result = MessageBox.Show(
                    "Xác nhận bạn đã nhận đơn hàng này?",
                    "Xác nhận nhận hàng",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    bool success = donHangBLL.DoiTrangThai(maDH, "Hoàn tất");

                    if (success)
                    {
                        MessageBox.Show(
                            "✅ Đã xác nhận nhận hàng thành công!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        LoadOrders(); // Reload lại danh sách
                    }
                    else
                    {
                        ShowError("Không thể cập nhật trạng thái đơn hàng!");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi xác nhận: {ex.Message}");
            }
        }

        /// <summary>
        /// Mở form chi tiết đơn hàng
        /// </summary>
        private void OpenOrderDetail(int maDH)
        {
            try
            {
                ui.OpenChildForm(new ChiTietDonHang(maDH), Khachhang.KH_pn_tab);
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi mở chi tiết: {ex.Message}");
            }
        }

        ///// <summary>
        ///// Event: Đổi con trỏ chuột khi hover vào button
        ///// </summary>
        //private void DgvOrders_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;

        //    string colName = dgvOrders.Columns[e.ColumnIndex].Name;

        //    // Luôn hiện con trỏ Hand cho cột "Xem chi tiết"
        //    if (colName == "colChiTiet")
        //    {
        //        dgvOrders.Cursor = Cursors.Hand;
        //    }
        //    // Hiện con trỏ Hand cho cột "Thông tin đơn" khi có nút
        //    else if (colName == "colThongTin")
        //    {
        //        var order = _filteredOrders[e.RowIndex];

        //        // Có nút "Đã nhận" hoặc "Hủy đơn"
        //        if (order.TrangThai == "Đang giao" || order.TrangThai == "Chờ xác nhận")
        //        {
        //            dgvOrders.Cursor = Cursors.Hand;
        //        }
        //    }
        //}

        private void DgvOrders_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrders.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Hủy đơn hàng
        /// </summary>
        private void CancelOrder(int maDH)
        {
            try
            {
                var result = MessageBox.Show(
                    "Bạn có chắc muốn hủy đơn hàng này?\n\n⚠️ Lưu ý: Chỉ có thể hủy đơn đang ở trạng thái 'Chờ xác nhận'.",
                    "Xác nhận hủy đơn",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    bool success = donHangBLL.HuyDonHang(maDH);

                    if (success)
                    {
                        MessageBox.Show(
                            "✅ Đã hủy đơn hàng thành công!\n\nSố lượng sản phẩm đã được hoàn lại vào kho.",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        LoadOrders(); // Reload lại danh sách
                    }
                    else
                    {
                        ShowError("Không thể hủy đơn hàng!");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi hủy đơn:\n{ex.Message}");
            }
        }
        
        
        // ===== HELPER METHODS =====
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
