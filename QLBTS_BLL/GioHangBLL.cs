using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_BLL
{
    public class GioHangBLL
    {
        // Lấy giỏ hàng
        public List<SanPhamDTO> LayGioHangTheoMaKH(int maKH)
        {
            return GioHangDAL.LayGioHangTheoMaKH(maKH);
        }

        // Cập nhật số lượng tăng/giảm
        public void ThayDoiSoLuong(int maKH, SanPhamDTO sp, int delta)
        {
            int soLuongMoi = sp.SoLuong + delta;
            if (soLuongMoi < 1) soLuongMoi = 1;
            if (soLuongMoi > 99) soLuongMoi = 99;

            sp.SoLuong = soLuongMoi;
            GioHangDAL.CapNhatSoLuong(maKH, sp.MaSP, sp.SoLuong);
        }

        // Xóa 1 sản phẩm
        public void XoaSanPhamKhoiGio(int maKH, int maSP)
        {
            GioHangDAL.XoaSanPhamKhoiGio(maKH, maSP);
        }

        // Xóa toàn bộ
        public void XoaToanBoGio(int maKH)
        {
            GioHangDAL.XoaToanBoGio(maKH);
        }

        // Tính tổng tiền
        public decimal TinhTongTien(List<SanPhamDTO> danhSach)
        {
            decimal tong = 0;
            foreach (var sp in danhSach)
                tong += sp.Gia * sp.SoLuong;
            return tong;
        }

        // Tính thành tiền sau giảm giá
        public decimal TinhThanhTien(decimal tong, string giam)
        {
            string phanTramStr = giam.Replace("%", "").Trim();
            if (decimal.TryParse(phanTramStr, out decimal phanTram) && phanTram > 0)
                return tong * (1 - phanTram / 100);
            return tong;
        }

        // Tạo chuỗi hóa đơn
        public string TaoHoaDonText(List<SanPhamDTO> danhSach, string giam)
        {
            string bill = "=========== HÓA ĐƠN ===========\n";
            decimal tong = 0;
            foreach (var sp in danhSach)
            {
                bill += $"{sp.TenSP} ({sp.Size}) - SL: {sp.SoLuong} - {sp.Gia:N0}đ\n";
                tong += sp.Gia * sp.SoLuong;
            }

            decimal thanhTien = TinhThanhTien(tong, giam);
            bill += "\n--------------------------------\n";
            bill += $"Tạm tính: {tong:N0}đ\nGiảm giá: {giam}\nThành tiền: {thanhTien:N0}đ\n";
            bill += "\nCảm ơn quý khách!\n";

            return bill;
        }
    }
}
