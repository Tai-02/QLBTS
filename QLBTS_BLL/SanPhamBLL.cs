using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBTS_BLL
{
    public class SanPhamBLL
    {
        private SanPhamDAL dal = new SanPhamDAL();

        // 🔹 Lấy chi tiết sản phẩm theo MaSP
        public SanPhamDTO LaySanPham(int maSP, string size)
        {
            return dal.GetSanPhamTheoMaSP(maSP, size);
        }

        // 🔹 Kiểm tra tồn kho trước khi đặt hàng
        public bool KiemTraTonKho(int maSP, int soLuongCanMua)
        {
            return dal.CheckTonKho(maSP, soLuongCanMua);
        }

        // 🔹 Cập nhật tồn kho (trừ khi đặt hàng, cộng khi hủy)
        public bool CapNhatTonKho(int maSP, int soLuongThayDoi)
        {
            return dal.CapNhatTonKho(maSP, soLuongThayDoi);
        }

        // 🔹 Lấy giá hiện tại theo size (có KM)
        public int LayGiaHienTai(SanPhamDTO sp, string size)
        {
            if (sp == null) return 0;

            int gia = size.ToUpper() == "M" ? sp.GiaM : sp.GiaL;
            int km = size.ToUpper() == "M" ? sp.KhuyenMaiM : sp.KhuyenMaiL;

            double giaSauKM = gia - (gia * km / 100.0);
            // Làm tròn về bội số 1000
            return (int)(Math.Round(giaSauKM / 1000.0) * 1000);
        }

        // 🔹 Lấy giá hiển thị dạng string
        public string GetTextGia(SanPhamDTO sp, string size)
        {
            int gia = LayGiaHienTai(sp, size);
            return $"{gia:N0}đ";
        }

        // 🔹 Đặt hàng (trừ tồn kho)
        public bool DatHang(int maSP, int soLuong)
        {
            if (!KiemTraTonKho(maSP, soLuong))
                return false;

            return CapNhatTonKho(maSP, -soLuong);
        }

        // 🔹 Hủy đơn, trả lại hàng
        public bool HuyDon(int maSP, int soLuong)
        {
            return CapNhatTonKho(maSP, soLuong);
        }

        public int GetGiaHienTai(int maSP, string size)
        {
            if (size != "M" && size != "L")
                throw new ArgumentException("Size không hợp lệ. Phải là 'M' hoặc 'L'.");

            return dal.GetGiaHienTai(maSP, size);
        }

        public int GetSoLuongTon(int maSP)
        {
            return dal.GetSoLuongTon(maSP);
        }
    }
}
