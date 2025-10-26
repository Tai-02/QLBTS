using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class TaiKhoanDTO
    {
        /*
        [Key] //thêm dòng này để EF nhận diện khóa chính
        public int MaTK { get; set; }

        //public int MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string? Otp { get; set; }
        public string VaiTro { get; set; } 
        public bool Active { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayKichHoat { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
        public int LevelID { get; set; }

        // Liên kết với NguoiDung
        // Khóa ngoại
        public KhachHangDTO? KhachHang { get; set; }
        */

        public int MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public byte[] Anh { get; set; }
        public string Otp { get; set; }
        public string VaiTro { get; set; }     // "Admin", "NhanVienQuay", "NhanVienGiao", "KhachHang"
        public bool Active { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime? NgayKichHoat { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
        public int LevelID { get; set; }       // 0: Khách, 1: Admin, 2: NV Quầy, 3: NV Giao
        public string TrangThai { get; set; }  // "Hoạt động", "Ngưng"
    }
}
