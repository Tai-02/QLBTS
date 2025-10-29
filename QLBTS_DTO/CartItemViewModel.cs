using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    /// <summary>
    /// ViewModel để hiển thị sản phẩm trong giỏ hàng
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

        // ===== TỪ GioHang  =====
        public int MaTK { get; set; }        
        public DateTime? NgayTao { get; set; }

        // ===== PROPERTIES CÓ LOGIC =====
        public decimal GiaGoc => Gia;

        public decimal GiaSauKM
        {
            get
            {
                if (KhuyenMai > 0 && KhuyenMai <= 100)
                {
                    return Gia * (1 - KhuyenMai / 100m);
                }
                return Gia;
            }
        }

        public decimal ThanhTien => GiaSauKM * SoLuong;

        public decimal TienGiam
        {
            get
            {
                if (KhuyenMai > 0)
                {
                    return (GiaGoc - GiaSauKM) * SoLuong;
                }
                return 0;
            }
        }

        public bool CoKhuyenMai => KhuyenMai > 0;
        public bool ConHang => TrangThai == "Còn hàng";

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

        public CartItemViewModel()
        {
            TenSP = string.Empty;
            Size = "M";
            TrangThai = "Còn hàng";
        }
    }
}
