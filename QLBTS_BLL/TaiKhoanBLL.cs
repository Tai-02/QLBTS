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

        public bool KiemTraTaiKhoan(TaiKhoanDTO tk)
        {
            return dal.Kiemtra(tk);
        }

        public bool DangKy(TaiKhoanDTO tk)
        {
            if (dal.TenDangNhapTonTai(tk.TenDangNhap))
                throw new Exception("Tên đăng nhập đã tồn tại!");
            if (dal.EmailTonTai(tk.Email))
                throw new Exception("Email đã tồn tại!");
            Random rd = new Random();
            tk.Otp = rd.Next(0, 1000).ToString();
            tk.VaiTro = "KhachHang";
            tk.Active = false;
            tk.NgayTao = DateTime.Now;
            tk.LevelID = 0;

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

    }
}

