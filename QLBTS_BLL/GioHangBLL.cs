using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_BLL
{
    public class GioHangBLL
    {
        GioHangDAL dal = new GioHangDAL();

        // 1️⃣ Lấy giỏ hàng theo mã tài khoản
        public List<SanPhamDTO> LayGioHangTheoMaTK(int maTK)
        {
            if (maTK <= 0)
                throw new ArgumentException("Mã tài khoản không hợp lệ!");

            return GioHangDAL.LayGioHangTheoMaTK(maTK);
        }

        // 2️⃣ Thêm sản phẩm vào giỏ hàng
        public void ThemSanPhamVaoGio(int maTK, SanPhamDTO sp, int soLuong = 1)
        {
            if (sp == null)
                throw new ArgumentNullException(nameof(sp), "Sản phẩm không được null!");

            if (soLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");

            try
            {
                GioHangDAL.ThemSanPhamVaoGio(maTK, sp, soLuong);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("đã có trong giỏ"))
                    throw new Exception("Sản phẩm đã tồn tại trong giỏ hàng!");
                throw;
            }
        }

        // 3️⃣ Cập nhật số lượng
        public void CapNhatSoLuong(int maTK, int maSP, int soLuong)
        {
            if (soLuong <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");
            GioHangDAL.CapNhatSoLuong(maTK, maSP, soLuong);
        }

        // 4️⃣ Xóa 1 sản phẩm
        public void XoaSanPhamKhoiGio(int maTK, int maSP)
        {
            GioHangDAL.XoaSanPhamKhoiGio(maTK, maSP);
        }

        // 5️⃣ Xóa toàn bộ giỏ hàng
        public void XoaToanBoGio(int maTK)
        {
            GioHangDAL.XoaToanBoGio(maTK);
        }

        // 6️⃣ Khách hàng đặt hàng
        public int DatHangKH(int maKhach, List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Không có sản phẩm nào để đặt hàng!");

            return GioHangDAL.DatHangKH(maKhach, listSP);
        }

        // 7️⃣ Bán hàng nhân viên quầy
        public int BanHangNVQ(List<SanPhamDTO> listSP)
        {
            if (listSP == null || listSP.Count == 0)
                throw new Exception("Không có sản phẩm nào để bán!");

            return GioHangDAL.BanHangNVQ(listSP);
        }

        public void TangSoLuong(int maTK, int maSP)
        {
            GioHangDAL.TangSoLuong(maTK, maSP);
        }

        public void GiamSoLuong(int maTK, int maSP)
        {
            GioHangDAL.GiamSoLuong(maTK, maSP);
        }
        public decimal[] TinhTongTienGioHang(int maTK)
        {
            return GioHangDAL.TinhTongTienGioHang(maTK);
        }

    }
}