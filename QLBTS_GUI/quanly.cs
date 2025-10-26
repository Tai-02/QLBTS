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
            this.Size = new Size(1200, 700);
            this.BackColor = Color.WhiteSmoke;
            SetupControls();
        }

        private void quanly_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void SetupControls()
        {
            // Tính toán bố cục
            int marginX = 50;
            int totalWidth = this.ClientSize.Width - (marginX * 2);
            int startX = marginX;
            int gap = 20;
            int panelWidth = (totalWidth - gap) / 2;
            float heightRatio = 0.75f;
            int totalHeight = (int)(this.ClientSize.Height * heightRatio);
            int bottomMargin = 20;
            int startY = this.ClientSize.Height - totalHeight - bottomMargin;

            // DataGridView bên trái
            dgvNhanVien = new DataGridView
            {
                Location = new Point(startX, startY),
                Size = new Size(panelWidth, totalHeight),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                // ✅ Tăng chiều cao dòng và font chữ
                RowTemplate = { Height = 40 },
                ColumnHeadersHeight = 50,
                ColumnHeadersDefaultCellStyle =
                {
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle =
                {
                    Font = new Font("Segoe UI", 13),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };

            dgvNhanVien.Columns.Add("MaNV", "Mã NV");
            dgvNhanVien.Columns.Add("MaTK", "Mã TK");
            dgvNhanVien.Columns.Add("HoTen", "Tên Nhân Viên");
            dgvNhanVien.Columns.Add("VaiTro", "Chức Vụ");
            dgvNhanVien.Columns["MaNV"].Visible = false;
            dgvNhanVien.Columns["MaTK"].Visible = false;
            dgvNhanVien.CellClick += DgvNhanVien_CellClick;
            this.Controls.Add(dgvNhanVien);

            // Panel thông tin bên phải
            Panel infoPanel = new Panel
            {
                Location = new Point(startX + panelWidth + gap, startY),
                Size = new Size(panelWidth, totalHeight),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Gainsboro
            };
            this.Controls.Add(infoPanel);

            // Các controls bên trong infoPanel
            // ✅ Tăng cỡ chữ tiêu đề
            Label lblTitle = new Label { Text = "Thông Tin Nhân Viên", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.DarkRed, Location = new Point(0, 30), Size = new Size(infoPanel.Width, 45), TextAlign = ContentAlignment.MiddleCenter };
            infoPanel.Controls.Add(lblTitle);

            int leftMargin = 40;
            int textBoxWidth = infoPanel.Width - 210;
            int startY_inside = 120;
            int spacing = (infoPanel.Height - 320) / 5;

            txtTenNhanVien = AddLabelAndTextBox("Tên nhân viên:", new Point(leftMargin, startY_inside), textBoxWidth, infoPanel);
            txtTenDangNhap = AddLabelAndTextBox("Tên đăng nhập:", new Point(leftMargin, startY_inside + spacing), textBoxWidth, infoPanel);
            txtMatKhau = AddLabelAndTextBox("Mật khẩu:", new Point(leftMargin, startY_inside + spacing * 2), textBoxWidth, infoPanel);
            txtSoDienThoai = AddLabelAndTextBox("Số điện thoại:", new Point(leftMargin, startY_inside + spacing * 3), textBoxWidth, infoPanel);
            txtGmail = AddLabelAndTextBox("Gmail:", new Point(leftMargin, startY_inside + spacing * 4), textBoxWidth, infoPanel);

            // ✅ Tăng cỡ chữ Label và ComboBox
            Label lblChucVu = new Label { Text = "Chức vụ:", Font = new Font("Segoe UI", 14), Location = new Point(leftMargin, startY_inside + spacing * 5), AutoSize = true };
            cboChucVu = new ComboBox { Location = new Point(leftMargin + 160, startY_inside + spacing * 5 - 5), Size = new Size(textBoxWidth, 36), Font = new Font("Segoe UI", 14), DropDownStyle = ComboBoxStyle.DropDownList };
            cboChucVu.Items.AddRange(new string[] { "NhanVien", "GiaoHang", "Admin" });
            infoPanel.Controls.Add(lblChucVu);
            infoPanel.Controls.Add(cboChucVu);

            int btnY = infoPanel.Height - 90;
            int btnWidth = 160;
            int btnGap = (infoPanel.Width - (btnWidth * 3)) / 4;

            btnThem = CreateStandardButton("Thêm", new Point(btnGap, btnY), btnWidth, Color.FromArgb(76, 175, 80), btnThem_Click);
            btnXoa = CreateStandardButton("Xóa", new Point(btnGap * 2 + btnWidth, btnY), btnWidth, Color.FromArgb(244, 67, 54), btnXoa_Click);
            btnCapNhat = CreateStandardButton("Cập nhật", new Point(btnGap * 3 + btnWidth * 2, btnY), btnWidth, Color.FromArgb(33, 150, 243), btnCapNhat_Click);

            infoPanel.Controls.Add(btnThem);
            infoPanel.Controls.Add(btnXoa);
            infoPanel.Controls.Add(btnCapNhat);
        }

        // --- Hàm hỗ trợ ---
        private TextBox AddLabelAndTextBox(string labelText, Point location, int width, Control parent)
        {
            // ✅ Tăng cỡ chữ Label và TextBox, điều chỉnh lại kích thước
            Label lbl = new Label { Text = labelText, Font = new Font("Segoe UI", 14), Location = location, AutoSize = true };
            TextBox txt = new TextBox { Location = new Point(location.X + 160, location.Y - 5), Size = new Size(width, 36), Font = new Font("Segoe UI", 14) };
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
                Size = new Size(width, 60), // ✅ Tăng chiều cao nút
                // ✅ Tăng cỡ chữ nút
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

                NhanVienDTO nv = new NhanVienDTO
                {
                    HoTen = txtTenNhanVien.Text,
                    TenDangNhap = txtTenDangNhap.Text,
                    MatKhau = txtMatKhau.Text,
                    SDT = txtSoDienThoai.Text,
                    Email = txtGmail.Text,
                    VaiTro = cboChucVu.SelectedItem.ToString()
                };

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
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                catch (Exception ex)
                {
                    MessageBox.Show("Xóa nhân viên thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(txtTenNhanVien.Text) || cboChucVu.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NhanVienDTO nv = new NhanVienDTO
                {
                    MaNV = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value),
                    MaTK = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaTK"].Value),
                    HoTen = txtTenNhanVien.Text,
                    TenDangNhap = txtTenDangNhap.Text,
                    MatKhau = txtMatKhau.Text,
                    SDT = txtSoDienThoai.Text,
                    Email = txtGmail.Text,
                    VaiTro = cboChucVu.SelectedItem.ToString()
                };

                if (nhanVienDAL.CapNhatNhanVien(nv))
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhanVien();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}