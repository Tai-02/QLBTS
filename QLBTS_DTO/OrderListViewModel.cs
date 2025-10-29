using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QLBTS_DTO
{
    /// <summary>
    /// ViewModel cho danh sách đơn hàng
    /// </summary>
    public class OrderListViewModel
    {
        public int MaDH { get; set; }
        public int? MaKhach { get; set; }
        public string TenKhach { get; set; }
        public DateTime NgayDat { get; set; }
        public int TongTien { get; set; }
        public string TrangThai { get; set; }

        // Properties hiển thị
        public string DisplayMaDH => $"#{MaDH}";
        public string DisplayTongTien => $"{TongTien:N0}đ";

        public string ThongTinDon
        {
            get
            {
                // Chỉ hiển thị nút "Đã nhận" cho đơn đang giao
                if (TrangThai == "Đang giao")
                    return "Đã nhận";
                return string.Empty;
            }
        }

        

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

        public OrderListViewModel()
        {
            TenKhach = string.Empty;
            TrangThai = "Chờ xác nhận";
        }
    }
}
