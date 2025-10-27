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

        public Donhangdanhan(int maNVGiaoHang)
        {
            InitializeComponent();
            donHangDAL = new DonHangDAL();
            maNhanVienDangNhap = maNVGiaoHang;
            this.Size = new Size(1000, 600); // Tăng kích thước form cho cân đối
            SetupFormControls();
            LoadDonHangDaNhan();
        }

        public Donhangdanhan() : this(2)
        {
        }

        private void Donhangdanhan_Load(object sender, EventArgs e)
        {
            // Có thể để trống
        }

        // ==========================================================
        // ===        PHƯƠNG THỨC ĐÃ ĐƯỢC VIẾT LẠI HOÀN TOÀN       ===
        // ==========================================================
        private void SetupFormControls()
        {
            // 1. Tính toán vị trí và kích thước mới
            int topSpaceHeight = this.ClientSize.Height / 4; // Chiều cao của 1/4 khoảng trắng ở trên
            int contentY_Start = topSpaceHeight; // Vị trí bắt đầu của nội dung

            int horizontalMargin = 50; // Lề trái và phải
            int titleHeight = 60;
            int spaceBetweenControls = 20;

            // 2. Title Label
            lblTitle = new Label();
            lblTitle.Text = "Đơn hàng đã nhận";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(this.ClientSize.Width, titleHeight);
            lblTitle.Location = new Point(0, contentY_Start); // Đặt tiêu đề ở đầu khu vực 3/4
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; // Cho phép co giãn theo chiều ngang
            this.Controls.Add(lblTitle);

            // 3. DataGridView
            int gridX = horizontalMargin;
            int gridY = contentY_Start + titleHeight + spaceBetweenControls;
            int gridWidth = this.ClientSize.Width - (horizontalMargin * 2);
            int gridHeight = this.ClientSize.Height - gridY - 20; // Chiều cao còn lại trừ đi lề dưới

            dgvOrders = new DataGridView();
            dgvOrders.Location = new Point(gridX, gridY);
            dgvOrders.Size = new Size(gridWidth, gridHeight);
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right; // Co giãn theo cả 4 chiều

            // Các thuộc tính khác giữ nguyên
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.RowHeadersVisible = false;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.BackgroundColor = Color.White;
            dgvOrders.BorderStyle = BorderStyle.FixedSingle;
            dgvOrders.ColumnHeadersHeight = 45;
            dgvOrders.RowTemplate.Height = 55;
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.GridColor = Color.Gainsboro;

            // Styling
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Thêm cột
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaDonHang", HeaderText = "Mã đơn hàng", FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Gia", HeaderText = "Giá", FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewButtonColumn { Name = "GiaoHang", HeaderText = "Giao hàng", UseColumnTextForButtonValue = true, FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewButtonColumn { Name = "ChiTiet", HeaderText = "Chi tiết", UseColumnTextForButtonValue = true, FillWeight = 25 });

            // Gán sự kiện
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
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(33, 150, 243))) // Blue
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
                            LoadDonHangDaNhan();
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
                    ChiTietDonHangForm formChiTiet = new ChiTietDonHangForm(maDonHang);
                    formChiTiet.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở form chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDonHangDaNhan()
        {
            try
            {
                dgvOrders.Rows.Clear();
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
