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

        /// <summary>
        /// Text hiển thị trạng thái
        /// </summary>
        public string TextTrangThai
        {
            get
            {
                return TrangThai switch
                {
                    "Chờ xác nhận" => "⏳ Chờ xác nhận",
                    "Đã xác nhận" => "✓ Đã xác nhận",
                    "Đang pha chế" => "☕ Đang pha chế",
                    "Chờ giao hàng" => "📦 Chờ giao hàng",
                    "Đang giao" => "🚚 Đang giao",
                    "Đã giao" => "✅ Đã giao",
                    "Đã hủy" => "❌ Đã hủy",
                    _ => TrangThai
                };
            }
        }

        /// <summary>
        /// Màu trạng thái
        /// </summary>
        public System.Drawing.Color ColorTrangThai
        {
            get
            {
                return TrangThai switch
                {
                    "Chờ xác nhận" => System.Drawing.Color.Orange,
                    "Đã xác nhận" => System.Drawing.Color.Blue,
                    "Đang pha chế" => System.Drawing.Color.Purple,
                    "Chờ giao hàng" => System.Drawing.Color.DarkOrange,
                    "Đang giao" => System.Drawing.Color.DodgerBlue,
                    "Đã giao" => System.Drawing.Color.Green,
                    "Đã hủy" => System.Drawing.Color.Red,
                    _ => System.Drawing.Color.Black
                };
            }
        }

        public OrderDetailViewModel()
        {
            TenSP = string.Empty;
            Size = "M";
            TrangThai = "Chờ xác nhận";
        }
    }
}
