using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_BLL
{
    public class GioHangBLL
    {
        // 🔹 1️⃣ Lấy giỏ hàng theo mã tài khoản
        public List<SanPhamDTO> LayGioHangTheoMaTK(int maTK)
        {
            if (maTK <= 0)
                throw new ArgumentException("Mã tài khoản không hợp lệ!");

            return GioHangDAL.LayGioHangTheoMaTK(maTK);
        }

        // 🔹 2️⃣ Thêm sản phẩm vào giỏ hàng
        public void ThemSanPhamVaoGio(int maTK, SanPhamDTO sp, int soLuong = 1)
        {
            if (sp == null)
                throw new ArgumentNullException(nameof(sp), "Sản phẩm không được null!");
            if (soLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            GioHangDAL.ThemSanPhamVaoGio(maTK, sp, soLuong);
        }

        // 🔹 3️⃣ Cập nhật số lượng sản phẩm trong giỏ theo size
        public void CapNhatSoLuong(int maTK, int maSP, string size, int soLuong)
        {
            if (soLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            GioHangDAL.CapNhatSoLuong(maTK, maSP, size, soLuong);
        }

        // 🔹 4️⃣ Xóa 1 sản phẩm khỏi giỏ
        public void XoaSanPhamKhoiGio(int maTK, int maSP, string size)
        {
            GioHangDAL.XoaSanPhamKhoiGio(maTK, maSP, size);
        }

        // 🔹 5️⃣ Xóa toàn bộ giỏ hàng
        public void XoaToanBoGio(int maTK)
        {
            GioHangDAL.XoaToanBoGio(maTK);
        }

        // 🔹 6️⃣ Khách hàng đặt hàng
        public int DatHangKH(int maKhach, List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Không có sản phẩm nào để đặt hàng!");

            return GioHangDAL.DatHangKH(maKhach, listSP);
        }

        // 🔹 7️⃣ Bán hàng nhân viên quầy
        public int BanHangNVQ(List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Không có sản phẩm nào để bán!");

            return GioHangDAL.BanHangNVQ(listSP);
        }

        // 🔹 8️⃣ Tăng số lượng sản phẩm (+1)
        public void TangSoLuong(int maTK, int maSP, string size)
        {
            GioHangDAL.TangSoLuong(maTK, maSP, size);
        }

        // 🔹 9️⃣ Giảm số lượng sản phẩm (-1)
        public void GiamSoLuong(int maTK, int maSP, string size)
        {
            GioHangDAL.GiamSoLuong(maTK, maSP, size);
        }

        // 🔹 10️⃣ Tính tổng tiền giỏ hàng
        public decimal[] TinhTongTienGioHang(int maTK)
        {
            return GioHangDAL.TinhTongTienGioHang(maTK);
        }
    }
}