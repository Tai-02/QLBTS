using System;
using System.Collections.Generic;
using QLBTS_DAL;
using QLBTS_DTO;

namespace QLBTS_BLL
{
    public class QuanLiSanPhamBLL
    {
        public List<SanPhamDTO> GetAllSanPham()
        {
            return QuanliSanPhamDAL.GetAll();
        }

        public bool ThemSanPham(SanPhamDTO sp)
        {
            KiemTraHopLe(sp, isUpdate: false);
            return QuanliSanPhamDAL.Insert(sp);
        }

        public bool CapNhatSanPham(SanPhamDTO sp)
        {
            if (sp.MaSP <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ!");

            KiemTraHopLe(sp, isUpdate: true);
            return QuanliSanPhamDAL.Update(sp);
        }

        public bool XoaSanPham(int maSP)
        {
            if (maSP <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ!");
            return QuanliSanPhamDAL.Delete(maSP);
        }

        public List<string> GetDistinctLoaiSP()
        {
            return QuanliSanPhamDAL.GetDistinctLoaiSP();
        }

        // ✅ Kiểm tra hợp lệ (phù hợp DTO dùng int, không nullable)
        private void KiemTraHopLe(SanPhamDTO sp, bool isUpdate)
        {
            if (sp == null)
                throw new ArgumentNullException(nameof(sp), "Thông tin sản phẩm không được null!");

            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new ArgumentException("Tên sản phẩm không được để trống!");

            // ✅ Ít nhất 1 giá > 0
            if (sp.GiaM <= 0 && sp.GiaL <= 0)
                throw new ArgumentException("Phải có ít nhất một giá (M hoặc L) hợp lệ (> 0)!");

            // ✅ Kiểm tra khuyến mãi (nếu có)
            if (sp.KhuyenMaiM < 0 || sp.KhuyenMaiL < 0)
                throw new ArgumentException("Khuyến mãi không được nhỏ hơn 0!");

            if (sp.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ!");

        }
    }
}
