using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace QLBTS_DTO
{
    public class SanPhamDTO
    {
        public int MaSP { get; set; }
        public string? TenSP { get; set; }
        public string? LoaiSP { get; set; }
        public string? Size { get; set; }  // "M" hoặc "L"
        public int SoLuong { get; set; }

        public int GiaM { get; set; }
        public int GiaL { get; set; }
        public int KhuyenMaiM { get; set; }
        public int KhuyenMaiL { get; set; }

        public byte[]? HinhAnh { get; set; }
        public string? TrangThai { get; set; }

        public int GiaHienTai => Size == "M" ? GiaM : GiaL;
        public int KhuyenMaiHienTai => Size == "M" ? KhuyenMaiM : KhuyenMaiL;
    }

}
