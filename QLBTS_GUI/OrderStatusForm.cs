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
    public partial class OrderStatusForm : Form
    {
        // ===== FIELDS =====
        private List<OrderListViewModel> _allOrders;
        private List<OrderListViewModel> _filteredOrders;
        private OrderBLL _orderBLL;
        private int _maTK;

        // ===== CONSTRUCTOR =====
        public OrderStatusForm(int maTK)
        {
            InitializeComponent();
            _orderBLL = new OrderBLL();
            _maTK = maTK;

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
            dgvOrders.CellMouseEnter += DgvOrders_CellMouseEnter;
            dgvOrders.CellMouseLeave += DgvOrders_CellMouseLeave;
        }

        /// <summary>
        /// Load đơn hàng của khách
        /// </summary>
        private void LoadOrders()
        {
            try
            {
                _allOrders = _orderBLL.GetOrdersByCustomer(_maTK);
                _filteredOrders = new List<OrderListViewModel>(_allOrders);
                RefreshGrid();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải đơn hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Refresh DataGridView
        /// </summary>
        private void RefreshGrid()
        {
            if (_filteredOrders == null || !_filteredOrders.Any())
            {
                dgvOrders.DataSource = null;
                lblTitle.Text = "Trạng thái đơn hàng (0 đơn)";
                return;
            }

            dgvOrders.AutoGenerateColumns = false;
            var bindingList = new BindingList<OrderListViewModel>(_filteredOrders);
            dgvOrders.DataSource = bindingList;

            // ✅ ĐẢM BẢO THỨ TỰ CỘT
            dgvOrders.Columns["colMaDH"].DisplayIndex = 0;
            dgvOrders.Columns["colGia"].DisplayIndex = 1;
            dgvOrders.Columns["colTrangThai"].DisplayIndex = 2;
            dgvOrders.Columns["colThongTin"].DisplayIndex = 3;
            dgvOrders.Columns["colChiTiet"].DisplayIndex = 4;

            // Ẩn các cột tự động sinh
            foreach (DataGridViewColumn col in dgvOrders.Columns)
            {
                if (!col.Name.StartsWith("col"))
                {
                    col.Visible = false;
                }
            }

            lblTitle.Text = $"Trạng thái đơn hàng ({_filteredOrders.Count} đơn)";
        }

        /// <summary>
        /// Event: Click vào cell
        /// </summary>
        private void DgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var order = _filteredOrders[e.RowIndex];

            // ✅ XỬ LÝ NÚT "ĐÃ NHẬN"
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colThongTin")
            {
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
            // Tô màu cột "Trạng thái"
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colTrangThai")
            {
                if (e.RowIndex >= 0 && e.RowIndex < _filteredOrders.Count)
                {
                    var order = _filteredOrders[e.RowIndex];
                    e.CellStyle.ForeColor = order.ColorTrangThai;
                    e.CellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                }
            }
        }

        /// <summary>
        /// Event: Custom paint cho button
        /// </summary>
        /// <summary>
        /// Event: Custom paint cho button
        /// </summary>
        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var order = _filteredOrders[e.RowIndex];

            // ✅ VẼ NÚT "ĐÃ NHẬN"
            if (dgvOrders.Columns[e.ColumnIndex].Name == "colThongTin")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                if (order.TrangThai == "Đang giao")
                {
                    // Tính toán vị trí nút
                    int buttonWidth = e.CellBounds.Width - 20;
                    int buttonHeight = e.CellBounds.Height - 20;
                    int buttonX = e.CellBounds.X + (e.CellBounds.Width - buttonWidth) / 2;
                    int buttonY = e.CellBounds.Y + (e.CellBounds.Height - buttonHeight) / 2;

                    Rectangle buttonRect = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);

                    // Vẽ background nút màu đỏ
                    using (SolidBrush brush = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(brush, buttonRect);
                    }

                    // Vẽ border
                    using (Pen pen = new Pen(Color.DarkRed, 2))
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
                else
                {
                    // Nếu không phải "Đang giao", để trống
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

                // Vẽ background nút màu đen
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
        /// ✅ Xác nhận đã nhận đơn hàng
        /// </summary>
        private void ConfirmOrderReceived(int maDH)
        {
            try
            {
                var result = MessageBox.Show(
                    "Xác nhận bạn đã nhận đơn hàng này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    bool success = _orderBLL.UpdateOrderStatus(maDH, "Đã giao");

                    if (success)
                    {
                        MessageBox.Show(
                            "Đã xác nhận nhận hàng thành công!",
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
                OrderDetailForm detailForm = new OrderDetailForm();
                detailForm.LoadOrderDetail(maDH);
                detailForm.ShowDialog();

                LoadOrders();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi mở chi tiết: {ex.Message}");
            }
        }

        /// <summary>
        /// Event: Đổi con trỏ chuột khi hover vào button
        /// </summary>
        private void DgvOrders_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dgvOrders.Columns[e.ColumnIndex].Name;

            if (colName == "colChiTiet")
            {
                dgvOrders.Cursor = Cursors.Hand;
            }
            else if (colName == "colThongTin")
            {
                var order = _filteredOrders[e.RowIndex];
                if (order.TrangThai == "Đang giao")
                {
                    dgvOrders.Cursor = Cursors.Hand;
                }
            }
        }

        private void DgvOrders_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            dgvOrders.Cursor = Cursors.Default;
        }
        // ===== HELPER METHODS =====

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
