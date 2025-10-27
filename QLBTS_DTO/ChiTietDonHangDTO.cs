using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace QLBTS_DTO
{
    public class ChiTietDonHangDTO
    {
        public int MaCT { get; set; }
        public int MaDH { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public string TenSP { get; set; } = string.Empty; // Gán giá trị mặc định
        public string Size { get; set; } = string.Empty; // Gán giá trị mặc định
        // Thuộc tính HinhAnh đã được xóa
    }
}