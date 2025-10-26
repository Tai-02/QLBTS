using QLBTS_BLL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class QLDonHangForm : Form
    {
        private readonly FormDonHangBLL bll = new FormDonHangBLL();

        public QLDonHangForm()
        {
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
            dgvOrders.Columns.Clear();
            DataTable dt = bll.LayDanhSachDonHang();
            dgvOrders.DataSource = dt;

            if (dgvOrders.Columns.Contains("MaDH")) dgvOrders.Columns["MaDH"].HeaderText = "Mã đơn hàng";
            if (dgvOrders.Columns.Contains("Gia")) dgvOrders.Columns["Gia"].HeaderText = "Giá";
            if (dgvOrders.Columns.Contains("TrangThai")) dgvOrders.Columns["TrangThai"].HeaderText = "Trạng thái đơn";

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
            if (e.RowIndex < 0) return;
            if (!dgvOrders.Columns[e.ColumnIndex].Name.StartsWith("btn")) return;

            e.PaintBackground(e.CellBounds, true);
            e.PaintContent(e.CellBounds);

            string col = dgvOrders.Columns[e.ColumnIndex].Name;
            Color btnColor = col switch
            {
                "btnNhan" => Color.LimeGreen,
                "btnHuy" => Color.Red,
                "btnChuyen" => Color.RoyalBlue,
                "btnChiTiet" => Color.Black,
                _ => Color.Black
            };

            using (SolidBrush brush = new SolidBrush(btnColor))
                e.Graphics.FillRectangle(brush, e.CellBounds);

            TextRenderer.DrawText(e.Graphics, (string)e.FormattedValue,
                new Font("Times New Roman", 11, FontStyle.Bold),
                e.CellBounds, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            e.Handled = true;
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            int maDH = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["MaDH"].Value);
            string col = dgvOrders.Columns[e.ColumnIndex].Name;
            string trangThai = dgvOrders.Rows[e.RowIndex].Cells["TrangThai"].Value?.ToString() ?? "";

            try
            {
                bool ok = false;

                if (col == "btnNhan")
                    ok = bll.NhanDonHang(maDH, trangThai);
                else if (col == "btnHuy")
                    ok = bll.HuyDonHang(maDH);
                else if (col == "btnChuyen")
                    ok = bll.ChuyenDonHang(maDH);
                else if (col == "btnChiTiet")
                {
                    string body = bll.TaoNoiDungChiTietDonHang(maDH);
                    MessageBox.Show(body, $"Chi tiết đơn #{maDH}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (ok)
                {
                    MessageBox.Show($"✅ Thao tác với đơn #{maDH} thành công!");
                    LoadDonHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}");
            }
        }
    }
}
