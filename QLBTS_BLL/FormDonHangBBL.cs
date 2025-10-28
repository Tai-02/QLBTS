using QLBTS_DAL;
using System;
using System.Data;

namespace QLBTS_BLL
{
    public class FormDonHangBLL
    {
        // Lấy danh sách đơn hàng
        public DataTable LayDanhSachDonHang()
        {
            return QLDonHangFormDAL.LayDanhSachDonHang();
        }

        // Xử lý nhận đơn hàng
        public bool NhanDonHang(int maDH, string trangThai)
        {
            if (trangThai == "Đang giao")
                throw new InvalidOperationException("Đơn hàng đang giao, không thể nhận lại!");

            return QLDonHangFormDAL.CapNhatTrangThai(maDH, "Đã xác nhận");
        }

        // Xử lý hủy đơn
        public bool HuyDonHang(int maDH)
        {
            return QLDonHangFormDAL.CapNhatTrangThai(maDH, "Đã hủy");
        }

        // Xử lý chuyển hàng
        public bool ChuyenDonHang(int maDH)
        {
            return QLDonHangFormDAL.CapNhatTrangThai(maDH, "Đang giao");
        }

        // Lấy chi tiết đơn hàng
        public DataTable LayChiTietDonHang(int maDH)
        {
            return QLDonHangFormDAL.LayChiTietDonHang(maDH);
        }

        // Tạo nội dung hiển thị chi tiết đơn hàng
        public string TaoNoiDungChiTietDonHang(int maDH)
        {
            DataTable dt = QLDonHangFormDAL.LayChiTietDonHang(maDH);
            if (dt.Rows.Count == 0)
                return "Không có chi tiết cho đơn này.";

            string tenKH = dt.Rows[0]["TenKhachHang"]?.ToString() ?? "(không rõ)";
            string ngayDat = "";
            if (dt.Rows[0]["NgayDat"] != DBNull.Value)
                ngayDat = Convert.ToDateTime(dt.Rows[0]["NgayDat"]).ToString("dd/MM/yyyy HH:mm");

            string body = $"Khách hàng: {tenKH}\nNgày đặt: {ngayDat}\n\nSản phẩm:\n";
            foreach (DataRow r in dt.Rows)
            {
                body += $"- {r["TenSP"]}: SL {r["SoLuong"]}, Giá {r["DonGia"]}\n";
            }

            return body;
        }
    }
}
