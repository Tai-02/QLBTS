using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    /// <summary>
    /// ViewModel để hiển thị sản phẩm trong giỏ hàng
    /// Kết hợp data từ: ChiTietGioHang + SanPham + GioHang
    /// </summary>
    public class CartItemViewModel
    {
        // ===== TỪ ChiTietGioHang =====
        public int MaCTGH { get; set; }
        public int MaGH { get; set; }
        public int MaSP { get; set; }
        public int SoLuong { get; set; }

        // ===== TỪ SanPham =====
        public string TenSP { get; set; }
        public string Size { get; set; }
        public int Gia { get; set; }
        public int KhuyenMai { get; set; }
        public byte[] HinhAnh { get; set; }
        public string TrangThai { get; set; }

        // ===== TỪ GioHang =====
        public int? MaKH { get; set; }
        public int? MaNV { get; set; }
        public DateTime? NgayTao { get; set; }

        // ===== PROPERTIES CÓ LOGIC =====

        /// <summary>
        /// Giá gốc trước khuyến mãi (lấy trực tiếp từ DB)
        /// </summary>
        public decimal GiaGoc => Gia;  // Gia trong DB đã là giá gốc

        /// <summary>
        /// Giá sau khuyến mãi
        /// </summary>
        public decimal GiaSauKM
        {
            get
            {
                if (KhuyenMai > 0)
                {
                    // Công thức: GiaSauKM = GiaGoc × (1 - KM/100)
                    return Gia * (1 - KhuyenMai / 100m);
                }
                return Gia;
            }
        }

        /// <summary>
        /// Tổng tiền cho item này (Giá x Số lượng)
        /// </summary>
        public decimal ThanhTien => Gia * SoLuong;

        /// <summary>
        /// Số tiền được giảm
        /// </summary>
        public decimal TienGiam
        {
            get
            {
                if (KhuyenMai > 0)
                {
                    return (GiaGoc - GiaSauKM) * SoLuong;  // ✓ ĐÚNG
                }
                return 0;
            }
        }

        /// <summary>
        /// Kiểm tra có khuyến mãi không
        /// </summary>
        public bool CoKhuyenMai => KhuyenMai > 0;

        /// <summary>
        /// Kiểm tra còn hàng không
        /// </summary>
        public bool ConHang => TrangThai == "Còn hàng";

        /// <summary>
        /// Text hiển thị khuyến mãi
        /// </summary>
        public string TextKhuyenMai
        {
            get
            {
                if (KhuyenMai > 0)
                {
                    return $"-{KhuyenMai}%";
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public CartItemViewModel()
        {
            TenSP = string.Empty;
            Size = "M";
            TrangThai = "Còn hàng";
        }
    }
}
