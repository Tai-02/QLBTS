#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QLBTS_DAL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class Donhangdanhan : Form
    {
        private DataGridView dgvOrders;
        private Label lblTitle;
        private DonHangDAL donHangDAL;
        private int maNhanVienDangNhap; // Dùng để gán cho nhân viên giao hàng

        // Constructor để nhận mã nhân viên giao hàng
        public Donhangdanhan(int maNVGiaoHang)
        {
            InitializeComponent();
            donHangDAL = new DonHangDAL();
            maNhanVienDangNhap = maNVGiaoHang; // Giả sử người đăng nhập là nhân viên giao hàng
            SetupFormControls();
            LoadDonHangDaNhan();
        }

        // Constructor mặc định để test (dùng mã NV giao hàng = 2 từ data mẫu)
        public Donhangdanhan() : this(2)
        {
        }
        // Dán đoạn code này vào cuối file Donhangdanhan.cs

        private void Donhangdanhan_Load(object sender, EventArgs e)
        {
            // Phương thức này có thể để trống, miễn là nó tồn tại
            // vì form designer đang tìm nó.
        }
        private void SetupFormControls()
        {
            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "Đơn hàng đã nhận";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(this.ClientSize.Width, 50);
            lblTitle.Location = new Point(0, 80);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitle);

            // DataGridView - căn giữa form
            int tableWidth = 850;
            int tableHeight = 400; // Tăng chiều cao để chứa nhiều đơn hơn
            int tableX = (this.ClientSize.Width - tableWidth) / 2;
            int tableY = 180;

            dgvOrders = new DataGridView();
            dgvOrders.Location = new Point(tableX, tableY);
            dgvOrders.Size = new Size(tableWidth, tableHeight);
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AllowUserToResizeRows = false;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.FixedSingle;
            dgvOrders.ColumnHeadersHeight = 45;
            dgvOrders.RowTemplate.Height = 55;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ReadOnly = true; // Dữ liệu chỉ để xem
            dgvOrders.GridColor = Color.Black;

            // Column header style
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Row style
            dgvOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 230);
            dgvOrders.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvOrders.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);

            // Add columns
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaDonHang", HeaderText = "Mã đơn hàng", FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Gia", HeaderText = "Giá", FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewButtonColumn { Name = "GiaoHang", HeaderText = "Giao hàng", Text = "Giao hàng", UseColumnTextForButtonValue = true, FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewButtonColumn { Name = "ChiTiet", HeaderText = "Chi tiết", Text = "Xem chi tiết", UseColumnTextForButtonValue = true, FillWeight = 25 });

            // Event handlers
            dgvOrders.CellContentClick += DgvOrders_CellContentClick;
            dgvOrders.CellPainting += DgvOrders_CellPainting;

            this.Controls.Add(dgvOrders);
        }

        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvOrders.Columns[e.ColumnIndex].Name == "GiaoHang")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                Rectangle rect = new Rectangle(e.CellBounds.X + 15, e.CellBounds.Y + 12, e.CellBounds.Width - 30, e.CellBounds.Height - 24);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(33, 150, 243))) // Blue color
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
                TextRenderer.DrawText(e.Graphics, "Giao hàng", new Font("Segoe UI", 10, FontStyle.Bold), rect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "ChiTiet")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                Rectangle rect = new Rectangle(e.CellBounds.X + 15, e.CellBounds.Y + 12, e.CellBounds.Width - 30, e.CellBounds.Height - 24);
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
                TextRenderer.DrawText(e.Graphics, "Xem chi tiết", new Font("Segoe UI", 10, FontStyle.Bold), rect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                e.Handled = true;
            }
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int maDonHang = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["MaDonHang"].Value);

            if (dgvOrders.Columns[e.ColumnIndex].Name == "GiaoHang")
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn giao đơn hàng #{maDonHang}?", "Xác nhận giao hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool success = donHangDAL.GiaoDonHang(maDonHang, maNhanVienDangNhap);
                        if (success)
                        {
                            MessageBox.Show("Đã cập nhật trạng thái giao hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonHangDaNhan(); // Tải lại danh sách, đơn hàng vừa giao sẽ biến mất
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại, đơn hàng có thể đã được xử lý.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "ChiTiet")
            {
                try
                {
                    DonHangDTO donHang = donHangDAL.GetDonHangById(maDonHang);
                    List<ChiTietDonHangDTO> chiTiet = donHangDAL.GetChiTietDonHang(maDonHang);

                    string thongTin = $"Đơn hàng #{maDonHang}\n" +
                                      $"Khách hàng: {donHang.TenKhachHang}\n" +
                                      $"SĐT: {donHang.SDTKhachHang}\n" +
                                      $"Địa chỉ: {donHang.DiaChiKhachHang}\n" +
                                      $"Tổng tiền: {string.Format("{0:N0}đ", donHang.TongTien)}\n\n" +
                                      "Chi tiết sản phẩm:\n";

                    foreach (var ct in chiTiet)
                    {
                        int thanhTienSanPham = ct.SoLuong * ct.DonGia;
                        thongTin += $"- {ct.TenSP} (Size {ct.Size}): {ct.SoLuong} x {string.Format("{0:N0}đ", ct.DonGia)} = {string.Format("{0:N0}đ", thanhTienSanPham)}\n";
                    }

                    MessageBox.Show(thongTin, "Chi tiết đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xem chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDonHangDaNhan()
        {
            try
            {
                dgvOrders.Rows.Clear();
                // Lấy các đơn hàng đã được nhân viên tại quầy xác nhận
                List<DonHangDTO> danhSachDonHang = donHangDAL.GetDonHangDaNhan();

                foreach (DonHangDTO dh in danhSachDonHang)
                {
                    dgvOrders.Rows.Add(dh.MaDH, string.Format("{0:N0}đ", dh.TongTien));
                }

                if (danhSachDonHang.Count == 0)
                {
                    lblTitle.Text = "Không có đơn hàng nào chờ giao";
                }
                else
                {
                    lblTitle.Text = "Đơn hàng đã nhận";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}