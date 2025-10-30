using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_BLL
{
    public class GioHangBLL
    {
        private int maTK; // Mã TK của NV quầy

        public GioHangBLL(int maTK)
        {
            this.maTK = maTK;
        }

        // 1️⃣ Lấy giỏ hàng của NV quầy
        public List<SanPhamDTO> LayGioHang()
        {
            return GioHangDAL.LayGioHangTheoMaTK(maTK);
        }

        // 2️⃣ Thay đổi số lượng sản phẩm
        public void ThayDoiSoLuong(SanPhamDTO sp, int delta)
        {
            int soLuongMoi = sp.SoLuong + delta;
            if (soLuongMoi < 1) soLuongMoi = 1;
            if (soLuongMoi > 99) soLuongMoi = 99;

            sp.SoLuong = soLuongMoi;
            GioHangDAL.CapNhatSoLuong(maTK, sp.MaSP, sp.SoLuong);
        }

        // 3️⃣ Xóa sản phẩm khỏi giỏ
        public void XoaSanPhamKhoiGio(int maSP)
        {
            GioHangDAL.XoaSanPhamKhoiGio(maTK, maSP);
        }

        // 4️⃣ Xóa toàn bộ giỏ
        public void XoaToanBoGio()
        {
            GioHangDAL.XoaToanBoGio(maTK);
        }

        // 5️⃣ Tính tổng tiền trước giảm giá
        public decimal TinhTongTien(List<SanPhamDTO> danhSach)
        {
            decimal tong = 0;
            foreach (var sp in danhSach)
                tong += sp.Gia * sp.SoLuong;
            return tong;
        }

        // 6️⃣ Tính tiền sau giảm giá
        public decimal TinhThanhTien(decimal tong, string giam)
        {
            string phanTramStr = giam.Replace("%", "").Trim();
            if (decimal.TryParse(phanTramStr, out decimal phanTram) && phanTram > 0)
                return tong * (1 - phanTram / 100);
            return tong;
        }

        // 7️⃣ Tạo hóa đơn dạng text
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

        // 8️⃣ Xác nhận đơn hàng: trực tiếp thành Hoàn tất
        public void XacNhanDonHang(int maDH)
        {
            GioHangDAL.XacNhanDonHang(maDH);
        }
    }
}
