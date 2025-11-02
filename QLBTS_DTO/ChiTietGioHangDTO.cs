using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class ChiTietGioHangDTO
    {
        public int MaCTGH { get; set; }
        public int MaGH { get; set; }
        public int MaSP { get; set; }
        public string? Size { get; set; }  // "M" hoặc "L"
        public int SoLuong { get; set; }
    }
}

