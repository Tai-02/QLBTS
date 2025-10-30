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
            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new ArgumentException("Tên sản phẩm không được để trống!");
            if (sp.Gia <= 0)
                throw new ArgumentException("Giá sản phẩm phải lớn hơn 0!");
            if (sp.Size != "M" && sp.Size != "L")
                throw new ArgumentException("Size phải là M hoặc L");
            if (sp.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ!");
            if (sp.KhuyenMai < 0)
                throw new ArgumentException("Khuyến mãi không hợp lệ!");

            return QuanliSanPhamDAL.Insert(sp);
        }

        public bool CapNhatSanPham(SanPhamDTO sp)
        {
            if (sp.MaSP <= 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ!");
            if (string.IsNullOrWhiteSpace(sp.TenSP))
                throw new ArgumentException("Tên sản phẩm không được để trống!");
            if (sp.Gia <= 0)
                throw new ArgumentException("Giá sản phẩm phải lớn hơn 0!");
            if (sp.Size != "M" && sp.Size != "L")
                throw new ArgumentException("Size phải là M hoặc L");
            if (sp.SoLuong < 0)
                throw new ArgumentException("Số lượng không hợp lệ!");
            if (sp.KhuyenMai < 0)
                throw new ArgumentException("Khuyến mãi không hợp lệ!");

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

    }
}
