using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class SanPhamDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public int Gia { get; set; }
        public int KhuyenMai { get; set; }
        public byte[] HinhAnh { get; set; } = Array.Empty<byte>();
        public string TrangThai { get; set; } = string.Empty;
    }
}
