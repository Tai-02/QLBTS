using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class LichSuHoatDongDTO
    {
        public int MaLS { get; set; }
        public int MaTK { get; set; }
        public int MaDH { get; set; }
        public int TongTien { get; set; }
        public string TrangThai { get; set; }
        public DateTime ThoiGian { get; set; }
        public string LoaiLichSu { get; set; } // "MuaHang", "BanHang", "GiaoHang"

    }
}
