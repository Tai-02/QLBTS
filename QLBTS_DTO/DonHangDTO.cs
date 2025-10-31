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
        public int? MaKhach { get; set; }    // MaTK của khách hàng
        public int? MaNVQuay { get; set; }   // MaTK của nhân viên quầy
        public int? MaNVGiao { get; set; }   // MaTK của nhân viên giao hàng
        public DateTime NgayDat { get; set; }
        public int TongTien { get; set; }
        public string? TrangThai { get; set; }

    }
}
