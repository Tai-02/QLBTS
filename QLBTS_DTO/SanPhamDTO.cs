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
        public string TenSP { get; set; }
        public string Size { get; set; } // "M" | "L"
        public int SoLuong { get; set; }
        public int Gia { get; set; }
        public int KhuyenMai { get; set; }
        //public byte[] HinhAnh { get; set; }
        public string HinhAnh { get; set; }
        public string TrangThai { get; set; }
    }
}
