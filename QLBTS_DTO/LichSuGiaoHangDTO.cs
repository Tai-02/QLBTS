using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class LichSuGiaoHangDTO
    {
        public int MaLSGH { get; set; }
        public int MaNVGiao { get; set; }
        public int MaDH { get; set; }
        public int TongTien { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public DateTime ThoiGian { get; set; }
    }
}
