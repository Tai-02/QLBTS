using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class TaiKhoanDTO
    {
        public int MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public string VaiTro { get; set; } 
        public bool Active { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayKichHoat { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
        public int LevelID { get; set; }
    }
}
