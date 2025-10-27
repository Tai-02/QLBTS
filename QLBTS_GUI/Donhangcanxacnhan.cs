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
            this.Size = new Size(1000, 600); // Tăng kích thước form cho cân đối
            SetupFormControls();
            LoadDonHangCanXacNhan();
        }

        // Constructor mặc định (dùng mã NV = 1 tạm thời)
        public Donhangcanxacnhan() : this(1)
        {
        }

        // ==========================================================
        // ===        PHƯƠNG THỨC ĐÃ ĐƯỢC VIẾT LẠI HOÀN TOÀN       ===
        // ==========================================================
        private void SetupFormControls()
        {
            // 1. Tính toán vị trí và kích thước mới
            int topSpaceHeight = this.ClientSize.Height / 4;
            int contentY_Start = topSpaceHeight;

            int horizontalMargin = 50;
            int titleHeight = 60;
            int spaceBetweenControls = 20;

            // 2. Title Label
            lblTitle = new Label();
            lblTitle.Text = "Đơn hàng cần xác nhận";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Size = new Size(this.ClientSize.Width, titleHeight);
            lblTitle.Location = new Point(0, contentY_Start);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.Controls.Add(lblTitle);

            // 3. DataGridView
            int gridX = horizontalMargin;
            int gridY = contentY_Start + titleHeight + spaceBetweenControls;
            int gridWidth = this.ClientSize.Width - (horizontalMargin * 2);
            int gridHeight = this.ClientSize.Height - gridY - 20;

            dgvOrders = new DataGridView();
            dgvOrders.Location = new Point(gridX, gridY);
            dgvOrders.Size = new Size(gridWidth, gridHeight);
            dgvOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

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
            dgvOrders.Columns.Add(new DataGridViewButtonColumn { Name = "NhanDon", HeaderText = "Nhận đơn", UseColumnTextForButtonValue = true, FillWeight = 25 });
            dgvOrders.Columns.Add(new DataGridViewButtonColumn { Name = "ChiTiet", HeaderText = "Chi tiết", UseColumnTextForButtonValue = true, FillWeight = 25 });

            // Gán sự kiện
            dgvOrders.CellContentClick += DgvOrders_CellContentClick;
            dgvOrders.CellPainting += DgvOrders_CellPainting;

            this.Controls.Add(dgvOrders);
        }

        // --- CÁC PHƯƠNG THỨC KHÁC GIỮ NGUYÊN ---

        private void DgvOrders_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvOrders.Columns[e.ColumnIndex].Name == "NhanDon")
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                Rectangle rect = new Rectangle(e.CellBounds.X + 15, e.CellBounds.Y + 12, e.CellBounds.Width - 30, e.CellBounds.Height - 24);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(76, 175, 80))) // Green
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
                TextRenderer.DrawText(e.Graphics, "Nhận", new Font("Segoe UI", 10, FontStyle.Bold), rect, Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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

            if (dgvOrders.Columns[e.ColumnIndex].Name == "NhanDon")
            {
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn nhận đơn hàng #{maDonHang}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool success = donHangDAL.NhanDonHang(maDonHang, maNhanVienDangNhap);
                        if (success)
                        {
                            MessageBox.Show($"Đã nhận đơn hàng #{maDonHang} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDonHangCanXacNhan();
                        }
                        else
                        {
                            MessageBox.Show("Không thể nhận đơn hàng. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string giaFormatted = string.Format("{0:N0}đ", dh.TongTien);
                        dgvOrders.Rows.Add(dh.MaDH, giaFormatted);
                    }
                    if (danhSachDonHang.Count == 0)
                    {
                        MessageBox.Show("Hiện tại không có đơn hàng nào cần xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Donhangcanxacnhan_Load(object sender, EventArgs e)
        {
            // Form đã load dữ liệu trong constructor
        }
    }
}