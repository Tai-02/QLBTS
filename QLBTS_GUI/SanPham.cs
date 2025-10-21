using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_GUI
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }
        public string LoaiSP { get; set; }
        public string Size { get; set; }        
        public string TrangThaiGiao { get; set; }

    }
}
