using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;
namespace QLBTS_BBL
{
    public class LichSuHoatDongBBL
    {

        public List<LichSuHoatDongDTO> LayLichSu(int maTK)
        {
            // Xu ly nghiep vu truoc khi goi DAL
            if (maTK <= 0)
                throw new ArgumentException("Mã nhân viên giao không hợp lệ.");

            // Goi du lieu tu DAL
            List<LichSuHoatDongDTO> list = LichSuHoatDongDAL.GetLichSu(maTK);

            // Xu ly sau khi lay du lieu neu can (vi du sap xep, loc, tinh toan bo sung,...)
            if (list == null)
                list = new List<LichSuHoatDongDTO>();

            return list;
        }

        public bool ThemLichSu(int maTK, int maDH, string moTa, int tongTien, string loaiLichSu)
        {
            try
            {
                // Gọi hàm ở tầng DAL
                return LichSuHoatDongDAL.ThemLichSu(maTK, maDH, moTa, tongTien, loaiLichSu);
            }
            catch (Exception ex)
            {
                // Có thể log hoặc xử lý lỗi thêm ở đây
                throw new Exception("Lỗi BLL - ThemLichSu: " + ex.Message, ex);
            }
        }
    }
}
