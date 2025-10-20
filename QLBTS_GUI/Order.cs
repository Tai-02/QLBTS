using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_GUI
{
    internal class Order
    {
        public string MaDon { get; set; }
        public string Gia { get; set; }
        public string TrangThai { get; set; }
        public Order(string maDon, string gia, string trangThai)
        {
            MaDon = maDon;
            Gia = gia;
            TrangThai = trangThai;
        }
    }
}
