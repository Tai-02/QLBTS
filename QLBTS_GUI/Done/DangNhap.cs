using MySql.Data.MySqlClient;
using QLBTS_BLL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBTS_GUI
{
    public partial class DangNhap : Form
    {
        private TaiKhoanBLL bll = new TaiKhoanBLL();
        Control ParentPanel;
        UI_Form ui = new UI_Form();

        public DangNhap(Control pn)
        {
            InitializeComponent();
            ParentPanel = pn;
        }


        private void Dn_btn_dangnhap_Click(object sender, EventArgs e)
        {
            string username = Dn_txt_tendangnhap.Text.Trim();
            string password = Dn_txt_mk.Text.Trim();

            // 1. Kiểm tra rỗng
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TaiKhoanDTO tk = new TaiKhoanDTO
            {
                TenDangNhap = username,
                MatKhau = password
            };

            // **SỬ DỤNG TRY-CATCH ĐỂ BẮT LỖI NGHIỆP VỤ TỪ BLL**
            try
            {
                // Giả sử bll.XuLyDangNhapVaKiemTraTrangThai(tk) trả về LevelID nếu thành công
                int levelID = bll.XuLyDangNhapVaKiemTraTrangThai(tk);

                // 2. Đăng nhập thành công (Không có Exception nào bị ném)
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật thông tin toàn cục
                Khung.lvID_temp = levelID;
                Khung.MaTK_temp = bll.LayMaTK(username);

                // Mở Form tương ứng
                if (Khung.lvID_temp == 0) ui.OpenChildForm(new Khachhang(), ParentPanel);
                else if (Khung.lvID_temp == 1) ui.OpenChildForm(new Admin(), ParentPanel);
                else if (Khung.lvID_temp == 2) ui.OpenChildForm(new NVQUAY(), ParentPanel);
                else if (Khung.lvID_temp == 3) ui.OpenChildForm(new NVGIAO(), ParentPanel);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;

                // 3. Xử lý Lỗi: Tài khoản chưa kích hoạt (Active = 0)
                if (errorMessage.Contains("chưa được kích hoạt"))
                {
                    DialogResult dialogResult = MessageBox.Show(
                        "Tài khoản này chưa được kích hoạt. Bạn có muốn kích hoạt ngay bây giờ không?",
                        "Xác nhận Kích hoạt",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (dialogResult == DialogResult.Yes)
                    {
                        string email = bll.LayEmailTheoTenDangNhap(username);
                        Random rd = new Random();

                        tk.Otp = rd.Next(0, 999999).ToString();
                        tk.NgayTao = DateTime.Now;

                        string message = $"Xin chào {tk.TenDangNhap},\n\nMã OTP kích hoạt của bạn là: {tk.Otp}\n\nVui lòng nhập mã này trong ứng dụng để kích hoạt tài khoản.";

                        if(bll.CapNhatOTPVaNgayTao(email, tk.Otp, message))
                        {
                            MessageBox.Show("Vui lòng kiểm tra email để nhận mã OTP.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ui.OpenChildForm(new NhapOTP(ParentPanel, email), ParentPanel);
                        }

                        return; 
                    }
                }

                // 4. Xử lý các lỗi khác (Tên đăng nhập không tồn tại, Mật khẩu sai)
                else
                {
                    MessageBox.Show(errorMessage, "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Xóa thông tin đăng nhập sau khi thất bại
                Dn_txt_tendangnhap.Clear();
                Dn_txt_mk.Clear();
                Dn_txt_tendangnhap.Focus(); // Cho phép người dùng nhập lại từ đầu
            }
        }

        private void Dn_btn_Dangkingay_Click(object sender, EventArgs e)
        {
            UI_Form ui = new UI_Form();
            ui.OpenChildForm(new DangKi(ParentPanel), ParentPanel);
        }
    }
}
