#nullable disable
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QLBTS_DAL;
using QLBTS_DTO;

namespace QLBTS_GUI
{
    public partial class quanly : Form
    {
        // Controls
        private DataGridView dgvNhanVien;
        private TextBox txtTenNhanVien, txtTenDangNhap, txtMatKhau, txtSoDienThoai, txtGmail;
        private ComboBox cboChucVu;
        private Button btnThem, btnXoa, btnCapNhat;

        // Data Access Layer
        private NhanVienDAL nhanVienDAL;

        public quanly()
        {
            InitializeComponent();
            nhanVienDAL = new NhanVienDAL();
            this.Text = "Quản Lý Nhân Viên";
            this.StartPosition = FormStartPosition.CenterScreen;
            // ✅ Kích thước form theo yêu cầu
            this.Size = new Size(1000, 600);
            this.BackColor = Color.WhiteSmoke;
            SetupControls();
        }

        private void quanly_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        // ==========================================================
        // ===        PHƯƠNG THỨC ĐÃ ĐƯỢC VIẾT LẠI HOÀN TOÀN       ===
        // ==========================================================
        private void SetupControls()
        {
            // 1. Tính toán bố cục chính
            int marginX = 20; // Giảm lề ngang
            float heightRatio = 0.75f;
            int contentHeight = (int)(this.ClientSize.Height * heightRatio);
            int topSpace = this.ClientSize.Height - contentHeight;
            int startY = topSpace;

            // 2. Tạo TableLayoutPanel làm container chính
            TableLayoutPanel mainLayout = new TableLayoutPanel
            {
                Location = new Point(marginX, startY),
                Size = new Size(this.ClientSize.Width - (marginX * 2), contentHeight),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = this.BackColor
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Cột 1 chiếm 50%
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // Cột 2 chiếm 50%
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.Controls.Add(mainLayout);

            // 3. DataGridView bên trái
            dgvNhanVien = new DataGridView
            {
                Dock = DockStyle.Fill, // ✅ Tự động lấp đầy ô của TableLayoutPanel
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                RowTemplate = { Height = 40 },
                ColumnHeadersHeight = 50,
                ColumnHeadersDefaultCellStyle = { Font = new Font("Segoe UI", 14, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter },
                DefaultCellStyle = { Font = new Font("Segoe UI", 13), Alignment = DataGridViewContentAlignment.MiddleLeft }
            };
            dgvNhanVien.Columns.Add("MaNV", "Mã NV");
            dgvNhanVien.Columns.Add("MaTK", "Mã TK");
            dgvNhanVien.Columns.Add("HoTen", "Tên Nhân Viên");
            dgvNhanVien.Columns.Add("VaiTro", "Chức Vụ");
            dgvNhanVien.Columns["MaNV"].Visible = false;
            dgvNhanVien.Columns["MaTK"].Visible = false;
            dgvNhanVien.CellClick += DgvNhanVien_CellClick;
            mainLayout.Controls.Add(dgvNhanVien, 0, 0); // Thêm vào cột 0, dòng 0

            // 4. Panel thông tin bên phải
            Panel infoPanel = new Panel
            {
                Dock = DockStyle.Fill, // ✅ Tự động lấp đầy ô của TableLayoutPanel
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Gainsboro,
                Padding = new Padding(15)
            };
            mainLayout.Controls.Add(infoPanel, 1, 0); // Thêm vào cột 1, dòng 0

            // Các controls bên trong infoPanel
            Label lblTitle = new Label { Text = "Thông Tin Nhân Viên", Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = Color.DarkRed, Dock = DockStyle.Top, TextAlign = ContentAlignment.MiddleCenter, Height = 60 };
            infoPanel.Controls.Add(lblTitle);

            int leftMargin = 20;
            int textBoxWidth = infoPanel.ClientSize.Width - 190;
            int startY_inside = 90;
            int spacing = (infoPanel.ClientSize.Height - 250) / 6;

            txtTenNhanVien = AddLabelAndTextBox("Tên nhân viên:", new Point(leftMargin, startY_inside), textBoxWidth, infoPanel);
            txtTenDangNhap = AddLabelAndTextBox("Tên đăng nhập:", new Point(leftMargin, startY_inside + spacing), textBoxWidth, infoPanel);
            txtMatKhau = AddLabelAndTextBox("Mật khẩu:", new Point(leftMargin, startY_inside + spacing * 2), textBoxWidth, infoPanel);
            txtSoDienThoai = AddLabelAndTextBox("Số điện thoại:", new Point(leftMargin, startY_inside + spacing * 3), textBoxWidth, infoPanel);
            txtGmail = AddLabelAndTextBox("Gmail:", new Point(leftMargin, startY_inside + spacing * 4), textBoxWidth, infoPanel);

            Label lblChucVu = new Label { Text = "Chức vụ:", Font = new Font("Segoe UI", 14), Location = new Point(leftMargin, startY_inside + spacing * 5), AutoSize = true };
            cboChucVu = new ComboBox { Location = new Point(leftMargin + 160, startY_inside + spacing * 5 - 5), Size = new Size(textBoxWidth, 36), Font = new Font("Segoe UI", 14), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            cboChucVu.Items.AddRange(new string[] { "NhanVien", "GiaoHang", "Admin" });
            infoPanel.Controls.Add(lblChucVu);
            infoPanel.Controls.Add(cboChucVu);

            int btnWidth = 130;
            int btnGap = (infoPanel.ClientSize.Width - (btnWidth * 3)) / 4;

            btnThem = CreateStandardButton("Thêm", new Point(btnGap, 0), btnWidth, Color.FromArgb(76, 175, 80), btnThem_Click);
            btnXoa = CreateStandardButton("Xóa", new Point(btnGap * 2 + btnWidth, 0), btnWidth, Color.FromArgb(244, 67, 54), btnXoa_Click);
            btnCapNhat = CreateStandardButton("Cập nhật", new Point(btnGap * 3 + btnWidth * 2, 0), btnWidth, Color.FromArgb(33, 150, 243), btnCapNhat_Click);

            // Neo các nút vào cạnh dưới của panel
            btnThem.Anchor = btnXoa.Anchor = btnCapNhat.Anchor = AnchorStyles.Bottom;
            btnThem.Top = btnXoa.Top = btnCapNhat.Top = infoPanel.ClientSize.Height - 70;

            infoPanel.Controls.Add(btnThem);
            infoPanel.Controls.Add(btnXoa);
            infoPanel.Controls.Add(btnCapNhat);
        }

        // --- Hàm hỗ trợ ---
        private TextBox AddLabelAndTextBox(string labelText, Point location, int width, Control parent)
        {
            Label lbl = new Label { Text = labelText, Font = new Font("Segoe UI", 14), Location = location, AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(location.X + 160, location.Y - 5), Size = new Size(width, 36), Font = new Font("Segoe UI", 14), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
            parent.Controls.Add(lbl);
            parent.Controls.Add(txt);
            return txt;
        }

        private Button CreateStandardButton(string text, Point location, int width, Color color, EventHandler clickEvent)
        {
            Button btn = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(width, 60),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += clickEvent;
            return btn;
        }

        // --- Các hàm chức năng và sự kiện (giữ nguyên) ---
        private void LoadNhanVien()
        {
            try
            {
                dgvNhanVien.Rows.Clear();
                List<NhanVienDTO> danhSach = nhanVienDAL.GetTatCaNhanVien();
                foreach (var nv in danhSach)
                {
                    dgvNhanVien.Rows.Add(nv.MaNV, nv.MaTK, nv.HoTen, nv.VaiTro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtTenNhanVien.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtSoDienThoai.Clear();
            txtGmail.Clear();
            cboChucVu.SelectedIndex = -1;
            dgvNhanVien.ClearSelection();
        }

        private void DgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                int maNV = Convert.ToInt32(row.Cells["MaNV"].Value);

                var nv = nhanVienDAL.GetTatCaNhanVien().Find(n => n.MaNV == maNV);
                if (nv != null)
                {
                    txtTenNhanVien.Text = nv.HoTen;
                    txtTenDangNhap.Text = nv.TenDangNhap;
                    txtMatKhau.Text = nv.MatKhau;
                    txtSoDienThoai.Text = nv.SDT;
                    txtGmail.Text = nv.Email;
                    cboChucVu.SelectedItem = nv.VaiTro;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text) || cboChucVu.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NhanVienDTO nv = new NhanVienDTO { HoTen = txtTenNhanVien.Text, TenDangNhap = txtTenDangNhap.Text, MatKhau = txtMatKhau.Text, SDT = txtSoDienThoai.Text, Email = txtGmail.Text, VaiTro = cboChucVu.SelectedItem.ToString() };
                if (nhanVienDAL.ThemNhanVien(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVien();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm nhân viên thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            int maTK = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaTK"].Value);
            string tenNV = dgvNhanVien.SelectedRows[0].Cells["HoTen"].Value.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{tenNV}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (nhanVienDAL.XoaNhanVien(maTK))
                    {
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhanVien();
                        ClearFields();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Xóa nhân viên thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0) { MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text) || cboChucVu.SelectedItem == null) { MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                NhanVienDTO nv = new NhanVienDTO { MaNV = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value), MaTK = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaTK"].Value), HoTen = txtTenNhanVien.Text, TenDangNhap = txtTenDangNhap.Text, MatKhau = txtMatKhau.Text, SDT = txtSoDienThoai.Text, Email = txtGmail.Text, VaiTro = cboChucVu.SelectedItem.ToString() };
                if (nhanVienDAL.CapNhatNhanVien(nv))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVien();
                    ClearFields();
                }
            }
            catch (Exception ex) { MessageBox.Show("Cập nhật thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
