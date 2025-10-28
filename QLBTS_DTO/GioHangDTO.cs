using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    public class GioHangDTO
    {
        public int MaGH { get; set; }
        public int MaTK { get; set; }   // có thể là khách hoặc nhân viên
        public DateTime NgayTao { get; set; }

    }
}
