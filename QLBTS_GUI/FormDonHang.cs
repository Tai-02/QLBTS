using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class QLDonHangForm : Form
    {
        private List<Order> orders = new List<Order>();

        public QLDonHangForm()
        {
            InitializeComponent();
        }

        private void QLDonHangForm_Load(object sender, EventArgs e)
        {
            // Khởi tạo dữ liệu mẫu
            orders.Add(new Order("12345", "100.000đ", "Chờ xác nhận"));
            orders.Add(new Order("1232132", "200.000đ", "Đã xác nhận"));

            SetupDataGridView();
            LoadOrders();
        }

        private void SetupDataGridView()
        {
            dgvOrders.Columns.Clear();
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Cambria", 12, FontStyle.Bold);
            dgvOrders.EnableHeadersVisualStyles = false;

            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.RowTemplate.Height = 50;
            dgvOrders.DefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            dgvOrders.Columns.Add("MaDon", "Mã đơn hàng");
            dgvOrders.Columns.Add("Gia", "Giá");
            dgvOrders.Columns.Add("TrangThai", "Trạng thái đơn");

            var btnNhan = new DataGridViewButtonColumn { HeaderText = "Nhận đơn", Text = "Nhận", UseColumnTextForButtonValue = true };
            var btnHuy = new DataGridViewButtonColumn { HeaderText = "Hủy đơn", Text = "Hủy", UseColumnTextForButtonValue = true };
            var btnChuyen = new DataGridViewButtonColumn { HeaderText = "Chuyển cho giao hàng", Text = "Chuyển", UseColumnTextForButtonValue = true };
            var btnChiTiet = new DataGridViewButtonColumn { HeaderText = "Chi tiết", Text = "Xem chi tiết", UseColumnTextForButtonValue = true };

            dgvOrders.Columns.AddRange(btnNhan, btnHuy, btnChuyen, btnChiTiet);

            dgvOrders.CellFormatting += dgvOrders_CellFormatting;
            dgvOrders.CellClick += dgvOrders_CellClick;
            dgvOrders.CellPainting += dgvOrders_CellPainting;
        }


        private void LoadOrders()
        {
            dgvOrders.Rows.Clear();
            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            foreach (var o in orders)
            {
                dgvOrders.Rows.Add(o.MaDon, o.Gia, o.TrangThai);
            }
            dgvOrders.ClearSelection();
        }

        private void dgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvOrders.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var cell = dgvOrders.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                switch (dgvOrders.Columns[e.ColumnIndex].HeaderText)
                {
                    case "Nhận đơn":
                        cell.Style.BackColor = Color.LightGreen;
                        break;
                    case "Hủy đơn":
                        cell.Style.BackColor = Color.Red;
                        cell.Style.ForeColor = Color.White;
                        break;
                    case "Chuyển cho giao hàng":
                        cell.Style.BackColor = Color.RoyalBlue;
                        cell.Style.ForeColor = Color.White;
                        break;
                    case "Chi tiết":
                        cell.Style.BackColor = Color.Black;
                        cell.Style.ForeColor = Color.White;
                        break;
                }
            }
        }

        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var maDon = dgvOrders.Rows[e.RowIndex].Cells["MaDon"].Value.ToString();
            var order = orders.Find(o => o.MaDon == maDon);

            if (order == null) return;

            string colName = dgvOrders.Columns[e.ColumnIndex].HeaderText;

            switch (colName)
            {
                case "Nhận đơn":
                    order.TrangThai = "Đã xác nhận";
                    MessageBox.Show($"✅ Đơn {order.MaDon} đã được nhận!");
                    break;

                case "Hủy đơn":
                    order.TrangThai = "Đã hủy";
                    MessageBox.Show($"❌ Đơn {order.MaDon} đã bị hủy!");
                    break;

                case "Chuyển cho giao hàng":
                    order.TrangThai = "Đang giao hàng";
                    MessageBox.Show($"🚚 Đơn {order.MaDon} đã chuyển cho giao hàng!");
                    break;

                case "Chi tiết":
                    MessageBox.Show($"📦 Chi tiết đơn {order.MaDon}\nGiá: {order.Gia}\nTrạng thái: {order.TrangThai}", "Thông tin đơn hàng");
                    break;
            }

            LoadOrders();
        }
        private void dgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Kiểm tra nếu là cột nút
            if (dgvOrders.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                e.PaintBackground(e.CellBounds, true);

                Color backColor = Color.White;
                string header = dgvOrders.Columns[e.ColumnIndex].HeaderText;

                switch (header)
                {
                    case "Nhận đơn":
                        backColor = Color.LightGreen;
                        break;
                    case "Hủy đơn":
                        backColor = Color.Red;
                        break;
                    case "Chuyển cho giao hàng":
                        backColor = Color.RoyalBlue;
                        break;
                    case "Chi tiết":
                        backColor = Color.Black;
                        break;
                }

                using (Brush b = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(b, e.CellBounds);
                }

                // Vẽ text
                string text = e.FormattedValue?.ToString();
                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    e.CellStyle.Font,
                    e.CellBounds,
                    (header == "Chi tiết" || header == "Hủy đơn" || header == "Chuyển cho giao hàng") ? Color.White : Color.Black,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                using (Pen pen = new Pen(Color.White, 5))
                {
                    e.Graphics.DrawRectangle(pen, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1);
                }

                e.Handled = true;
            }
        }

    }
}
