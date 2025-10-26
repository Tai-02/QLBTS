using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_BLL
{
    public class SanPhamBLL
    {
        // Lay toan bo danh sach san pham
        public List<SanPham> GetAllSanPham()
        {
            return SanPhamDAL.GetAll();
        }

        // Them moi san pham
        public bool ThemSanPham(SanPham sp)
        {
            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new ArgumentException("Tên sản phẩm không được để trống!");
            if (sp.Gia <= 0)
                throw new ArgumentException("Giá sản phẩm phải lớn hơn 0!");
            if (sp.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ!");

            return SanPhamDAL.Insert(sp);
        }

        // Cap nhat san pham
        public bool CapNhatSanPham(SanPham sp)
        {
            if (sp.MaSP <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ!");
            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new ArgumentException("Tên sản phẩm không được để trống!");
            if (sp.Gia <= 0)
                throw new ArgumentException("Giá sản phẩm phải lớn hơn 0!");
            if (sp.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ!");

            return SanPhamDAL.Update(sp);
        }

        // Xoa san pham
        public bool XoaSanPham(int maSP)
        {
            if (maSP <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ!");

            return SanPhamDAL.Delete(maSP);
        }
    }
}
