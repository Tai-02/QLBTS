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
        public int MaTK { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public byte[] AnhKH { get; set; }
    }
}
