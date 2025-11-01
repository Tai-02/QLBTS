using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();

        public int XuLyDangNhapVaKiemTraTrangThai(TaiKhoanDTO tk)
        {
            // 1. KIỂM TRA TÊN ĐĂNG NHẬP CÓ TỒN TẠI KHÔNG
            if (!dal.TenDangNhapTonTai(tk.TenDangNhap))
            {
                throw new Exception("Tên đăng nhập không tồn tại.");
            }

            // 2. KIỂM TRA MẬT KHẨU
            if (!dal.KiemTraMatKhau(tk))
            {
                // Mật khẩu sai (nhưng tên đăng nhập tồn tại)
                throw new Exception("Mật khẩu không đúng.");
            }

            // 3. KIỂM TRA TRẠNG THÁI ACTIVE
            int activeStatus = dal.LayTrangThaiActive(tk.TenDangNhap);

            if (activeStatus == 0)
            {
                // Mật khẩu đúng, nhưng Active = 0
                throw new Exception("Tài khoản chưa được kích hoạt.");
            }
            else if (activeStatus == -1)
            {
                // Lỗi dữ liệu/lỗi không tìm thấy TK dù đã kiểm tra tồn tại (Không nên xảy ra)
                throw new Exception("Lỗi hệ thống: Không tìm thấy thông tin tài khoản.");
            }

            // 4. ĐĂNG NHẬP THÀNH CÔNG -> LẤY LEVELID
            int levelID = dal.LayLevelID(tk.TenDangNhap); // Giả sử hàm LayLevelID đã có sẵn trong DAL

            if (levelID == -1)
            {
                throw new Exception("Lỗi hệ thống: Không xác định được vai trò (LevelID) của tài khoản.");
            }

            return levelID; // Trả về LevelID khi đăng nhập thành công
        }

        public bool DangKy(TaiKhoanDTO tk)
        {
            if (dal.TenDangNhapTonTai(tk.TenDangNhap))
                throw new Exception("Tên đăng nhập đã tồn tại!");
            if (dal.EmailTonTai(tk.Email))
                throw new Exception("Email đã tồn tại!");            

            string message = $"Xin chào {tk.TenDangNhap},\n\nMã OTP kích hoạt của bạn là: {tk.Otp}\n\nVui lòng nhập mã này trong ứng dụng để kích hoạt tài khoản.";

            if (!SendMailTo(tk.Email, message))
                throw new Exception("Không thể gửi email xác thực!");

            return dal.InsertTaiKhoan(tk);
        }

        public static bool SendMailTo(string email, string message)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("tai728298@gmail.com", "ofad bogh nshi dzsl"),
                    EnableSsl = true,
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("tai728298@gmail.com", "QLBTS App"),
                    Subject = "Xác thực tài khoản",
                    Body = message,
                    IsBodyHtml = false
                };
                mail.To.Add(email);
                client.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi gửi email: " + ex.Message);
            }
        }

        public bool KiemTraOTP(string email, string otp)
        {
            return dal.KiemTraOTP(email, otp);
        }

        public int LayLevelID(string tenDangNhap)
        {
            return dal.LayLevelID(tenDangNhap);
        }

        public int LayMaTK(string tenDangNhap)
        {
            return dal.LayMaTK(tenDangNhap);
        }

        public TaiKhoanDTO LayThongTinTaiKhoanTheoMaTK(int maTK)
        {
            return dal.LayThongTinTaiKhoanTheoMaTK(maTK);
        }

        public bool CapNhatThongTin(TaiKhoanDTO tk)
        {
            if (string.IsNullOrWhiteSpace(tk.HoTen) ||
                string.IsNullOrWhiteSpace(tk.Email))
                throw new ArgumentException("Tên đăng nhập và email không được để trống.");

            return dal.CapNhatThongTin(tk);
        }
        public List<TaiKhoanDTO> LayDanhSachNhanVien()
        {
            return dal.LayDanhSachNhanVien();
        }

        // Trong file TaiKhoanBLL.cs

        public bool InsertTaiKhoan(TaiKhoanDTO tk)
        {
            if (dal.TenDangNhapTonTai(tk.TenDangNhap))
                throw new Exception("Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.");

            if (dal.EmailTonTai(tk.Email))
                throw new Exception("Email đã tồn tại! Vui lòng sử dụng email khác.");

            // GỌI TẦNG DAL: Thực hiện chèn dữ liệu
            return dal.InsertTaiKhoan(tk);

            // Lưu ý: Nếu muốn trả về false, bạn phải bỏ 'throw' và thay bằng logic thông báo lỗi/return false
        }

        public bool XoaTaiKhoan(int maTK)
        {
            if (maTK <= 0)
            {
                throw new Exception("Mã tài khoản không hợp lệ.");
            }
            return dal.XoaTaiKhoan(maTK);
        }

        public int LayMaTK_TheoEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return -1;
            }
            return dal.LayMaTK_TheoEmail(email);
        }

        public bool CapNhatTaiKhoan_QuanLy(TaiKhoanDTO tk)
        {
            if (tk.MaTK <= 0)
                throw new Exception("Mã tài khoản không hợp lệ.");
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap))
                throw new Exception("Tên đăng nhập không được để trống.");
            if (string.IsNullOrWhiteSpace(tk.Email))
                throw new Exception("Email không được để trống.");

            return dal.CapNhatTaiKhoan_QuanLy(tk);
            
        }
        public bool CapNhatOTPVaNgayTao(string email, string otp, string message)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email không được để trống.");
            }

            if (!SendMailTo(email, message))
                throw new Exception("Không thể gửi email xác thực!");
            return dal.CapNhatOTPVaNgayTao(email, otp);
        }

        public string LayEmailTheoTenDangNhap(string tenDangNhap)
        {
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không hợp lệ.");
            }
            return dal.LayEmailTheoTenDangNhap(tenDangNhap);
        }

        public bool kttk(TaiKhoanDTO tk)
        {
            return dal.Kiemtra(tk);
        }

        public string LaySDT(int maTK)
        {
            try
            {
                string sdt = dal.LaySDTTheoMaTK(maTK);
                return sdt ?? ""; // nếu null thì trả về chuỗi rỗng, không ném lỗi
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL - LaySDT: " + ex.Message, ex);
            }
        }

        public string LayDiaChi(int maTK)
        {
            try
            {
                string diaChi = dal.LayDiaChiTheoMaTK(maTK);
                return diaChi ?? ""; // nếu null thì trả về chuỗi rỗng, không ném lỗi
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi BLL - LayDiaChi: " + ex.Message, ex);
            }
        }

    }
}

