using System;
using System.Collections.Generic;
using QLBTS_DAL;
using QLBTS_DTO;

namespace QLBTS_BLL
{
    public class GioHangCuaBanBLL
    {
        public List<SanPhamDTO> LayGioHangTheoMaKH(int maKH)
        {
            return GioHangCuaBanDAL.LayGioHangTheoMaKH(maKH);
        }

        public decimal TinhTongTien(List<SanPhamDTO> danhSachSP)
        {
            decimal tong = 0;
            foreach (var sp in danhSachSP)
                tong += sp.Gia * sp.SoLuong;
            return tong;
        }

        public decimal TinhThanhTienSauGiam(string tongTienText, string giamText)
        {
            decimal.TryParse(tongTienText.Replace("đ", "").Replace(",", "").Trim(), out decimal tong);
            string giam = giamText.Replace("%", "").Trim();

            if (decimal.TryParse(giam, out decimal phanTram) && phanTram > 0)
            {
                return tong * (1 - phanTram / 100);
            }
            else
            {
                return tong;
            }
        }
    }
}
