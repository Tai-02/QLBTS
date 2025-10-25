using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    /// <summary>
    /// ViewModel hiển thị chi tiết sản phẩm
    /// </summary>
    public class ProductDetailViewModel
    {
        // Từ SanPham
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string Size { get; set; }
        public int SoLuong { get; set; }
        public int Gia { get; set; }
        public int KhuyenMai { get; set; }
        public byte[] HinhAnh { get; set; }
        public string TrangThai { get; set; }

        // Properties có logic
        public decimal GiaGoc => Gia;

        public decimal GiaSauKM
        {
            get
            {
                if (KhuyenMai > 0)
                {
                    return Gia * (1 - KhuyenMai / 100m);
                }
                return Gia;
            }
        }

        public bool CoKhuyenMai => KhuyenMai > 0;
        public bool ConHang => TrangThai == "Còn hàng";

        public string TextGia
        {
            get
            {
                if (CoKhuyenMai)
                {
                    return $"Giá: {GiaSauKM:N0}đ";
                }
                return $"Giá: {Gia:N0}đ";
            }
        }

        public ProductDetailViewModel()
        {
            TenSP = string.Empty;
            Size = "M";
            TrangThai = "Còn hàng";
        }
    }
}
