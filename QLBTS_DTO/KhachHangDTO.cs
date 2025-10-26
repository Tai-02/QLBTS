using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QLBTS_DTO
{
    public class KhachHangDTO
    {
        [Key]
        public int MaKH { get; set; }

        [ForeignKey("TaiKhoan")]
        //public int MaKH { get; set; }
        public int MaTK { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public byte[]? AnhKH { get; set; }
        public TaiKhoanDTO TaiKhoan { get; set; }

    }
}
