using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class DonHangDTO
    {
        public int MaDH { get; set; }
        public int? MaKH { get; set; }
        public int? MaNVQuay { get; set; }
        public int? MaNVGiao { get; set; }
        public DateTime NgayDat { get; set; }
        public int TongTien { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public string DiaChiKhachHang { get; set; }
        public string SDTKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        // Thêm dòng này vào file DonHangDTO.cs
        public DateTime ThoiGianGiao { get; set; }
    }
}
