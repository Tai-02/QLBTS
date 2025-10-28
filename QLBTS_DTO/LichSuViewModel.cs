using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_DTO
{
    /// <summary>
    /// ViewModel cho lịch sử hoạt động
    /// </summary>
    public class LichSuViewModel
    {
        public int MaLS { get; set; }
        public int? MaTK { get; set; }
        public int? MaDH { get; set; }
        public int? TongTien { get; set; }
        public string TrangThai { get; set; }
        public DateTime ThoiGian { get; set; }
        public string LoaiLichSu { get; set; }

        // Property hiển thị
        public string NoiDung
        {
            get
            {
                string thoiGian = ThoiGian.ToString("H:mm dd/MM/yyyy");
                string moTa = "";

                switch (LoaiLichSu)
                {
                    case "MuaHang":
                        if (TrangThai == "Chờ xác nhận")
                            moTa = $"đã đặt hàng thành công, mã đơn {MaDH:000}";
                        else if (TrangThai == "Đã giao")
                            moTa = $"đã nhận hàng thành công, mã đơn {MaDH:000}";
                        else if (TrangThai == "Đã hủy")
                            moTa = $"đã hủy đơn hàng, mã đơn {MaDH:000}";
                        else
                            moTa = $"đơn hàng {MaDH:000} - {TrangThai}";
                        break;

                    case "BanHang":
                        moTa = $"đã bán hàng, đơn {MaDH:000}, tổng {TongTien:N0}đ";
                        break;

                    case "GiaoHang":
                        moTa = $"đã giao hàng, đơn {MaDH:000}";
                        break;

                    default:
                        moTa = $"hoạt động - mã đơn {MaDH:000}";
                        break;
                }

                return $"{thoiGian} {moTa}";
            }
        }

        public LichSuViewModel()
        {
            TrangThai = string.Empty;
            LoaiLichSu = "MuaHang";
        }
    }
}
