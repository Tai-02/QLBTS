using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class NhanVienDTO
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MatKhau { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        public int MaVaiTro { get; set; }
        public string TrangThai { get; set; } = string.Empty;
    }
}
