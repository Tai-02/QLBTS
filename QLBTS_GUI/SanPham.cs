using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_GUI
{
    internal class SanPham
    {
        public string Ten { get; set; }
        public string Size { get; set; }
        public decimal Gia { get; set; }
        public string HinhAnh { get; set; }  // đường dẫn ảnh (VD: "images/trasua1.jpg")
    }
}
