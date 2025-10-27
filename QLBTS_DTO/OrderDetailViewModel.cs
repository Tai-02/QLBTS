using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    /// <summary>
    /// ViewModel cho chi tiết đơn hàng
    /// Kết hợp: DonHang + ChiTietDonHang + SanPham
    /// </summary>
    public class OrderDetailViewModel
    {
        // ===== TỪ DonHang =====
        public int MaDH { get; set; }
        public int? MaKhach { get; set; }  
        public int? MaNVQuay { get; set; }
        public int? MaNVGiao { get; set; }
        public DateTime NgayDat { get; set; }
        public int TongTien { get; set; }
        public string TrangThai { get; set; }

        // ===== TỪ ChiTietDonHang =====
        public int MaCT { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }

        // ===== TỪ SanPham =====
        public string TenSP { get; set; }
        public string Size { get; set; }
        public byte[] HinhAnh { get; set; }
        public int KhuyenMai { get; set; }

        // ===== PROPERTIES CÓ LOGIC =====

        /// <summary>
        /// Thành tiền của item này
        /// </summary>
        public decimal ThanhTien => DonGia * SoLuong;
    
    }
}
