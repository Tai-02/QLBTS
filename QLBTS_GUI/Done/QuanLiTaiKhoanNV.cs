using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class QuanLiTaiKhoanNV : Form
    {
        private TaiKhoanBLL taikhoanBLL;
        public QuanLiTaiKhoanNV()
        {
            InitializeComponent();
            taikhoanBLL = new TaiKhoanBLL();
            LoadTrangThaiComboBox();
        }

        private void QuanLiTaiKhoanNV_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
        }
        private void LoadTrangThaiComboBox()
        {
            cbb_trangthai.Items.Clear();

            cbb_trangthai.Items.Add("Hoạt Động");
            cbb_trangthai.Items.Add("Ngừng");

            cbb_trangthai.SelectedIndex = 0;
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                dgvQLTK.AutoGenerateColumns = false;
                dgvQLTK.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                List<TaiKhoanDTO> danhSachNV = taikhoanBLL.LayDanhSachNhanVien();

                dgvQLTK.DataSource = danhSachNV;

                dgvQLTK.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                dgvQLTK.RowsDefaultCellStyle.BackColor = Color.White;
                dgvQLTK.DefaultCellStyle.SelectionBackColor = Color.White;
                dgvQLTK.DefaultCellStyle.SelectionForeColor = Color.Black;

                dgvQLTK.EnableHeadersVisualStyles = false;
                dgvQLTK.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                dgvQLTK.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvQLTK.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgvQLTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra Row Index trước (đảm bảo không click vào header)
            if (e.RowIndex < 0)
            {
                return;
            }

            try
            {
                DataGridViewRow row = dgvQLTK.Rows[e.RowIndex];

                // ⭐️ Áp dụng ?. và ?? cho tất cả các Cell Value ⭐️
                // Điều này xử lý cả giá trị null/DBNull và đảm bảo trả về chuỗi rỗng nếu không có dữ liệu.
                txt_hoten.Text = row.Cells["HoTen"].Value?.ToString() ?? string.Empty;
                txt_tendangnhap.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? string.Empty;
                txt_email.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;

                // Lưu ý: Tên cột Sdt/SDT phải khớp chính xác
                txt_sdt.Text = row.Cells["Sdt"].Value?.ToString() ?? string.Empty;
                txt_diachi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                txt_vaitro.Text = row.Cells["VaiTro"].Value?.ToString() ?? string.Empty;
                txt_lvID.Text = row.Cells["LvID"].Value?.ToString() ?? string.Empty;

                // Xử lý ComboBox TrangThai
                if (dgvQLTK.Columns.Contains("TrangThai") && row.Cells["TrangThai"].Value != null)
                {
                    string trangThaiDuocChon = row.Cells["TrangThai"].Value?.ToString() ?? string.Empty;

                    if (cbb_trangthai.Items.Contains(trangThaiDuocChon))
                    {
                        cbb_trangthai.SelectedItem = trangThaiDuocChon;
                    }
                }
                else
                {
                    cbb_trangthai.SelectedIndex = -1; 
                }
            }
            catch (Exception ex)
            {
                // Giữ lại catch cho các lỗi khác (ví dụ: tên cột sai)
                MessageBox.Show($"Lỗi khi tải dữ liệu chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        

        private void QLTKNV_btn_Them_Click(object sender, EventArgs e)
        {
            const string MAT_KHAU_MAC_DINH = "taikhoan@123";

            // 1. Xác thực cơ bản (Giữ nguyên)
            if (string.IsNullOrWhiteSpace(txt_tendangnhap.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_tendangnhap.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_email.Text))
            {
                MessageBox.Show("Email không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_email.Focus();
                return;
            }

            // 2. XÁC THỰC LevelID (Giữ nguyên)
            int levelID;
            if (!int.TryParse(txt_lvID.Text, out levelID) || (levelID != 2 && levelID != 3))
            {
                MessageBox.Show("Level ID không hợp lệ. Level ID phải là 2 (Nhân viên Quầy) hoặc 3 (Nhân viên Giao).", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_lvID.Focus();
                return;
            }

            try
            {
                // 3. Chuẩn bị dữ liệu DTO
                TaiKhoanDTO tkMoi = new TaiKhoanDTO
                {
                    TenDangNhap = txt_tendangnhap.Text.Trim(),
                    MatKhau = MAT_KHAU_MAC_DINH,
                    Email = txt_email.Text.Trim(),

                    // Xử lý các trường có thể null
                    SDT = string.IsNullOrWhiteSpace(txt_sdt.Text) ? null : txt_sdt.Text.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(txt_diachi.Text) ? null : txt_diachi.Text.Trim(),
                    HoTen = string.IsNullOrWhiteSpace(txt_hoten.Text) ? null : txt_hoten.Text.Trim(),

                    // Vai trò và LevelID
                    LevelID = levelID,
                    VaiTro = (levelID == 2) ? "NhanVienQuay" : "NhanVienGiao",

                    // Sửa Active thành int (1 hoặc 0) nếu DTO là int
                    // Nếu DTO là bool, giữ nguyên: Active = (cbb_trangthai.SelectedItem.ToString() == "Hoạt Động")
                    Active = (cbb_trangthai.SelectedItem.ToString() == "Hoạt Động")? true : false,

                    NgayTao = DateTime.Now
                };

                // 4. Gọi BLL và kiểm tra giá trị trả về
                if (taikhoanBLL.InsertTaiKhoan(tkMoi))
                {
                    MessageBox.Show($"Thêm tài khoản '{tkMoi.TenDangNhap}' thành công!\nMật khẩu mặc định: {MAT_KHAU_MAC_DINH}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 5. Tải lại danh sách
                    LoadDanhSachNhanVien();
                    // ClearInputFields(); 
                }
                else
                {
                    // Nếu BLL trả về false, thường là lỗi nghiệp vụ (trùng tên/email)
                    MessageBox.Show("Thêm tài khoản thất bại. Tên đăng nhập hoặc Email có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Lỗi hệ thống bất ngờ (không phải lỗi nghiệp vụ từ BLL)
                MessageBox.Show($"Lỗi hệ thống khi thêm tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QLTKNV_btn_Xoa_Click(object sender, EventArgs e)
        {
            // 1. Đảm bảo có hàng được chọn
            if (dgvQLTK.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy hàng đầu tiên được chọn
            DataGridViewRow row = dgvQLTK.SelectedRows[0];

            string email = row.Cells["Email"].Value.ToString();
            int _matk = taikhoanBLL.LayMaTK_TheoEmail(email);
            string tenDangNhap = row.Cells["TenDangNhap"].Value?.ToString() ?? "N/A";


            // 2. Yêu cầu xác nhận
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản '{tenDangNhap}' không? Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                return;
            }

            // 3. Gọi BLL để xóa
            try
            {
                if (taikhoanBLL.XoaTaiKhoan(_matk))
                {
                    MessageBox.Show($"Đã xóa tài khoản '{tenDangNhap}' thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Tải lại danh sách
                    LoadDanhSachNhanVien();
                    // Gọi hàm clear fields sau khi xóa
                    // ClearInputFields(); 
                }
                else
                {
                    // Trường hợp BLL/DAL trả về false (rất hiếm nếu DAL ném lỗi)
                    MessageBox.Show("Xóa tài khoản thất bại. Vui lòng kiểm tra lại kết nối hoặc dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 5. Xử lý lỗi từ BLL/DAL (Khóa ngoại, lỗi hệ thống)
                MessageBox.Show($"Không thể xóa tài khoản '{tenDangNhap}'. \nNguyên nhân: {ex.Message}", "Lỗi xóa dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QLTKNV_btn_Capnhat_Click(object sender, EventArgs e)
        {
            if (dgvQLTK.SelectedRows.Count < 1 )
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để cập nhật.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy MaTK từ hàng đang được chọn
            DataGridViewRow row = dgvQLTK.SelectedRows[0];
            string email = row.Cells["Email"].Value.ToString();
            int maTK = taikhoanBLL.LayMaTK_TheoEmail(email);


            try
            {
                // 2. Tạo đối tượng TaiKhoanDTO và gán dữ liệu từ Form
                TaiKhoanDTO tk = new TaiKhoanDTO()
                {
                    MaTK = maTK,
                    TenDangNhap = txt_tendangnhap.Text.Trim(),
                    Email = txt_email.Text.Trim(),
                    SDT = string.IsNullOrWhiteSpace(txt_sdt.Text) ? null : txt_sdt.Text.Trim(),
                    DiaChi = string.IsNullOrWhiteSpace(txt_diachi.Text) ? null : txt_diachi.Text.Trim(),
                    HoTen = string.IsNullOrWhiteSpace(txt_hoten.Text) ? null : txt_hoten.Text.Trim(),                        
                    LevelID = int.Parse(txt_lvID.Text.Trim()),
                    VaiTro = int.Parse(txt_lvID.Text.Trim()) == 2 ? "NhanVienQuay" : "NhanVienGiao",
                    TrangThai = cbb_trangthai.SelectedItem?.ToString() ?? "",
                    Active = (cbb_trangthai.SelectedItem.ToString() == "Hoạt Động") ? true : false
                };
                

                if (taikhoanBLL.CapNhatTaiKhoan_QuanLy(tk))
                {
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Tải lại DataGridView để thấy sự thay đổi
                    LoadDanhSachNhanVien();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Không có dữ liệu nào được thay đổi (có thể dữ liệu cũ và mới giống nhau).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Bắt và hiển thị lỗi nghiệp vụ (trùng TenDangNhap/Email) hoặc lỗi hệ thống
                MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}", "Lỗi Cập Nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

