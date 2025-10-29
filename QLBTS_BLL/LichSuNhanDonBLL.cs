using QLBTS_DAL;
using QLBTS_DTO;
using System;
using System.Collections.Generic;

namespace QLBTS_BBL
{
    public class FormLichSuNhanDonBBL
    {
        public List<LichSuHoatDongDTO> LayLichSuTheoMaNV(int maNVGiao)
        {
            // Xu ly nghiep vu truoc khi goi DAL
            if (maNVGiao <= 0)
                throw new ArgumentException("Mã nhân viên giao không hợp lệ.");

            // Goi du lieu tu DAL
            List<LichSuHoatDongDTO> list = LichSuNhanDonDAL.LayLichSuTheoMaNV(maNVGiao);

            // Xu ly sau khi lay du lieu neu can (vi du sap xep, loc, tinh toan bo sung,...)
            if (list == null)
                list = new List<LichSuHoatDongDTO>();

            return list;
        }
    }
}
