using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using QLBTS_DTO;
using QLBTS_DAL;

namespace QLBTS_GUI
{
    public partial class Donhangcanxacnhan : Form
    {
        private DataGridView? dgvOrders;
        private Label? lblTitle;
        private DonHangDAL donHangDAL;
        private int maNhanVienDangNhap; // Mã nhân viên đang đăng nhập

        // Constructor với mã nhân viên
        public Donhangcanxacnhan(int maNV)
        {
            InitializeComponent();
            donHangDAL = new DonHangDAL();
            maNhanVienDangNhap = maNV;
            SetupFormControls();
            LoadDonHangCanXacNhan();
        }

        // Constructor mặc định (dùng mã NV = 1 tạm thời)
        public Donhangcanxacnhan() : this(1)
        {
        }

        private void SetupFormControls()
        {
            // Title Label
            lblTitle = new Label();
            lblTitle.Text = "Đơn hàng cần xác nhận";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(this.ClientSize.Width, 50);
            lblTitle.Location = new Point(0, 80);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTitle);

            // DataGridView - căn giữa form
            int tableWidth = 850;
            int tableHeight = 400;
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
            dgvOrders.ReadOnly = false;
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
            DataGridViewTextBoxColumn colMaDonHang = new DataGridViewTextBoxColumn();
            colMaDonHang.Name = "MaDonHang";
            colMaDonHang.HeaderText = "Mã đơn hàng";
            colMaDonHang.ReadOnly = true;
            colMaDonHang.FillWeight = 25;
            dgvOrders.Columns.Add(colMaDonHang);

            DataGridViewTextBoxColumn colGia = new DataGridViewTextBoxColumn();
            colGia.Name = "Gia";
            colGia.HeaderText = "Giá";
            colGia.ReadOnly = true;
            colGia.FillWeight = 25;
            dgvOrders.Columns.Add(colGia);

            // Button column for "Nhận đơn"
            DataGridViewButtonColumn btnNhanDon = new DataGridViewButtonColumn();
            btnNhanDon.Name = "NhanDon";
            btnNhanDon.HeaderText = "Nhận đơn";
            btnNhanDon.Text = "Nhận";
            btnNhanDon.UseColumnTextForButtonValue = true;
            btnNhanDon.FillWeight = 25;
            dgvOrders.Columns.Add(btnNhanDon);

            // Button column for "Chi tiết"
            DataGridViewButtonColumn btnChiTiet = new DataGridViewButtonColumn();
            btnChiTiet.Name = "ChiTiet";
            btnChiTiet.HeaderText = "Chi tiết";
            btnChiTiet.Text = "Xem chi tiết";
            btnChiTiet.UseColumnTextForButtonValue = true;
            btnChiTiet.FillWeight = 25;
            dgvOrders.Columns.Add(btnChiTiet);

            // Event handlers
            dgvOrders.CellContentClick += DgvOrders_CellContentClick;
            dgvOrders.CellPainting += DgvOrders_CellPainting;

            this.Controls.Add(dgvOrders);
        }

        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Custom painting for buttons
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dgvOrders.Columns[e.ColumnIndex].Name == "NhanDon")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    // Draw green button
                    Rectangle rect = new Rectangle(
                        e.CellBounds.X + 15,
                        e.CellBounds.Y + 12,
                        e.CellBounds.Width - 30,
                        e.CellBounds.Height - 24
                    );

                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(76, 175, 80)))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }

                    TextRenderer.DrawText(e.Graphics, "Nhận",
                        new Font("Segoe UI", 10, FontStyle.Bold),
                        rect, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                    e.Handled = true;
                }
                else if (dgvOrders.Columns[e.ColumnIndex].Name == "ChiTiet")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    // Draw black button
                    Rectangle rect = new Rectangle(
                        e.CellBounds.X + 15,
                        e.CellBounds.Y + 12,
                        e.CellBounds.Width - 30,
                        e.CellBounds.Height - 24
                    );

                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }

                    TextRenderer.DrawText(e.Graphics, "Xem chi tiết",
                        new Font("Segoe UI", 10, FontStyle.Bold),
                        rect, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

                    e.Handled = true;
                }
            }
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy mã đơn hàng từ dòng được chọn
            int maDonHang = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["MaDonHang"].Value);

            if (dgvOrders.Columns[e.ColumnIndex].Name == "NhanDon")
            {
                // Xử lý nhận đơn hàng
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn nhận đơn hàng #{maDonHang}?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool success = donHangDAL.NhanDonHang(maDonHang, maNhanVienDangNhap);

                        if (success)
                        {
                            MessageBox.Show(
                                $"Đã nhận đơn hàng #{maDonHang} thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            // Tải lại danh sách
                            LoadDonHangCanXacNhan();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Không thể nhận đơn hàng. Vui lòng thử lại!",
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Lỗi: " + ex.Message,
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else if (dgvOrders.Columns[e.ColumnIndex].Name == "ChiTiet")
            {
                // Mở form chi tiết sản phẩm
                try
                {
                    // Tạm thời hiển thị thông báo, sau này sẽ mở form ChiTietSanPham
                    DonHangDTO donHang = donHangDAL.GetDonHangById(maDonHang);
                    List<ChiTietDonHangDTO> chiTiet = donHangDAL.GetChiTietDonHang(maDonHang);

                    string thongTin = $"Đơn hàng #{maDonHang}\n";
                    thongTin += $"Khách hàng: {donHang.TenKhachHang}\n";
                    thongTin += $"SĐT: {donHang.SDTKhachHang}\n";
                    thongTin += $"Địa chỉ: {donHang.DiaChiKhachHang}\n";
                    thongTin += $"Tổng tiền: {string.Format("{0:N0}đ", donHang.TongTien)}\n\n";
                    thongTin += "Chi tiết sản phẩm:\n";

                    foreach (var ct in chiTiet)
                    {
                        // ==========================================================
                        // === DÒNG CODE ĐÃ ĐƯỢC SỬA LẠI ĐỂ TÍNH TOÁN CHO ĐÚNG ===
                        // ==========================================================
                        int thanhTienSanPham = ct.SoLuong * ct.DonGia;
                        thongTin += $"- {ct.TenSP} (Size {ct.Size}): {ct.SoLuong} x {string.Format("{0:N0}đ", ct.DonGia)} = {string.Format("{0:N0}đ", thanhTienSanPham)}\n";
                    }

                    MessageBox.Show(thongTin, "Chi tiết đơn hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // TODO: Sau này thay bằng:
                    // ChiTietSanPham formChiTiet = new ChiTietSanPham(maDonHang);
                    // formChiTiet.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Lỗi khi xem chi tiết: " + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // Load dữ liệu đơn hàng từ CSDL
        private void LoadDonHangCanXacNhan()
        {
            try
            {
                if (dgvOrders != null)
                {
                    dgvOrders.Rows.Clear();
                    List<DonHangDTO> danhSachDonHang = donHangDAL.GetDonHangCanXacNhan();

                    foreach (DonHangDTO dh in danhSachDonHang)
                    {
                        // Format giá tiền
                        string giaFormatted = string.Format("{0:N0}đ", dh.TongTien);
                        dgvOrders.Rows.Add(dh.MaDH, giaFormatted);
                    }

                    if (danhSachDonHang.Count == 0)
                    {
                        MessageBox.Show(
                            "Hiện tại không có đơn hàng nào cần xác nhận!",
                            "Thông báo",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Donhangcanxacnhan_Load(object sender, EventArgs e)
        {
            // Form đã load dữ liệu trong constructor
        }
    }
}