using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class KhachHangDTO
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;
        public DateTime NgayTao { get; set; }
    }
}
